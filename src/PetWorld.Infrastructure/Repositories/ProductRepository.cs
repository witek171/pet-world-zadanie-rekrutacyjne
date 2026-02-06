using Microsoft.EntityFrameworkCore;
using PetWorld.Application.Interfaces;
using PetWorld.Domain.Entities;
using PetWorld.Infrastructure.Data;

namespace PetWorld.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly PetWorldDbContext _context;

    public ProductRepository(PetWorldDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }
}
