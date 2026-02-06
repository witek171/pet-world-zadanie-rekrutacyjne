using PetWorld.Domain.Entities;

namespace PetWorld.Application.Interfaces;

public interface IProductService
{
	Task<IEnumerable<Product>> GetAllProductsAsync();
	Task<string> GetProductCatalogPromptAsync();
}