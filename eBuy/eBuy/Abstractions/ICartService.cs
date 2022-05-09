using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.Abstractions
{
    public interface ICartService
    {
        int UserCartCount(string userId);
        Task<bool> AddToCart(Guid productId, int quantity, string userId);
        int ActiveCartQuantity(string userId, Guid productId);
    }
}
