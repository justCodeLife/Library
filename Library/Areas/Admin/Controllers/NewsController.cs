using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public NewsController(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            List<News> model = new List<News>();
            model = _context.News.Select(n => new News
            {
                NewsID = n.NewsID,
                newsTitle = n.newsTitle,
                newsContent = n.newsContent,
                newsDate = n.newsDate,
                newsImage = n.newsImage
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddEditNews(int id)
        {
            var model = new News();

            if (id != 0)
            {
                using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    model = _context.News.Find(id);
                    if (model == null)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            var currentDay = DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(currentDay);
            int month = persianCalendar.GetMonth(currentDay);
            int day = persianCalendar.GetDayOfMonth(currentDay);
            string shamsiDate = $"{Convert.ToDateTime(day + "/" + month + "/" + year):yyyy/MM/dd}";
            ViewBag.sdate = shamsiDate;
            return PartialView("AddEditNews", model);
        }

        public IActionResult DeleteNews()
        {
            return View();
        }
    }
}