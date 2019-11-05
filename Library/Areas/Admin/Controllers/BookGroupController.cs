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
//    [Authorize(Roles = "Admin")]
    public class BookGroupController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public BookGroupController(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = _context.BookGroups.Include(b => b.books);
            return View(await model.ToListAsync());


//            List<BookGroup> model = new List<BookGroup>();
//            model = _context.BookGroups.Select(bg => new BookGroup
//            {
//                BookGroupID = bg.BookGroupID,
//                BookGroupName = bg.BookGroupName,
//                BookGroupDescription = bg.BookGroupDescription
//            }).ToList();
//            return
//                View(model);
        }

        [HttpGet]
        public IActionResult AddEditBookGroup(int id)
        {
            var bookgroup = new BookGroup();
            if (id != 0)
            {
                using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    bookgroup = _context.BookGroups.SingleOrDefault(a => a.BookGroupID == id);
                    if (bookgroup == null)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return PartialView("_AddEditBookGroup", bookgroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditBookGroup(BookGroup model, int id, string redirectURL)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        db.BookGroups.Add(model);
                        db.SaveChanges();
                    }

//                    return RedirectToAction("Index");
                    return PartialView("_successfulResponse", redirectURL);
                }
                else
                {
                    using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        var m = db.BookGroups.Find(id);
                        m.BookGroupName = model.BookGroupName;
                        m.BookGroupDescription = model.BookGroupDescription;
                        db.BookGroups.Update(m);
                        db.SaveChanges();
                    }

//                    return RedirectToAction("Index");
                    return PartialView("_successfulResponse", redirectURL);
                }
            }
            else
            {
                return PartialView("_AddEditBookGroup", model);
            }
        }

        [HttpGet]
        public IActionResult DeleteBookGroup(int id)
        {
            var tblBookGroup = new BookGroup();
            using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                tblBookGroup = db.BookGroups.SingleOrDefault(bg => bg.BookGroupID == id);
                if (tblBookGroup == null)
                {
                    return RedirectToAction("Index");
                }
            }

            return PartialView("_deleteGroup", tblBookGroup.BookGroupName);
        }

        [HttpPost]
        public IActionResult DeleteBookGroup(int id, IFormCollection form)
        {
            using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                var m = db.BookGroups.Find(id);
                db.BookGroups.Remove(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}