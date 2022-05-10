using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.DTOs
{
    public class RemovedDTO
    {
        public int id { get; set; }
        public Guid productId { get; set; }
        public string userId { get; set; }
        public int quantity { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModified { get; set; }
    }
}
