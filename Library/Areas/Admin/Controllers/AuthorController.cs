using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Areas.Admin.Controllers
{
    [Area("Admin")]
 //   [Authorize(Roles = "Admin")]
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public AuthorController(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = _context.Authors.Include(b => b.books);
            return View(await model.ToListAsync());
//            List<Author> model = new List<Author>();
//            model = _context.Authors.Select(a => new Author
//            {
//                AuthorID = a.AuthorID,
//                AuthorName = a.AuthorName,
//                AuthorDescription = a.AuthorDescription
//            }).ToList();
//            return
//                View(model);
        }

        [HttpGet]
        public IActionResult DeleteAuthor(int id)
        {
            var tblAuthor = new Author();
            using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                tblAuthor = db.Authors.SingleOrDefault(bg => bg.AuthorID == id);
                if (tblAuthor == null)
                {
                    return RedirectToAction("Index");
                }
            }

            return PartialView("_deleteAuthor", tblAuthor.AuthorName);
        }

        [HttpPost]
        public IActionResult DeleteAuthor(int id, IFormCollection form)
        {
            using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                var m = db.Authors.Find(id);
                db.Authors.Remove(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public IActionResult AddEditAuthor(int id)
        {
            var author = new Author();
            if (id != 0)
            {
                using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    author = _context.Authors.SingleOrDefault(a => a.AuthorID == id);
                    if (author == null)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return PartialView("_AddEditAuthor", author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditAuthor(Author model, int id, string redirectURL)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        db.Authors.Add(model);
                        db.SaveChanges();
                    }

//                    return RedirectToAction("Index");
                    return PartialView("_successfulResponse", redirectURL);
                }
                else
                {
                    using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        var m = db.Authors.Find(id);
                        m.AuthorName = model.AuthorName;
                        m.AuthorDescription = model.AuthorDescription;
                        db.Authors.Update(m);
                        db.SaveChanges();
                    }

//                    return RedirectToAction("Index");
                    return PartialView("_successfulResponse", redirectURL);
                }
            }
            else
            {
                return PartialView("_AddEditAuthor", model);
            }
        }
    }
}