using eBuy.Abstractions;
using eBuy.Data;
using eBuy.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eBuy.Controllers
{
    public class CartController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IHomeService _homeService;
        private readonly ICartService _cartService;
        public CartController(UserManager<ApplicationUser> userManager, IHomeService homeService, ICartService cartService)
        {
            _userManager = userManager;
            _homeService = homeService;
            _cartService = cartService;
        }
        //Add product to Cart
        public async Task<IActionResult> AddToCart(Guid productId, int quantity)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            bool result = await _cartService.AddToCart(productId, quantity, userId);
            if (result)
            {
                HomeViewModel viewModel = _homeService.DisplayProducts(userId);
                return View("~/Views/Home/Index.cshtml", viewModel);
            }
            else
                return View("~/Views/500.cshtml");
        }
        //Cart Index page
        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            CartViewModel cartView = _cartService.ViewCart(userId);
            return View(cartView);
        }
        //Remove products from cart
        public async Task<IActionResult> Remove(int cartId)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            bool result = await _cartService.RemoveCartItem(cartId, userId);
            if (result)
            {
                CartViewModel cartView = _cartService.ViewCart(userId);
                return View("index", cartView);
            }
            else
                return View("~/Views/500.cshtml");
        }
    }
}
