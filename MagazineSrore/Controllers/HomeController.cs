using MagazineSrore.Models;
using Aspose.Pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MagazineSrore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var catigories = _context.Categories.ToList();
            var test = _context.Testimonials.ToList();
            var all = Tuple.Create<IEnumerable<Category>, IEnumerable<Testimonial>>(catigories, test);
            return View(all);
        }
        public IActionResult Product(int id, string name)
        {
            var cat = _context.Categories.ToList();
            var pro = _context.Products.ToList();

            if (id != 0 && name == null)
            {
                var xyz = from c in cat
                          join p in pro on c.Cid equals p.Cid
                          where p.Cid.Equals(id)
                          select new JoinTable { category = c, product = p };
                return View(xyz);
            }
            else if (id != 0 && name != null)
            {
                var xyz = from c in cat
                          join p in pro on c.Cid equals p.Cid
                          where p.Cid.Equals(id) && p.Pname.Contains(name)
                          select new JoinTable { category = c, product = p };
                return View(xyz);
            }
            else if (id == 0 && name != null)
            {
                var xyz = from c in cat
                          join p in pro on c.Cid equals p.Cid
                          where p.Pname.Contains(name)
                          select new JoinTable { category = c, product = p };
                return View(xyz);
            }

            else
            {
                //var xyz = _context.Categories.ToList();
                //var xyz = _context.Products.ToList();
                var xyz = from c in cat
                          join p in pro on c.Cid equals p.Cid
                          select new JoinTable { category = c, product = p };
                return View(xyz);
        }

    }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Pay(int id)
        {
            var xyz = _context.Products.Where(x => x.Pid == id).SingleOrDefault();
            return View(xyz);
        }
        public IActionResult Transaction(int id)
        {
            string time = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
                      
            var price = _context.Products.Where(x => x.Pid == id).SingleOrDefault().Price;

            Aspose.Pdf.Document document = new Aspose.Pdf.Document();
            Aspose.Pdf.Page page = document.Pages.Add();
            
            Table table = new Aspose.Pdf.Table();
                table.Border = new BorderInfo(BorderSide.All, .5f, Color.FromRgb(System.Drawing.Color.Purple));
                // Add text to new page
                table.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Green));
                //var invoice = _context.OrdersdoneFps.Where(x => x.UserFk == custId).ToList();
                var card1 = _context.Products.Where(x => x.Pid == id).SingleOrDefault();
                var sale = new Sale2
                {
                    Datesold = DateTime.Now.Date,
                    Price = (decimal)(card1.Price),
                    Amount = 1,
                    Pid = id,
                    Userid = (decimal)HttpContext.Session.GetInt32("Useid")
                };
                page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("feshnacy"));
                page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
                page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
                page.ArtBox.Center();


                {
                    // Add row to table
                    Aspose.Pdf.Row row = table.Rows.Add();
                    // Add table cells
                    row.Cells.Add("The Date Of Purchase");
                    row.Cells.Add("Product Name");
                    row.Cells.Add("Product Price");


                }

                //for multible products
                {
                    Aspose.Pdf.Row row = table.Rows.Add();
                    // Add table cells
                    row.Cells.Add(DateTime.Now.ToString());
                    row.Cells.Add(card1.Pname.ToString());
                    row.Cells.Add("$" + card1.Price.ToString());

                }

                page.Paragraphs.Add(table);
                //var sum = _context.OrdersFps.Where(x => x.UserFk == custId).Sum(x => x.HandcraftFkNavigation.Price);

                page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

                page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Thank you for shopping, feshnacy "));
                //num == rand to save docu 
                document.Save(time + " document.pdf");
                SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com", 587);
                string email = HttpContext.Session.GetString("Email");
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com", 587);
                MailAddress mf = new MailAddress("moayadmohammad2001@outlook.com", "moayadmohammad2001@outlook.com");
                mail.From=new MailAddress("moayadmohammad2001@outlook.com", "moayad");
                Console.WriteLine(email);
                mail.To.Add(new MailAddress(email, email));
                mail.Subject = "Purchase Invoice";
                mail.Body = "Moayad";
                smtp.Credentials = new NetworkCredential("moayadmohammad2001@outlook.com", "ASDFasdf12345");
                Attachment data = new Attachment(time + " document.pdf");
                smtp.EnableSsl = true;

                mail.Attachments.Add(data);
                smtp.Send(mail);


                {      //remove item product
                    _context.Add(sale);
                    _context.SaveChangesAsync();
                }
                /* */
                return RedirectToAction(nameof(Product));

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult EidtUser()
        {
            ViewBag.id = HttpContext.Session.GetInt32("Useid");
            //var user = _context.User1s.Where(x => x.Userid == ViewBag.id).SingleOrDefault();
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult About()
        {
            var image = _context.Homes.Where(x => x.Hid == 3).SingleOrDefault().ToString();
            ViewBag.image = image;
            return View();
        }
    }
    
}
