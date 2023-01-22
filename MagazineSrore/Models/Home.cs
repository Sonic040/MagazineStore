using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MagazineSrore.Models
{
    public partial class Home
    {
        public decimal Hid { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
    }
}
