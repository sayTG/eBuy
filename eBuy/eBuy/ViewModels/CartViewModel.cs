using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.ViewModels
{
    public class CartViewModel
    {
        public List<CartProducts> CartProducts { get; set; }
        public int CartCount { get; set; }
    }
    public class CartProducts
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
