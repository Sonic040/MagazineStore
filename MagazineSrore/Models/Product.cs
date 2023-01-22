using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MagazineSrore.Models
{
    public partial class Product
    {
        public Product()
        {
            Sale2s = new HashSet<Sale2>();
        }

        public decimal Pid { get; set; }
        public string Pname { get; set; }
        public decimal Price { get; set; }
        public string Imagepath { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public decimal Userid { get; set; }
        public decimal Cid { get; set; }

        public virtual Category CidNavigation { get; set; }
        public virtual User1 User { get; set; }
        public virtual ICollection<Sale2> Sale2s { get; set; }
    }
}
