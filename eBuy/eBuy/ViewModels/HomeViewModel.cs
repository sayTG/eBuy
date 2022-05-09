using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.ViewModels
{
    public class HomeViewModel
    {
        public List<HomeViewProducts> HomeViewProducts { get; set; }
        public int CartCount { get; set; }

    }
    public class HomeViewProducts
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityAdded { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string File { get; set; }
    }
}
