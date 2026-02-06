using Microsoft.EntityFrameworkCore;
using PetWorld.Application.Interfaces;
using PetWorld.Application.Services;
using PetWorld.Infrastructure.Agents;
using PetWorld.Infrastructure.Data;
using PetWorld.Infrastructure.Repositories;
using PetWorld.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=mysql;Database=petworld;User=petworld;Password=petworld123;";

builder.Services.AddDbContext<PetWorldDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAgentService, WriterCriticAgentService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PetWorldDbContext>();
    var retries = 10;
    while (retries > 0)
    {
        try
        {
            context.Database.EnsureCreated();
            break;
        }
        catch
        {
            retries--;
            if (retries == 0) throw;
            Thread.Sleep(3000);
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
