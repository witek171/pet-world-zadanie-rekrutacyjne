using Microsoft.EntityFrameworkCore;
using PetWorld.Domain.Entities;

namespace PetWorld.Infrastructure.Data;

public class PetWorldDbContext : DbContext
{
	public PetWorldDbContext(DbContextOptions<PetWorldDbContext> options) : base(options)
	{
	}

	public DbSet<ChatConversation> ChatConversations { get; set; }
	public DbSet<Product> Products { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ChatConversation>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Id);
			entity.Property(e => e.Question).HasMaxLength(2000).IsRequired();
			entity.Property(e => e.Answer).HasMaxLength(4000).IsRequired();
			entity.Property(e => e.CreatedAt).IsRequired();
			entity.Property(e => e.IterationCount).IsRequired();
		});

		modelBuilder.Entity<Product>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Id);
			entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
			entity.Property(e => e.Category).HasMaxLength(100).IsRequired();
			entity.Property(e => e.Price).HasPrecision(10, 2).IsRequired();
			entity.Property(e => e.Description).HasMaxLength(500).IsRequired();
		});
	}
}