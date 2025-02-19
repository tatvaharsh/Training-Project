using Demo.Entity.Data;
using Demo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repository.Implementations;

public class BrandRepository(ApplicationContext context) : IBrandRepository
{
    private readonly ApplicationContext _context = context;


    public IEnumerable<Brand> GetAll() => _context.Brands;

    public (IEnumerable<Brand> brands, int totalRecords) GetPagedRecords(int pageSize, int pageNumber)
    {
        IQueryable<Brand> query = _context.Brands;
        return (query
            .OrderBy(p => p.Brandid)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList(), query.Count());
    }
    public async Task AddAsync(Brand Brand)
    {
        await _context.Brands.AddAsync(Brand);
        await _context.SaveChangesAsync();
    }

    public async Task<Brand?> GetOneByIdAsync(int id)
    {
        return await _context.Brands.SingleOrDefaultAsync(m => m.Brandid == id);
    }

    public async Task UpdateAsync(Brand Brand)
    {
        _context.Brands.Update(Brand);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var brand = await _context.Brands.SingleOrDefaultAsync(m => m.Brandid == id);
        var productsToRemove = _context.Products.Where(p => p.Brandid == id).ToList();
        if (productsToRemove.Count != 0)
        {
            _context.Products.RemoveRange(productsToRemove);
            await _context.SaveChangesAsync();
        }
        if (brand != null)
        {
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }

    }

    public int GetAssociatedProductCount(int id)
    {
        return _context.Products.Where(m => m.Brandid == id).Count();
    }
}
