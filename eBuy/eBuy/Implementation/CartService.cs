using eBuy.Abstractions;
using eBuy.Data;
using eBuy.EntityMapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.Implementation
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly ICustomMapping _customMapping;

        public CartService(ApplicationDbContext context, IProductService productService, ICustomMapping customMapping)
        {
            _context = context;
            _productService = productService;
            _customMapping = customMapping;
        }
        public int UserCartCount(string userId)
        {
            return _context.Cart.Where(x => x.Id != x.Deleted && x.UserId == userId).Count();
        }
    }
}
