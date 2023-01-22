using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MagazineSrore.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MagazineSrore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Products.Include(p => p.CidNavigation).Include(p => p.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.CidNavigation)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Pid == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["Cid"] = new SelectList(_context.Categories, "Cid", "Cname");
            ViewData["Userid"] = new SelectList(_context.User1s, "Userid", "Email");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pid,Pname,Price,Imagepath,Userid,Cid,ImageFile")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnvironment.WebRootPath; // wwwrootpath
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName; // image name
                    string path = Path.Combine(wwwrootPath + "/Image/", imageName); // wwwroot/Image/imagename

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(filestream);
                    }
                    product.Imagepath = imageName;

                    _context.Add(product);
                    await _context.SaveChangesAsync();


                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["Cid"] = new SelectList(_context.Categories, "Cid", "Cname", product.Cid);
            ViewData["Userid"] = new SelectList(_context.User1s, "Userid", "Email", product.Userid);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["Cid"] = new SelectList(_context.Categories, "Cid", "Cname", product.Cid);
            ViewData["Userid"] = new SelectList(_context.User1s, "Userid", "Email", product.Userid);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Pid,Pname,Price,Imagepath,Userid,Cid,ImageFile")] Product product)
        {
            if (id != product.Pid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.ImageFile != null)
                    {
                        string wwwrootPath = _webHostEnvironment.WebRootPath; // wwwrootpath
                        string imageName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName; // image name
                        string path = Path.Combine(wwwrootPath + "/Image/", imageName); // wwwroot/Image/imagename

                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await product.ImageFile.CopyToAsync(filestream);
                        }
                        product.Imagepath = imageName;
                        _context.Update(product);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Pid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cid"] = new SelectList(_context.Categories, "Cid", "Cname", product.Cid);
            ViewData["Userid"] = new SelectList(_context.User1s, "Userid", "Email", product.Userid);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.CidNavigation)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Pid == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(decimal id)
        {
            return _context.Products.Any(e => e.Pid == id);
        }
    }
}
