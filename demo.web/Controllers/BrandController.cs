using Microsoft.AspNetCore.Mvc;
using Demo.Entity.ViewModel;
using Demo.Service.Interfaces;
using Demo.Entity.Helpers;

namespace Demo.Web.Controllers;

public class BrandController(IBrandService service) : Controller
{
    private readonly IBrandService _service = service;

    public IActionResult Index()
    {
        return View(new BrandPagination()
        {
            Brands = [],
            Page = new()
        });
    }
    public ActionResult GetBrandList(int pagesize, int pagenumber = 1)
    {
        return PartialView("_BrandPartialView", _service.GetPagedRecords(pagesize, pagenumber));
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
        return View(new Brand());
    }
    public async Task<IActionResult> Edit(int id)
    {
        return View(await _service.GetOneByIdAsync(id, Constants.EDIT));
    }
    [HttpPost]
    public async Task<IActionResult> Add(Brand brand)
    {
        if (ModelState.IsValid)
        {

            await _service.AddAsync(brand);
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Brand brand)
    {
        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(brand);
            return RedirectToAction("Details", new { id = brand.BrandId });
        }
        return RedirectToAction("Index");
    }
    public IActionResult ShowDeleteModal(int id, string requestType)
    {
        return PartialView("_DeleteModal", _service.GetCount(id, requestType));
    }
    public IActionResult GetAll()
    {
        return Json(_service.GetAll());
    }
    public async Task<IActionResult> GetBrand(int id, string operationType)
    {
        var data = await _service.GetOneByIdAsync(id, operationType);
        return Ok(new { message = data.BrandName });
    }
}
