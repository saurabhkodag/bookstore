using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using BullyBook.Data;
using Microsoft.AspNetCore.Mvc;

namespace BullyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]

public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
        return View(objCategoryList);
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
        var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
        if (categoryFromDb == null) { return NotFound(); }


        return View(categoryFromDb);

    }

    //Update
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("CustomError", "The DisplayOrder cannot excatly match the name");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(obj);
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
        var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
        if (categoryFromDb == null) { return NotFound(); }


        return View(categoryFromDb);

    }

    //Update
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        if (id == null) { return NotFound(); }
        var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
        if (categoryFromDb == null) { return NotFound(); }
        _unitOfWork.Category.Remove(categoryFromDb);
        TempData["success"] = "Category deleted successfully";
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("CustomError", "The DisplayOrder cannot excatly match the name");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }
}
