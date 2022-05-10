using DataTablesParser;
using eBuy.Abstractions;
using eBuy.Data;
using eBuy.DTOs;
using eBuy.EntityMapping;
using eBuy.Models;
using eBuy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly ICustomMapping _customMapping;
        private UserManager<ApplicationUser> _userManager;
        public AdminController(ApplicationDbContext context, IProductService productService, ICustomMapping customMapping, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _productService = productService;
            _customMapping = customMapping;
            _userManager = userManager;
        }
        //Admin Dashoard Index page
        public IActionResult Index()
        {
            return View();
        }
        //Admin page to manage products
        public IActionResult Manage()
        {
            return View();
        }
        //Admin page to view removed products
        public IActionResult Removed()
        {
            return View();
        }
        //Admin page to check the details of product
        public IActionResult Details(Guid productId, string userId)
        {
            UserProductsViewModels viewModel = _productService.ProductAndUser(productId, userId);
            return View(viewModel);
        }
        //Admin page to create product
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
        //Admin page to edit product
        public IActionResult Edit(Guid? productId)
        {
            if (productId == null)
                return View("~/Views/404.cshtml");
            Products product = _productService.GetProduct(productId);
            ProductImages image = _productService.GetProductImage(productId);
            EditProductsViewModel productsViewModel = _customMapping.OutMap(product, image, new EditProductsViewModel());
            if (product == null)
                return View("~/Views/404.cshtml");
            return View(productsViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid productId, EditProductsViewModel productsViewModel)
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
        //Admin page to delete product
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
        public IActionResult AllRemoved()
        {
            var query = from p in _context.Cart.Where(p => p.Id == p.Deleted)
                        select new RemovedDTO
                        {
                            id = p.Id,
                            productId = p.ProductId,
                            userId = p.UserId,
                            quantity = p.Quantity,
                            dateCreated = p.DateCreated,
                            dateModified = p.DateModified
                        };
            var parser = new Parser<RemovedDTO>(Request.Form, query);
            return Json(parser.Parse());
        }
        #endregion
    }
}
