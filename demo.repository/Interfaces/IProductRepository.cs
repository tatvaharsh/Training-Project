using Demo.Entity.Data;
namespace Demo.Repository.Interfaces;

public interface IProductRepository
{
    (IEnumerable<Product> products, int totalRecords) GetPagedRecords(int pageSize, int pageNumber);
    Task AddAsync(Product product);
    Task<Product?> GetOneByIdAsync(int id);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}
