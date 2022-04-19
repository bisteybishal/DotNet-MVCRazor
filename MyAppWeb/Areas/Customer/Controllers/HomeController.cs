

using Microsoft.AspNetCore.Mvc;
using MyApp.Models;
using System.Diagnostics;
using MyApp.DataAccessLayer.Infrastructure.IRepository;

using Microsoft.AspNetCore.Authorization;
using MyAppModels.ViewModels;
using MyAppModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace MyAppWeb.Controllers
{
    [Area("Customer")]
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitofWork _unitofwork;
        public HomeController(ILogger<HomeController> logger, IUnitofWork unitofwork, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _unitofwork = unitofwork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
           
            IEnumerable<Product> products = _unitofwork.Product.GetAll();
            return View(products);
        }
        //http get for Detail 
        [HttpGet]
        public IActionResult Details(int? ProductId)
        {
            Cart cart=new Cart
            {
              Product = _unitofwork.Product.GetT(x=>x.Id== ProductId),
                    Count = 1,
                    ProductId= (int)ProductId
            };
            return View(cart);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(Cart cart)
        {
            if (ModelState.IsValid)
            {
                cart.ApplicationUserId = _userManager.GetUserId(User);
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //var ApplicationUserId = claims.Value;
            var cartfromTable = _unitofwork.Cart.GetT(x => x.ApplicationUserId==cart.ApplicationUserId  && x.ProductId==cart.ProductId);

                if (cartfromTable != null)
                {

                    cart.Count += cartfromTable.Count;
                    //_unitofwork.Cart.Update(cart);

                }
                else
                {
                    _unitofwork.Cart.Add(cart);
                }

                
                _unitofwork.Save();
            }
                               
              
            return RedirectToAction("Index");

        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}