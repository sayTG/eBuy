using eBuy.Abstractions;
using eBuy.Data;
using eBuy.Models;
using eBuy.Utilities;
using eBuy.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                await _context.AddAsync(product);
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
        public Products GetProduct(Guid? productId)
        {
            return _context.Products.Where(x => x.Id != x.Deleted && x.ProductId == productId).FirstOrDefault();
        }
        public ProductImages GetProductImage(Guid? productId)
        {
            return _context.ProductImages.Where(x => x.Id != x.Deleted && x.ProductId == productId).FirstOrDefault();
        }
        public async Task<bool> EditProduct(Guid productId, ProductsViewModel productViewModel)
        {
            try
            {
                _context.Database.BeginTransaction();
                Products product = GetProduct(productId);
                product.Name = productViewModel.Name;
                product.Description = productViewModel.Description;
                product.IsEnabled = productViewModel.IsEnabled;
                product.DateModified = productViewModel.DateModified;
                product.UnitPrice = productViewModel.UnitPrice;
                product.Quantity = productViewModel.Quantity <= 0 ? 1 : productViewModel.Quantity;

                if (productViewModel.File != null)
                {
                    ProductImages image = GetProductImage(productId);
                    image.Image = await productViewModel.File.FormFileGetBytes();
                    image.DateModified = DateTime.Now;
                    _context.Update(image);
                }
                _context.Update(product);
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
        public async Task<bool> DeleteProduct(Guid productId)
        {
            try
            {
                _context.Database.BeginTransaction();
                Products product = GetProduct(productId);
                if (product == null)
                    return false;
                product.Deleted = product.Id;
                product.DateModified = DateTime.Now;

                ProductImages image = GetProductImage(productId);
                if (image != null)
                {
                    image.Deleted = image.Id;
                    image.DateModified = DateTime.Now;
                    _context.Update(image);
                }
                _context.Update(product);
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
        public List<Products> GetAllProduct()
        {
            return _context.Products.Where(x => x.Id != x.Deleted).ToList();
        }
    }
}
