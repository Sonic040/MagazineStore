using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineSrore.Models
{
    public class JoinTable
    {
        public Product product { get; set; }
        public Category category { get; set; }
    }
}
