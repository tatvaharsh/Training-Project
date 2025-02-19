using Demo.Entity.Data;
namespace Demo.Repository.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();
    (IEnumerable<Category> categories, int totalRecords) GetPagedRecords(int pageSize, int pageNumber);
    Task AddAsync(Category category);
    Task<Category?> GetOneByIdAsync(int id);
    Task UpdateAsync(Category category);
    Task DeleteAsync(int id);
    int GetAssociatedProductCount(int id);
}
