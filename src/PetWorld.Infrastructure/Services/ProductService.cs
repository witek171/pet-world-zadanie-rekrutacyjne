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
    {
        return await _productRepository.GetAllProductsAsync();
    }

    public async Task<string> GetProductCatalogPromptAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        var sb = new StringBuilder();
        sb.AppendLine("KATALOG PRODUKTOW PETWORLD:");
        sb.AppendLine();
        
        foreach (var product in products)
        {
            sb.AppendLine($"- {product.Name} | Kategoria: {product.Category} | Cena: {product.Price} zl | {product.Description}");
        }
        
        return sb.ToString();
    }
}
