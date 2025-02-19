using Demo.Entity.ViewModel;
namespace Demo.Service.Interfaces;

public interface ICategoryService
{
    public IEnumerable<Category> GetAll();
    public CategoryPagination GetPagedRecords(int pageSize, int pageNumber);
    public Task AddAsync(Category category);
    public Task<Category> GetOneByIdAsync(int id, string operationType);
    public Task UpdateAsync(Category category);
    public Task DeleteAsync(int id);
    public DeleteModal GetAssociatedProductCount(int id, string requestType);

}
