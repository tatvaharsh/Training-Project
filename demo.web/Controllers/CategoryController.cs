using Microsoft.AspNetCore.Mvc;
using Demo.Entity.ViewModel;
using Demo.Service.Interfaces;
using Demo.Entity.Helpers;

namespace Demo.Web.Controllers;

public class CategoryController(ICategoryService service) : Controller
{
    private readonly ICategoryService _service = service;

    public IActionResult Index()
    {
        return View(new CategoryPagination()
        {
            Categories = [],
            Page = new()
        });
    }
    public ActionResult GetCategoryList(int pagesize, int pagenumber = 1)
    {
        return PartialView("_CategoryPartialView", _service.GetPagedRecords(pagesize, pagenumber));
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
        return View(new Category());
    }
    public async Task<IActionResult> Edit(int id)
    {
        return View(await _service.GetOneByIdAsync(id, Constants.EDIT));
    }
    [HttpPost]
    public async Task<IActionResult> Add(Category category)
    {
        if (ModelState.IsValid)
        {
            await _service.AddAsync(category);
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(category);
            return RedirectToAction("Details", new { id = category.CategoryId });
        }
        return RedirectToAction("Index");
    }
    public IActionResult ShowDeleteModal(int id, string requestType)
    {
        return PartialView("_DeleteModal", _service.GetAssociatedProductCount(id, requestType));
    }
    public IActionResult GetAll()
    {
        return Json(_service.GetAll());
    }
    public async Task<IActionResult> GetCategory(int id, string operationType)
    {
        var data = await _service.GetOneByIdAsync(id, operationType);
        return Json(new { message = data.CategoryName });
    }

}
