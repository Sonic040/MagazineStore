using MagazineSrore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineSrore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var users = _context.User1s.Where(x => x.Roleid == 1).Count();
            ViewBag.users = users;
            var cats = _context.Categories.Count();
            ViewBag.cats = cats;
            var products = _context.Products.Count();
            ViewBag.products = products;
            var users2 = _context.User1s.Where(x => x.Roleid == 1).ToList();
            var product = _context.Products.ToList();
            var cat = _context.Sale2s.ToList();
            var all = Tuple.Create<IEnumerable<MagazineSrore.Models.User1>, IEnumerable<MagazineSrore.Models.Product>, IEnumerable<MagazineSrore.Models.Sale2>>(users2,product,cat);
            return View(all);
        }
        public async Task<IActionResult> tables(DateTime? startDate, DateTime? endDate)
        {
            //_context.Products.ToListAsync();
            var result = _context.Sale2s;
            if (startDate == null && endDate == null)

            {
                var res = await result.ToListAsync();
                var xyz = _context.User1s.ToList();
                var xyz1 = _context.Products.ToList();
                var all = Tuple.Create<IEnumerable<MagazineSrore.Models.User1>, IEnumerable<MagazineSrore.Models.Product>, IEnumerable<MagazineSrore.Models.Sale2>>(xyz, xyz1, res);

                return View(all);
            }
            else if (startDate == null && endDate != null)
            {
                var xyz = _context.User1s.ToList();
                var xyz1 = _context.Products.ToList();
                var res = await result.Where(x => x.Datesold == endDate).ToListAsync();
                var all = Tuple.Create<IEnumerable<MagazineSrore.Models.User1>, IEnumerable<MagazineSrore.Models.Product>, IEnumerable<MagazineSrore.Models.Sale2>>(xyz, xyz1, res);
                return View(all);

            }
            else if (startDate != null && endDate == null)
            {
                var xyz = _context.User1s.ToList();
                var xyz1 = _context.Products.ToList();
                var res = await result.Where(x => x.Datesold.Date == startDate).ToListAsync();
                var all = Tuple.Create<IEnumerable<MagazineSrore.Models.User1>, IEnumerable<MagazineSrore.Models.Product>, IEnumerable<MagazineSrore.Models.Sale2>>(xyz, xyz1, res);
                return View(all);

            }
            else
            {
                var xyz = _context.User1s.ToList();
                var xyz1 = _context.Products.ToList();
                var res = await result.Where(x => x.Datesold >= startDate && x.Datesold <= endDate).ToListAsync();
                //var date = res.FirstOrDefault().Datesold;
                var all = Tuple.Create<IEnumerable<MagazineSrore.Models.User1>, IEnumerable<MagazineSrore.Models.Product>, IEnumerable<MagazineSrore.Models.Sale2>>(xyz, xyz1, res);
                return View(all);

            }
        }
    }
}
