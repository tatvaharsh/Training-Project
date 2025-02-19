using Demo.Entity.Data;
namespace Demo.Repository.Interfaces;

public interface IBrandRepository
{
    IEnumerable<Brand> GetAll();
    (IEnumerable<Brand> brands, int totalRecords) GetPagedRecords(int pageSize, int pageNumber);
    Task AddAsync(Brand brand);
    Task<Brand?> GetOneByIdAsync(int id);
    Task UpdateAsync(Brand brand);
    Task DeleteAsync(int id);
    int GetAssociatedProductCount(int id);
    // Task DeleteAssociatedEntitiesAsync(int id);

}
