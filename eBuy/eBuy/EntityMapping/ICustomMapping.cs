﻿using eBuy.Models;
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
    }
}
