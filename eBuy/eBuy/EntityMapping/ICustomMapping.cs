using eBuy.Data;
using eBuy.Models;
using eBuy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.EntityMapping
{
    public interface ICustomMapping
    {
        ProductsViewModel OutMap(Products products, ProductsViewModel productsViewModel);
        HomeViewProducts OutMap(Products products, ProductImages productImage, HomeViewProducts viewProducts);
        CartProducts OutMap(Cart cart, Products product, CartProducts cartProduct);
        UserProductsViewModels OutMap(Products product, ApplicationUser user, UserProductsViewModels viewModel);
        EditProductsViewModel OutMap(Products products, ProductImages image, EditProductsViewModel productsViewModel);
    }
}
