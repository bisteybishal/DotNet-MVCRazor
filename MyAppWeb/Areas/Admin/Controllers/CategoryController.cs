using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyAppModels.ViewModels;

namespace MyAppWeb.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private IUnitofWork _unitofWork;
        public CategoryController(IUnitofWork unitofwork)
        {
            _unitofWork =unitofwork;
        }

        public IActionResult Index()
        {
            CategoryVM categoryVM = new CategoryVM();
            categoryVM.categories = _unitofWork.Category.GetAll();
            return View(categoryVM);
        }
        [HttpGet]
        public IActionResult CreateUpdate(int? Id)
        {
            CategoryVM vm = new CategoryVM();

            if (Id == 0 || Id == null)
            {
                return View(vm);
            }
            else
            {
                vm.category = _unitofWork.Category.GetT(x => x.Id == Id);
                if (vm.category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);

                }
               
            }
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.category.Id == 0)
                {
                    _unitofWork.Category.Add(vm.category);
                    TempData["success"] = "Data Created Sucessfully";
                }
                else
                {
                    _unitofWork.Category.Update(vm.category);
                    TempData["success"] = "Data Updated Sucessfully";
                }
                
                _unitofWork.Save();
                
                return RedirectToAction("Index");

            }
            return View();

        }
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == 0 || Id == null)
            {
                return NotFound();
            }
            var category = _unitofWork.Category.GetT(x => x.Id == Id);
            
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? Id)
        {
          var category = _unitofWork.Category.GetT(x => x.Id == Id);
            if (category == null)
            {
                return NotFound();

            }
          _unitofWork.Category.Delete(category);
           _unitofWork.Save();
            TempData["success"] = "Data Removed Sucessfully";
            return RedirectToAction("Index");

        }
        
    }
    //[HttpGet]
    //public IActionResult Create()
    //{
    //    return View();

    //}
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Create(Category category)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _unitofWork.Category.Add(category);
    //        _unitofWork.Save();
    //        TempData["success"] = "Data Created Sucessfully";
    //        return RedirectToAction("Index");

    //    }
    //    return View();

    //}
}
