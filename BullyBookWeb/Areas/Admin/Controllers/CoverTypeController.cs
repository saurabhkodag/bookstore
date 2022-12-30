using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using BullyBook.Data;
using Microsoft.AspNetCore.Mvc;

namespace BullyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]

public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);
    }



    //GET
    public IActionResult Create()
    {

        return View();
    }

    //Edit 

    public IActionResult Edit(int? id)
    {
        if (id == null) { return NotFound(); }
        var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
        if (CoverTypeFromDb == null) { return NotFound(); }


        return View(CoverTypeFromDb);

    }

    //Update
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category edited successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    //Delete 

    public IActionResult Delete(int? id)
    {
        if (id == null) { return NotFound(); }
        var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
        if (CoverTypeFromDb == null) { return NotFound(); }


        return View(CoverTypeFromDb);

    }

    //Update
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {   
        var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
        if (CoverTypeFromDb == null) { return NotFound(); }
        _unitOfWork.CoverType.Remove(CoverTypeFromDb);
        TempData["success"] = "CoverType deleted successfully";
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
}
