using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.ViewModels
{
    public class UserProductsViewModels
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
