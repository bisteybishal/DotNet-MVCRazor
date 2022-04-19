using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyAppModels.ViewModels;

namespace MyAppWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
       
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitofWork _UnitofWork;

        public CartController(IUnitofWork unitofWork, UserManager<IdentityUser> userManager)
        {
            _UnitofWork = unitofWork;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            var userid = _userManager.GetUserId(User);
            var itemlist = _UnitofWork.Cart.GetAll(userid);
            CartVM vm = new CartVM();
            

            vm.Listofcart=itemlist;

            foreach (var cart in itemlist)
            {
                cart.Product = _UnitofWork.Product.GetT(x => x.Id == cart.ProductId);
                Cart.Total += cart.Product.Price * cart.Count;
            }

            return View(vm);
        }
        public IActionResult plus(int Id)
        {
            var cart = _UnitofWork.Cart.GetT(x => x.Id == Id);
            _UnitofWork.Cart.IncrementCartitem(cart, 1);
            _UnitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult minus(int Id)
        {
            var cart = _UnitofWork.Cart.GetT(x => x.Id == Id);
            
            if(cart.Count >= 0)
            {
                _UnitofWork.Cart.Delete(cart);
            }
            else 
            {
                _UnitofWork.Cart.DecrementCartitem(cart, 1);

            }
            _UnitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult delete(int Id)
        {
            var cart = _UnitofWork.Cart.GetT(x => x.Id == Id);
            _UnitofWork.Cart.Delete(cart);
            _UnitofWork.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
