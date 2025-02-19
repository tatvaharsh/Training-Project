using Demo.Entity.ViewModel;
namespace Demo.Service.Interfaces;

public interface IProductService
{
    public ProductPagination GetPagedRecords(int pageSize, int pageNumber);
    public Task AddAsync(Product product);
    public Task<Product> GetOneByIdAsync(int id, string ope);
    public Task UpdateAsync(Product product);
    public Task DeleteAsync(int id);
}
