using eBuy.Models;
using eBuy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.EntityMapping
{
    public class CustomMapping : ICustomMapping
    {
        public ProductsViewModel OutMap(Products products, ProductsViewModel productsViewModel)
        {
            productsViewModel.ProductId = products.ProductId;
            productsViewModel.DateCreated = products.DateCreated;
            productsViewModel.DateModified = products.DateModified;
            productsViewModel.UnitPrice = products.UnitPrice;
            productsViewModel.Quantity = products.Quantity;
            productsViewModel.Description = products.Description;
            productsViewModel.Name = products.Name;
            productsViewModel.IsEnabled = products.IsEnabled;
            return productsViewModel;
        }
        public HomeViewProducts OutMap(Products products, ProductImages productImage, HomeViewProducts viewProducts)
        {
            viewProducts.ProductId = products.ProductId;
            viewProducts.UnitPrice = products.UnitPrice;
            viewProducts.Quantity = products.Quantity;
            viewProducts.Description = products.Description;
            viewProducts.Name = products.Name;
            viewProducts.File = "data:image/png;base64," + Convert.ToBase64String(productImage.Image);
            return viewProducts;
        }
    }
}
