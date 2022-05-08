using eBuy.Abstractions;
using eBuy.Data;
using eBuy.Models;
using eBuy.Utilities;
using eBuy.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace eBuy.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddNewProductAsync(ProductsViewModel productViewModel)
        {
            try
            {
                _context.Database.BeginTransaction();
               
                    Products product = new Products()
                    {
                        ProductId = Guid.NewGuid(),
                        Name = productViewModel.Name,
                        Description = productViewModel.Description,
                        IsEnabled = productViewModel.IsEnabled,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        UnitPrice = productViewModel.UnitPrice,
                        Quantity = productViewModel.Quantity <= 0 ? 1 : productViewModel.Quantity,
                    };
                    ProductImages image = await AddProductImage(productViewModel.File, product.ProductId);
                    await _context.Products.AddAsync(product);
                    await _context.AddAsync(image);
                    await _context.SaveChangesAsync();
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
        public async Task<ProductImages> AddProductImage(IFormFile formFile, Guid productId)
        {
            return new ProductImages()
            {
                ProductId = productId,
                Image = await formFile.FormFileGetBytes(),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
        }
    }
}
