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
    public class User1Controller : Controller
    {
        private readonly ModelContext _context;

        public User1Controller(ModelContext context)
        {
            _context = context;
        }

        // GET: User1
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.User1s.Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: User1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user1 = await _context.User1s
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (user1 == null)
            {
                return NotFound();
            }

            return View(user1);
        }

        // GET: User1/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename");
            return View();
        }

        // POST: User1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Fname,Lname,Email,Password,Imagepath,Roleid")] User1 user1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", user1.Roleid);
            return View(user1);
        }

        // GET: User1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user1 = await _context.User1s.FindAsync(id);
            if (user1 == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", user1.Roleid);
            return View(user1);
        }

        // POST: User1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userid,Fname,Lname,Email,Password,Imagepath,ImageFile,Roleid")] User1 user1)
        {
            if (id != user1.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!User1Exists(user1.Userid))
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
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", user1.Roleid);
            return View(user1);
        }

        // GET: User1/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user1 = await _context.User1s
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (user1 == null)
            {
                return NotFound();
            }

            return View(user1);
        }

        // POST: User1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var user1 = await _context.User1s.FindAsync(id);
            _context.User1s.Remove(user1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool User1Exists(decimal id)
        {
            return _context.User1s.Any(e => e.Userid == id);
        }
    }
}
