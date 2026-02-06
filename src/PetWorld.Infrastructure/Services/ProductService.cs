using System.Text;
using PetWorld.Application.Interfaces;
using PetWorld.Domain.Entities;

namespace PetWorld.Infrastructure.Services;

public class ProductService : IProductService
{
	private readonly IProductRepository _productRepository;

	public ProductService(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	public async Task<IEnumerable<Product>> GetAllProductsAsync()
		=> await _productRepository.GetAllProductsAsync();

	public async Task<string> GetProductCatalogPromptAsync()
	{
		IEnumerable<Product> products = await _productRepository.GetAllProductsAsync();
		StringBuilder sb = new();
		sb.AppendLine("KATALOG PRODUKTOW PETWORLD:");
		sb.AppendLine();

		foreach (Product product in products)
			sb.AppendLine(
				$"- {product.Name} | Kategoria: {product.Category} | Cena: {product.Price} zl | {product.Description}");

		return sb.ToString();
	}
}