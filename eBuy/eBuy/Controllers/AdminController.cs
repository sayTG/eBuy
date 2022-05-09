using DataTablesParser;
using eBuy.Abstractions;
using eBuy.Data;
using eBuy.DTOs;
using eBuy.EntityMapping;
using eBuy.Models;
using eBuy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly ICustomMapping _customMapping;

        public AdminController(ApplicationDbContext context, IProductService productService, ICustomMapping customMapping)
        {
            _context = context;
            _productService = productService;
            _customMapping = customMapping;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Manage()
        {
            return View();
        }
        public IActionResult Create()
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
                    return RedirectToAction(nameof(Manage));
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
            return View("create", product);
        }
        public IActionResult Edit(Guid? productId)
        {
            if (productId == null)
                return View("~/Views/404.cshtml");
            Products product = _productService.GetProduct(productId);
            ProductsViewModel productsViewModel = _customMapping.OutMap(product, new ProductsViewModel());
            if (product == null)
                return View("~/Views/404.cshtml");
            return View(productsViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid productId, ProductsViewModel productsViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await _productService.EditProduct(productId, productsViewModel);
                if (result)
                    return RedirectToAction(nameof(Manage));
                else
                {
                    TempData["Success"] = false;
                    TempData["Msg"] = "An Error Occured";
                }
            }
            return View("edit", productsViewModel);
        }
        public async Task<IActionResult> Delete(Guid productId)
        {
            bool response = await _productService.DeleteProduct(productId);
            if (!response)
                return Json(new { success = false, message = "Error while deleting product" });

            return Json(new { success = true, message = "Product deleted successfully" });
        }

        #region API CALLS
        public IActionResult GetAll()
        {
            var query = from p in _context.Products.Where(p => p.Id != p.Deleted)
                        select new ProductDTO
                        {
                            id = p.Id,
                            productId = p.ProductId,
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
