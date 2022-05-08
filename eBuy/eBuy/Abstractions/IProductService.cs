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
    }
}
