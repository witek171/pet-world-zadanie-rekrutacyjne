namespace PetWorld.Domain.Entities;

public class Product
{
	public Product(string name, string category, decimal price, string description)
	{
		Name = name;
		Category = category;
		Price = price;
		Description = description;
	}

	public Guid Id { get; } = Guid.NewGuid();
	public string Name { get; }
	public string Category { get; }
	public decimal Price { get; }
	public string Description { get; }
}