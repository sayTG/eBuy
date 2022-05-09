using eBuy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.Abstractions
{
    public interface IHomeService
    {
        HomeViewModel DisplayProducts(string userId);
    }
}
