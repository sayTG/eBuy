using eBuy.Data;
using eBuy.Models;
using eBuy.ViewModels;
using System;

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
        public CartProducts OutMap(Cart cart, Products product, CartProducts cartProduct)
        {
            cartProduct.Id = cart.Id;
            cartProduct.UnitPrice = product.UnitPrice;
            cartProduct.Quantity = cart.Quantity;
            cartProduct.Name = product.Name;
            return cartProduct;
        }
        public UserProductsViewModels OutMap(Products product, ApplicationUser user, UserProductsViewModels viewModel)
        {
            viewModel.ProductName = product.Name;
            viewModel.UnitPrice = product.UnitPrice;
            viewModel.Quantity = product.Quantity;
            viewModel.Description = product.Description;
            viewModel.FirstName = user.FirstName;
            viewModel.LastName = user.LastName;
            viewModel.Email = user.Email;
            viewModel.UserName = user.UserName;
            viewModel.Phone = user.PhoneNumber;
            return viewModel;
        }
    }
}
