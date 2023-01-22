using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MagazineSrore.Models;

namespace MagazineSrore.Controllers
{
    public class Sale2Controller : Controller
    {
        private readonly ModelContext _context;

        public Sale2Controller(ModelContext context)
        {
            _context = context;
        }

        // GET: Sale2
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Sale2s.Include(s => s.PidNavigation).Include(s => s.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Sale2/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale2 = await _context.Sale2s
                .Include(s => s.PidNavigation)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Sid == id);
            if (sale2 == null)
            {
                return NotFound();
            }

            return View(sale2);
        }

        // GET: Sale2/Create
        public IActionResult Create()
        {
            ViewData["Pid"] = new SelectList(_context.Products, "Pid", "Pname");
            ViewData["Userid"] = new SelectList(_context.User1s, "Userid", "Email");
            return View();
        }

        // POST: Sale2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sid,Datesold,Price,Amount,Pid,Userid")] Sale2 sale2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sale2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Pid"] = new SelectList(_context.Products, "Pid", "Pname", sale2.Pid);
            ViewData["Userid"] = new SelectList(_context.User1s, "Userid", "Email", sale2.Userid);
            return View(sale2);
        }

        // GET: Sale2/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale2 = await _context.Sale2s.FindAsync(id);
            if (sale2 == null)
            {
                return NotFound();
            }
            ViewData["Pid"] = new SelectList(_context.Products, "Pid", "Pname", sale2.Pid);
            ViewData["Userid"] = new SelectList(_context.User1s, "Userid", "Email", sale2.Userid);
            return View(sale2);
        }

        // POST: Sale2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Sid,Datesold,Price,Amount,Pid,Userid")] Sale2 sale2)
        {
            if (id != sale2.Sid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Sale2Exists(sale2.Sid))
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
            ViewData["Pid"] = new SelectList(_context.Products, "Pid", "Pname", sale2.Pid);
            ViewData["Userid"] = new SelectList(_context.User1s, "Userid", "Email", sale2.Userid);
            return View(sale2);
        }

        // GET: Sale2/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale2 = await _context.Sale2s
                .Include(s => s.PidNavigation)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Sid == id);
            if (sale2 == null)
            {
                return NotFound();
            }

            return View(sale2);
        }

        // POST: Sale2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var sale2 = await _context.Sale2s.FindAsync(id);
            _context.Sale2s.Remove(sale2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Sale2Exists(decimal id)
        {
            return _context.Sale2s.Any(e => e.Sid == id);
        }
    }
}
