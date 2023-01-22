using System;
using System.Collections.Generic;

#nullable disable

namespace MagazineSrore.Models
{
    public partial class Testimonial
    {
        public decimal Tid { get; set; }
        public string Note { get; set; }
        public string Rating { get; set; }
        public decimal Userid { get; set; }

        public virtual User1 User { get; set; }
    }
}
