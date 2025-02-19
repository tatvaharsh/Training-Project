using Microsoft.AspNetCore.Mvc;
using Demo.Entity.ViewModel;
using Demo.Service.Interfaces;
using Demo.Entity.Helpers;

namespace Demo.Web.Controllers;

public class ProductController(IProductService service) : Controller
{
    private readonly IProductService _service = service;
    public IActionResult Index()
    {
        return View(new ProductPagination()
        {
            Products = [],
            Page = new()
        });
    }
    public ActionResult GetProductList(int pagesize = 3, int pagenumber = 1)
    {
        return PartialView("_ProductPartialView", _service.GetPagedRecords(pagesize, pagenumber));
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        return View(await _service.GetOneByIdAsync(id, Constants.DELETE));
    }
    public async Task<IActionResult> Details(int id)
    {
        return View(await _service.GetOneByIdAsync(id, Constants.DETAIL));
    }
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction("Index");
    }
    public IActionResult Add()
    {
        return View(new Product());
    }
    public async Task<IActionResult> Edit(int id)
    {
        return View(await _service.GetOneByIdAsync(id, Constants.EDIT));
    }
    [HttpPost]
    public async Task<IActionResult> Add(Product product)
    {
        if (ModelState.IsValid)
        {
            await _service.AddAsync(product);
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(product);
            return RedirectToAction("Details", new { id = product.ProductId });
        }
        return RedirectToAction("Index");
    }

    public IActionResult ShowDeleteModal(int id, string requestType)
    {
        return PartialView("_DeleteModal", new DeleteModal()
        {
            Id = id,
            Type = requestType,
            ProductCount = 0
        });
    }
}
