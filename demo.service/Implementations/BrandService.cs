using VMBrand = Demo.Entity.ViewModel.Brand;
using VMDeleteModal = Demo.Entity.ViewModel.DeleteModal;
using VMBrandPagination = Demo.Entity.ViewModel.BrandPagination;
using Demo.Entity.Data;
using Demo.Service.Interfaces;
using Demo.Repository.Interfaces;
using Demo.Entity.Helpers;
namespace Demo.Service.Implementations;

public class BrandService(IBrandRepository repository) : IBrandService
{
    private readonly IBrandRepository _repository = repository;
    public IEnumerable<VMBrand> GetAll()
    {
        IEnumerable<Brand> brandDb = _repository.GetAll();
        return brandDb
       .Select(item => new VMBrand()
       {
           BrandId = item.Brandid,
           BrandName = item.Brandname,
           BrandDescription = item.Description,

       });
    }
    public VMBrandPagination GetPagedRecords(int pageSize, int pageNumber)
    {
        VMBrandPagination model = new()
        {
            Page = new()
        };
        var productDb = _repository.GetPagedRecords(pageSize, pageNumber);
        model.Brands = productDb.brands.
            Select(item => new VMBrand()
            {
                BrandId = item.Brandid,
                BrandName = item.Brandname,
                BrandDescription = item.Description,

            });
        model.Page.SetPagination(productDb.totalRecords, pageSize, pageNumber);
        return model;
    }
    public async Task AddAsync(VMBrand brand)
    {
        Brand brandDb = new()
        {
            Brandname = brand.BrandName,
            Brandid = brand.BrandId,
            Description = brand.BrandDescription
        };
        await _repository.AddAsync(brandDb);

    }
    public async Task<VMBrand> GetOneByIdAsync(int id, string operationType)
    {
        Brand? brandDb = await _repository.GetOneByIdAsync(id);
        VMBrand brand = new()
        {
            OperationType = operationType
        };
        if (operationType != Constants.ADD)
        {
            brand.BrandId = brandDb!.Brandid;
            brand.BrandName = brandDb.Brandname;
            brand.BrandDescription = brandDb.Description;
        }
        return brand;
    }
    public async Task UpdateAsync(VMBrand brand)
    {
        Brand? brandDb = await _repository.GetOneByIdAsync(brand.BrandId);
        brandDb!.Brandname = brand.BrandName;
        brandDb.Brandid = brand.BrandId;
        brandDb.Description = brand.BrandDescription;
        await _repository.UpdateAsync(brandDb);

    }
    public async Task DeleteAsync(int id)
    {
        if (id != 0)
            await _repository.DeleteAsync(id);
    }
    public VMDeleteModal GetCount(int id, string requestType)
    {
        return new VMDeleteModal
        {
            Id = id,
            Type = requestType,
            ProductCount = _repository.GetAssociatedProductCount(id)
        };
    }

}
