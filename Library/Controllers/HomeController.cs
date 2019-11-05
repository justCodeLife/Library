using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, IServiceProvider serviceProvider,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var model = new MultiModel();
            model.LastBook = (from b in _context.Books orderby b.BookID descending select b).Take(6).ToList();
            model.MoreViewerBook = (from b in _context.Books orderby b descending select b).Take(6).ToList();
            model.ScientificBooks = (from b in _context.Books where b.BookGroupID == 7 orderby b descending select b)
                .Take(6).ToList();

            model.Users = (from u in _userManager.Users orderby u.Id descending select u).Take(10).ToList();


            ViewBag.imagePath = "/upload/normalImage/";
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}