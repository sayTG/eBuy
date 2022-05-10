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
        //Get the cart count
        public int UserCartCount(string userId)
        {
            return _context.Cart.Where(x => x.Id != x.Deleted && x.UserId == userId).Count();
        }
        //Get the quantity of a particular product added to cart
        public int ActiveCartQuantity(string userId, Guid productId)
        {
            return _context.Cart.Where(x => x.Id != x.Deleted && x.UserId == userId && x.ProductId == productId).Select(x => x.Quantity).FirstOrDefault();
        }
        //Add product to cart
        public async Task<bool> AddToCart(Guid productId, int quantity, string userId)
        {
            try
            {
                _context.Database.BeginTransaction();
                Cart existingProduct = _context.Cart.Where(x => x.ProductId == productId && x.UserId == userId && x.Id != x.Deleted).FirstOrDefault();
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
        //Get all products added to the cart
        public List<Cart> AllUserCart(string userId)
        {
            return _context.Cart.Where(x => x.Id != x.Deleted && x.UserId == userId).ToList();
        }
        //View all products added to cart
        public CartViewModel ViewCart(string userId)
        {
            List<CartProducts> cartProducts = new List<CartProducts>();
            List<Cart> carts = AllUserCart(userId);
            foreach(Cart cart in carts)
            {
                Products product = _productService.GetProduct(cart.ProductId);
                cartProducts.Add(_customMapping.OutMap(cart, product, new CartProducts()));
            }
            int count = UserCartCount(userId);
            return new CartViewModel { CartProducts = cartProducts, CartCount = count };
        }
        //Remove product from cart
        public async Task<bool> RemoveCartItem(int cartId, string userId)
        {
            try
            {
                Cart existingCart = _context.Cart.Where(x => x.Id == cartId && x.UserId == userId && x.Id != x.Deleted).FirstOrDefault();
                if (existingCart != null)
                {
                    _context.Database.BeginTransaction();
                    existingCart.Deleted = cartId;
                    existingCart.DateModified = DateTime.Now;
                    _context.Update(existingCart);
                    await _context.SaveChangesAsync();
                    _context.Database.CommitTransaction();
                    return true;
                }
                return false;
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
