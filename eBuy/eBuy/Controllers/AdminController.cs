using DataTablesParser;
using eBuy.Data;
using eBuy.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ManageProducts()
        {
            return View();
        }

        #region API CALLS
        public IActionResult GetAll()
        {
            var query = from p in _context.Products select p;
            var parser = new Parser<Products>(Request.Form, query);
            return Json(parser.Parse());
        }
        #endregion
    }
}
