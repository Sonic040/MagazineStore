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
    public class HomesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public HomesController(ModelContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Homes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Homes.ToListAsync());
        }

        // GET: Homes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.Hid == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // GET: Homes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hid,Imagepath,ImageFile")] Home home)
        {
            if (ModelState.IsValid)
            {
                if (home.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnvironment.WebRootPath; // wwwrootpath
                    string imageName = Guid.NewGuid().ToString() + "_" + home.ImageFile.FileName; // image name
                    string path = Path.Combine(wwwrootPath + "/Image/", imageName); // wwwroot/Image/imagename

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await home.ImageFile.CopyToAsync(filestream);
                    }
                    home.Imagepath = imageName;
                    _context.Add(home);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(home);
        }

        // GET: Homes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }
            return View(home);
        }

        // POST: Homes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Hid,Imagepath,ImageFile")] Home home)
        {
            if (id != home.Hid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (home.ImageFile != null)
                    {
                        string wwwrootPath = _webHostEnvironment.WebRootPath; // wwwrootpath
                        string imageName = Guid.NewGuid().ToString() + "_" + home.ImageFile.FileName; // image name
                        string path = Path.Combine(wwwrootPath + "/Image/", imageName); // wwwroot/Image/imagename

                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await home.ImageFile.CopyToAsync(filestream);
                        }
                        home.Imagepath = imageName;
                        _context.Update(home);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeExists(home.Hid))
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
            return View(home);
        }

        // GET: Homes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.Hid == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // POST: Homes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var home = await _context.Homes.FindAsync(id);
            _context.Homes.Remove(home);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeExists(decimal id)
        {
            return _context.Homes.Any(e => e.Hid == id);
        }
    }
}
