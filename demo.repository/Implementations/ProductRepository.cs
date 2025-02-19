using Demo.Entity.Data;
using Demo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repository.Implementations;

public class ProductRepository(ApplicationContext context) : IProductRepository
{
    private readonly ApplicationContext _context = context;

    public (IEnumerable<Product> products, int totalRecords) GetPagedRecords(int pageSize, int pageNumber)
    {
        IQueryable<Product> query = _context.Products;
        return (query
            .OrderBy(p => p.Productid)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize), query.Count());
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product?> GetOneByIdAsync(int id)
    {
        return await _context.Products.SingleOrDefaultAsync(m => m.Productid == id);
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.SingleOrDefaultAsync(m => m.Productid == id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
