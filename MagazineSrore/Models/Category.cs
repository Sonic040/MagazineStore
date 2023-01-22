using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MagazineSrore.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public decimal Cid { get; set; }
        public string Cname { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
