using System;
using System.Collections.Generic;

#nullable disable

namespace MagazineSrore.Models
{
    public partial class Role
    {
        public Role()
        {
            User1s = new HashSet<User1>();
        }

        public decimal Roleid { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<User1> User1s { get; set; }
    }
}
