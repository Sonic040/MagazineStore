using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MagazineSrore.Models
{
    public partial class User1
    {
        public User1()
        {
            Products = new HashSet<Product>();
            Sale2s = new HashSet<Sale2>();
            Testimonials = new HashSet<Testimonial>();
        }

        public decimal Userid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Imagepath { get; set; }
       
        public decimal Roleid { get; set; }

        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Sale2> Sale2s { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
