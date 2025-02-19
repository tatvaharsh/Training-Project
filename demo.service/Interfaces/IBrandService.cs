using Demo.Entity.ViewModel;
namespace Demo.Service.Interfaces;

public interface IBrandService
{
    public IEnumerable<Brand> GetAll();
    public BrandPagination GetPagedRecords(int pageSize, int pageNumber);
    public Task AddAsync(Brand brand);
    public Task<Brand> GetOneByIdAsync(int id, string operationType);
    public Task UpdateAsync(Brand brand);
    public Task DeleteAsync(int id);
    public DeleteModal GetCount(int id, string requestType);

}
