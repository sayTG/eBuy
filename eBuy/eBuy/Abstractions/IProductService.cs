using eBuy.Models;
using eBuy.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.Abstractions
{
    public interface IProductService
    {
        Task<bool> AddNewProductAsync(ProductsViewModel productViewModel);
        Task<ProductImages> AddProductImage(IFormFile formFile, Guid productId);
        Products GetProduct(Guid? productId);
        ProductImages GetProductImage(Guid? productId);
        Task<bool> EditProduct(Guid productId, ProductsViewModel productViewModel);
        Task<bool> DeleteProduct(Guid productId);
        List<Products> GetAllProduct();
    }
}
