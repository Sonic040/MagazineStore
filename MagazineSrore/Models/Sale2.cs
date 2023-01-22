using System;
using System.Collections.Generic;

#nullable disable

namespace MagazineSrore.Models
{
    public partial class Sale2
    {
        public decimal Sid { get; set; }
        public DateTime Datesold { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal Pid { get; set; }
        public decimal Userid { get; set; }

        public virtual Product PidNavigation { get; set; }
        public virtual User1 User { get; set; }
    }
}
