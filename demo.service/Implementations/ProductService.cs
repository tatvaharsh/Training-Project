using VMProduct = Demo.Entity.ViewModel.Product;
using VMProductPagination = Demo.Entity.ViewModel.ProductPagination;
using Demo.Entity.Data;
using Demo.Service.Interfaces;
using Demo.Repository.Interfaces;
using Demo.Entity.Helpers;
namespace Demo.Service.Implementations;

public class ProductService(IProductRepository repository) : IProductService
{
    private readonly IProductRepository _repository = repository;

    public VMProductPagination GetPagedRecords(int pageSize, int pageNumber)
    {
        VMProductPagination model = new()
        {
            Page = new()
        };
        var productDb = _repository.GetPagedRecords(pageSize, pageNumber);
        model.Products = productDb.products.
            Select(item => new VMProduct()
            {
                ProductId = item.Productid,
                ProductName = item.Productname,
                ProductDescription = item.Description,
                ProductQauntity = item.Quantity,
                ProductPrice = item.Price,
                BrandId = item.Brandid,
                CategoryId = item.Categoryid
            }
            );
        model.Page.SetPagination(productDb.totalRecords, pageSize, pageNumber);
        return model;
    }
    public async Task AddAsync(VMProduct product)
    {
        Product productDb = new()
        {
            Productname = product.ProductName,
            Categoryid = product.CategoryId,
            Brandid = product.BrandId,
            Quantity = product.ProductQauntity,
            Price = product.ProductPrice,
            Description = product.ProductDescription
        };
        await _repository.AddAsync(productDb);

    }
    public async Task<VMProduct> GetOneByIdAsync(int id, string operationType)
    {
        Product? productDb = await _repository.GetOneByIdAsync(id);
        VMProduct product = new()
        {
            OperationType = operationType,
            BrandId = 0,
            CategoryId = 0
        };

        if (operationType != Constants.ADD)
        {
            product.ProductId = productDb!.Productid;
            product.ProductName = productDb.Productname;
            product.CategoryId = productDb.Categoryid;
            product.BrandId = productDb.Brandid;
            product.ProductQauntity = productDb.Quantity;
            product.ProductPrice = productDb.Price;
            product.ProductDescription = productDb.Description;
        }
        return product;
    }
    public async Task UpdateAsync(VMProduct product)
    {
        Product? productDb = await _repository.GetOneByIdAsync(product.ProductId);
        productDb!.Productname = product.ProductName;
        productDb.Categoryid = product.CategoryId;
        productDb.Brandid = product.BrandId;
        productDb.Quantity = product.ProductQauntity;
        productDb.Price = product.ProductPrice;
        productDb.Description = product.ProductDescription;
        await _repository.UpdateAsync(productDb);
    }
    public async Task DeleteAsync(int id)
    {
        if (id != 0)
            await _repository.DeleteAsync(id);
    }
}
