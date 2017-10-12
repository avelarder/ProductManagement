using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagementMVC.Models
{
    public class ProductModel
    {
        [Required]
        public string Number { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, 9999999)]
        public double Price { get; set; }
    }
}