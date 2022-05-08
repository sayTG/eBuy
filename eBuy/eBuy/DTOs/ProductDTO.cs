using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.DTOs
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public double unitPrice { get; set; }
        public bool isEnabled { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModified { get; set; }
    }
}
