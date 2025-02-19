using VMCategory = Demo.Entity.ViewModel.Category;
using VMDeleteModal = Demo.Entity.ViewModel.DeleteModal;
using VMCategoryPagination = Demo.Entity.ViewModel.CategoryPagination;
using Demo.Entity.Data;
using Demo.Service.Interfaces;
using Demo.Repository.Interfaces;
using Demo.Entity.Helpers;
namespace Demo.Service.Implementations;

public class CategoryService(ICategoryRepository repository) : ICategoryService
{
    private readonly ICategoryRepository _repository = repository;
    public IEnumerable<VMCategory> GetAll()
    {
        IEnumerable<Category> categoryDb = _repository.GetAll();
        return categoryDb
        .Select(item => new VMCategory()
        {
            CategoryId = item.Categoryid,
            CategoryName = item.Categoryname,
            CategoryDescription = item.Description,

        });
    }
    public VMCategoryPagination GetPagedRecords(int pageSize, int pageNumber)
    {
        VMCategoryPagination model = new()
        {
            Page = new()
        };
        var productDb = _repository.GetPagedRecords(pageSize, pageNumber);
        model.Categories = productDb.categories.
            Select(item => new VMCategory()
            {
                CategoryId = item.Categoryid,
                CategoryName = item.Categoryname,
                CategoryDescription = item.Description
            }
            );
        model.Page.SetPagination(productDb.totalRecords, pageSize, pageNumber);
        return model;
    }
    public async Task AddAsync(VMCategory category)
    {
        Category categoryDb = new()
        {
            Categoryname = category.CategoryName,
            Categoryid = category.CategoryId,
            Description = category.CategoryDescription
        };
        await _repository.AddAsync(categoryDb);

    }
    public async Task<VMCategory> GetOneByIdAsync(int id, string operationType)
    {
        Category? categoryDb = await _repository.GetOneByIdAsync(id);
        VMCategory category = new()
        {
            OperationType = operationType
        };
        if (operationType != Constants.ADD)
        {
            category.CategoryId = categoryDb!.Categoryid;
            category.CategoryName = categoryDb.Categoryname;
            category.CategoryDescription = categoryDb.Description;
        }
        return category;
    }
    public async Task UpdateAsync(VMCategory category)
    {
        Category? categoryDb = await _repository.GetOneByIdAsync(category.CategoryId);
        categoryDb!.Categoryname = category.CategoryName;
        categoryDb.Categoryid = category.CategoryId;
        categoryDb.Description = category.CategoryDescription;
        await _repository.UpdateAsync(categoryDb);

    }
    public async Task DeleteAsync(int id)
    {
        if (id != 0)
            await _repository.DeleteAsync(id);
    }

    public VMDeleteModal GetAssociatedProductCount(int id, string requestType)
    {
        return new VMDeleteModal
        {
            Id = id,
            Type = requestType,
            ProductCount = _repository.GetAssociatedProductCount(id)
        };
    }

}
