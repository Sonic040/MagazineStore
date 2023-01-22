using MagazineSrore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineSrore.Controllers
{

    public class LoginRigisterController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LoginRigisterController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Rigister()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rigister([Bind("Userid,Fname,Lname,Email,Password,Imagepath,ImageFile,Roleid")] User1 user1)
        {
            if (ModelState.IsValid)
            {
                
                
               if (user1.ImageFile != null)
                {
                    if (user1.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + user1.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await user1.ImageFile.CopyToAsync(fileStream);
                        }
                        user1.Imagepath = fileName;
                        _context.Add(user1);
                    }
                    user1.Roleid = 1;
                    _context.Add(user1);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "LoginRigister");
                }
            }
            return View(user1);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] User1 user1)
        {
            var auth = _context.User1s.Where(result => result.Email == user1.Email && result.Password == user1.Password).SingleOrDefault();
            if (auth != null)
            {
                switch (auth.Roleid)
                {
                    case 1:
                        HttpContext.Session.SetString("Email", auth.Email);
                        HttpContext.Session.SetInt32("Useid", (int)auth.Userid);
                        HttpContext.Session.SetString("name", auth.Fname);
                        //ViewBag.userPatient = HttpContext.Session.GetString("UserName");
                        return RedirectToAction("Product", "Home",auth.Userid);
                    case 2:
                        HttpContext.Session.SetString("Email", auth.Email);
                        HttpContext.Session.SetInt32("Useid", (int)auth.Userid);
                        HttpContext.Session.SetString("name", auth.Fname);
                        return RedirectToAction("Index", "Admin", auth.Userid);
                    default:
                        break;
                }
            }
            return View();
        }

    }
}
