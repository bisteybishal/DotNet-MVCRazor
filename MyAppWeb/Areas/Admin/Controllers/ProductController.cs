using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyAppModels.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;


namespace MyAppWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitofWork _unitofwork;

        private IWebHostEnvironment _hostingEnvironment;
        public ProductController(IUnitofWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofwork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
        }
        #region APICALL

        public IActionResult AllProducts()
        {
            var products = _unitofwork.Product.GetAll();
            foreach(var product in products)
                {

               product.category = _unitofwork.Category.GetT(x => x.Id == product.CategoryId);
            }
           
            return Json(new { data = products });
        }
        #endregion
        public IActionResult Index()
        {
            ProductVM productVM = new ProductVM();
            productVM.Products = _unitofwork.Product.GetAll();
            return View(productVM);
        }
        [HttpGet]
        public IActionResult CreateUpdate(int? Id)
        {
            ProductVM vm = new ProductVM()
            {
                Product = new(),
                Categories = _unitofwork.Category.GetAll().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            if (Id == null || Id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Product = _unitofwork.Product.GetT(x => x.Id == Id);
                if (vm.Product == null)
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
        public IActionResult CreateUpdate(ProductVM vm)
        {
            

            if (ModelState.IsValid)
            {
                String fileName = String.Empty;
                if (vm.Photo != null)
                {

                    String uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "ProductImage");
                    fileName = Guid.NewGuid().ToString() + "-" + vm.Photo.FileName;
                    String filePath = Path.Combine(uploadDir, fileName);


                    //if (vm.Product.imageurl != null)
                    //{
                    //    var OldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, vm.Product.imageurl.TrimStart('\\'));
                    //    if (System.IO.File.Exists(OldImagePath))
                    //    {

                    //        System.IO.File.Delete(OldImagePath);

                    //    }
                    //}
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                     vm.Photo.CopyTo(fileStream);
                    }
                    vm.Product.imageurl = @"\ProductImage\" + fileName;
                }

                if (vm.Product.Id == 0)
                {
                    _unitofwork.Product.Add(vm.Product);
                    TempData["success"] = "Created Sucessfully";
                }
                else
                {
                    _unitofwork.Product.Update(vm.Product);
                    TempData["success"] = "Updated Successfully";
                }
                _unitofwork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var product = _unitofwork.Product.GetT(x => x.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}
        #region DeleteAPICALL
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var product = _unitofwork.Product.GetT(x => x.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Try again--- Something went wrong!!!!!!!!!!!!!" });
            }
            else
            {
                var OldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, product.imageurl.TrimStart('\\'));
                if (System.IO.File.Exists(OldImagePath))
                {

                    System.IO.File.Delete(OldImagePath);

                }
                _unitofwork.Product.Delete(product);
                _unitofwork.Save();
                return Json(new { success = true, message = "Sucessfully Deleted" });
            }           
           
            
        }
        #endregion
    }

}
