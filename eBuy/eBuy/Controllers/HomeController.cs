using eBuy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Principal;
using eBuy.Data;
using Microsoft.AspNetCore.Identity;
using eBuy.Abstractions;
using eBuy.ViewModels;

namespace eBuy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHomeService _homeService;
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IHomeService homeService)
        {
            _logger = logger;
            _userManager = userManager;
            _homeService = homeService;
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
