using Demo.Entity.Data;
using Demo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repository.Implementations;

public class CategoryRepository(ApplicationContext context) : ICategoryRepository
{
    private readonly ApplicationContext _context = context;

    public IEnumerable<Category> GetAll() => _context.Categories;
    public (IEnumerable<Category> categories, int totalRecords) GetPagedRecords(int pageSize, int pageNumber)
    {
        IQueryable<Category> query = _context.Categories;
        return (query
            .OrderBy(p => p.Categoryid)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize), query.Count());
    }
    public async Task AddAsync(Category Category)
    {
        await _context.Categories.AddAsync(Category);
        await _context.SaveChangesAsync();
    }

    public async Task<Category?> GetOneByIdAsync(int id)
    {
        return await _context.Categories.SingleOrDefaultAsync(m => m.Categoryid == id);
    }

    public async Task UpdateAsync(Category Category)
    {
        _context.Categories.Update(Category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var Category = await _context.Categories.SingleOrDefaultAsync(m => m.Categoryid == id);
        var productsToRemove = _context.Products.Where(p => p.Categoryid == id).ToList();
        if (productsToRemove.Count != 0)
        {
            _context.Products.RemoveRange(productsToRemove);
            await _context.SaveChangesAsync();
        }
        if (Category != null)
        {
            _context.Categories.Remove(Category);
            await _context.SaveChangesAsync();
        }
    }

    public int GetAssociatedProductCount(int id)
    {
        return _context.Products.Where(m => m.Categoryid == id).Count();
    }

}
