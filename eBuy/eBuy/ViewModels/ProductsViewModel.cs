using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eBuy.ViewModels
{
    public class ProductsViewModel
    {
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "Product Name cannot be empty")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Product Price cannot be empty")]
        public double UnitPrice { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [Required(ErrorMessage = "Please select an image")]
        public IFormFile File { get; set; }
    }
    public class EditProductsViewModel
    {
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "Product Name cannot be empty")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Product Price cannot be empty")]
        public double UnitPrice { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string File { get; set; }
        public IFormFile UpdatedFile { get; set; }
    }
}
