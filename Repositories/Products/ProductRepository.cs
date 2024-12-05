using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Products;

public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
{
    public async Task<List<Product>> GetTopPriceProductAsync(int count)
    {
        return await Context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
    }
}


