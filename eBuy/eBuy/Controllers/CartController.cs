using eBuy.Abstractions;
using eBuy.Data;
using eBuy.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> AddToCart(Guid productId, int quantity)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            bool result = await _cartService.AddToCart(productId, quantity, userId);
            if (result)
            {
                HomeViewModel viewModel = _homeService.DisplayProducts(userId);
                return View("~/Views/Home/Index", viewModel);
            }
            else
                return View("~/Views/500.cshtml");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
