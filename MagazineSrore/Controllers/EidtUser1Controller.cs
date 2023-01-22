using MagazineSrore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineSrore.Controllers
{
    public class EidtUser1Controller : Controller
    {
        private readonly ModelContext _context;

        public EidtUser1Controller(ModelContext context)
        {
            _context = context;
        }
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
            ViewBag.id = id;
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
            ViewBag.id = id;
            return View(user1);
        }

        private bool User1Exists(decimal userid)
        {
            throw new NotImplementedException();
        }
    }
}
