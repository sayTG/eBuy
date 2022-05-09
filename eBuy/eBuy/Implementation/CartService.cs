using eBuy.Abstractions;
using eBuy.Data;
using eBuy.EntityMapping;
using eBuy.Models;
using System;
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
        public int ActiveCartQuantity(string userId, Guid productId)
        {
            return _context.Cart.Where(x => x.Id != x.Deleted && x.UserId == userId && x.ProductId == productId).Select(x => x.Quantity).FirstOrDefault();
        }
        public async Task<bool> AddToCart(Guid productId, int quantity, string userId)
        {
            try
            {
                _context.Database.BeginTransaction();
                Cart existingProduct = _context.Cart.Where(x => x.ProductId == productId && x.UserId == userId).FirstOrDefault();
                if (existingProduct != null)
                {
                    existingProduct.Quantity = quantity;
                    existingProduct.DateModified = DateTime.Now;
                    _context.Update(existingProduct);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Cart newCart = new Cart()
                    {
                        ProductId = productId,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        Quantity = quantity,
                        UserId = userId
                    };
                    _context.Add(newCart);
                    await _context.SaveChangesAsync();
                }
                _context.Database.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                _context.Database.RollbackTransaction();
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
