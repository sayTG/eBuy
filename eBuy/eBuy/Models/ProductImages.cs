using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.Models
{
    public class ProductImages
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Deleted { get; set; }
    }
}
