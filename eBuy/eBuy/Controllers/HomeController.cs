using eBuy.Abstractions;
using eBuy.Data;
using eBuy.Models;
using eBuy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace eBuy.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHomeService _homeService;
        private readonly ICartService _cartService;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IHomeService homeService, ICartService cartService)
        {
            _logger = logger;
            _userManager = userManager;
            _homeService = homeService;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            HomeViewModel viewModel = _homeService.DisplayProducts(userId);
            return View(viewModel);
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
