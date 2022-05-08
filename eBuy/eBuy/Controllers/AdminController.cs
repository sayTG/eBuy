using DataTablesParser;
using eBuy.Abstractions;
using eBuy.Data;
using eBuy.DTOs;
using eBuy.Models;
using eBuy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;

        public AdminController(ApplicationDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ManageProducts()
        {
            return View();
        }
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductsViewModel product)
        {
            if (ModelState.IsValid)
            {
                bool result = await _productService.AddNewProductAsync(product);
                if (result)
                {
                    TempData["Success"] = true;
                    TempData["Msg"] = "Successfully uploaded";
                    return RedirectToAction(nameof(ManageProducts));
                }
                else
                {
                    TempData["Success"] = false;
                    TempData["Msg"] = "An Error Occured";
                }
            }
            else
            {
                TempData["Success"] = false;
                TempData["Msg"] = "Model State not valid";
            }
            return View("createproduct", product);
        }
        #region API CALLS
        public IActionResult GetAll()
        {
            var query = from p in _context.Products.Where(p => p.Id != p.Deleted)
                        select new ProductDTO
                        {
                            id = p.Id,
                            name = p.Name,
                            description = p.Description,
                            quantity = p.Quantity,
                            unitPrice = p.UnitPrice,
                            isEnabled = p.IsEnabled,
                            dateCreated = p.DateCreated,
                            dateModified = p.DateModified
                        };
            var parser = new Parser<ProductDTO>(Request.Form, query);
            return Json(parser.Parse());
        }
        #endregion
    }
}
