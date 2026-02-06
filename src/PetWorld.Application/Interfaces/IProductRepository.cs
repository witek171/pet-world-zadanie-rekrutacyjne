using PetWorld.Domain.Entities;

namespace PetWorld.Application.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
}
