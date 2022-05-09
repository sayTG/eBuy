using eBuy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Guid ProductId { get; set; }
        public Products Product { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int Deleted { get; set; }
    }
}
