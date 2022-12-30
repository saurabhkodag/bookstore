using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using BulkyBook.Model.ViewModels;
using BullyBook.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BullyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]

public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View();
    }




    //Edit 

    public IActionResult Upsert(int? id)
    {
        Company company = new();
  
        if (id == null||id==0) {
            return View(company); 
        }
        else
        {
            company = _unitOfWork.Company.GetFirstOrDefault(u=> u.Id == id);
            return View(company);
            
        }


    }

    //Update
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Company obj)
    {
        if (ModelState.IsValid)
        {
            if (obj.Id == 0)
            {
                _unitOfWork.Company.Add(obj);

                TempData["success"] = "Product created successfully";
            }
            else
            {
                _unitOfWork.Company.Update(obj);

                TempData["success"] = "Product updated successfully";
            }
            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
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

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var companyList= _unitOfWork.Company.GetAll();
        return Json(new { data = companyList });
    }
    //Update
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var obj = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
        if (obj == null) { 
            return Json(new {success=false,message="Error while deleting"}); 
        }
    
        _unitOfWork.Company.Remove(obj);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful" });
    }
    #endregion
}
