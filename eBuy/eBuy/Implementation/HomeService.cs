using eBuy.Abstractions;
using eBuy.Data;
using eBuy.EntityMapping;
using eBuy.Models;
using eBuy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.Implementation
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICustomMapping _customMapping;
        public HomeService(ApplicationDbContext context, IProductService productService, ICustomMapping customMapping, ICartService cartService)
        {
            _context = context;
            _productService = productService;
            _customMapping = customMapping;
            _cartService = cartService;
        }
        //Display list products added to cart and Image
        public HomeViewModel DisplayProducts(string userId)
        {
            int count = 0;
            List<HomeViewProducts> viewProducts = new List<HomeViewProducts>();
            List<Products> products = _productService.GetAllProduct();
            foreach(Products product in products)
            {
                ProductImages image = _productService.GetProductImage(product.ProductId);
                HomeViewProducts viewProduct = _customMapping.OutMap(product, image, new HomeViewProducts());
                int  quan = _cartService.ActiveCartQuantity(userId, product.ProductId);
                viewProduct.QuantityAdded = quan == 0 ? 1 : quan;
                viewProducts.Add(viewProduct);
            }
            if (userId != null)
                count = _cartService.UserCartCount(userId);
            else
                count = 0;
            return new HomeViewModel { HomeViewProducts = viewProducts, CartCount = count };
        }
    }
}
