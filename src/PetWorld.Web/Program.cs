using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using PetWorld.Application.Interfaces;
using PetWorld.Application.Services;
using PetWorld.Infrastructure.Agents;
using PetWorld.Infrastructure.Data;
using PetWorld.Infrastructure.Repositories;
using PetWorld.Infrastructure.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
						?? "Server=mysql;Database=petworld;User=petworld;Password=petworld123;";

builder.Services.AddDbContext<PetWorldDbContext>(options =>
	options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 0))));

builder.Services.AddSingleton<Kernel>(sp =>
{
	IConfiguration config = sp.GetRequiredService<IConfiguration>();
	string apiKey = config["OpenAI:ApiKey"]
					?? throw new InvalidOperationException("OpenAI:ApiKey not configured");
	string modelId = config["OpenAI:ModelId"] ?? "gpt-4o-mini";

	return Kernel.CreateBuilder()
		.AddOpenAIChatCompletion(modelId, apiKey)
		.Build();
});
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAgentService, WriterCriticAgentService>();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
	PetWorldDbContext context = scope.ServiceProvider.GetRequiredService<PetWorldDbContext>();
	int retries = 10;
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