using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InsertShowImage;
using Library.Models;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.Areas.Admin.Controllers
{
    [Area("Admin")]
//    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostingEnvironment _appEnvironment;

        public BookController(ApplicationDbContext context, IServiceProvider serviceProvider,
            IHostingEnvironment appEnvironment)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<BookListViewModel> model = new List<BookListViewModel>();
            var q = (from b in _context.Books
                join a in _context.Authors on b.AuthorID equals a.AuthorID
                join bg in _context.BookGroups on b.BookGroupID equals bg.BookGroupID
                select new
                {
                    b.BookID,
                    b.BookName,
                    b.BookPageCount,
                    b.BookImage,
                    b.AuthorID,
                    b.BookGroupID,
                    a.AuthorName,
                    bg.BookGroupName
                });

            foreach (var item in q)
            {
                BookListViewModel obj = new BookListViewModel
                {
                    BookID = item.BookID,
                    BookName = item.BookName,
                    BookPageCount = item.BookPageCount,
                    BookImage = item.BookImage,
                    AuthorID = item.AuthorID,
                    BookGroupID = item.BookGroupID,
                    AuthorName = item.AuthorName,
                    BookGroupName = item.BookGroupName
                };


                model.Add(obj);
            }

            ViewBag.Rootpath = "~/upload/thumbnailimage/";
            return
                View(model);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            AddEditBookViewModel model = new AddEditBookViewModel();
            model.BookGroups = _context.BookGroups.Select(bg => new SelectListItem
            {
                Text = bg.BookGroupName,
                Value = bg.BookGroupID.ToString()
            }).ToList();

            model.Authors = _context.Authors.Select(a => new SelectListItem
            {
                Text = a.AuthorName,
                Value = a.AuthorID.ToString()
            }).ToList();
            return PartialView("_AddEditBook", model);
        }

        [HttpGet]
        public IActionResult EditBook(int id)
        {
            AddEditBookViewModel model = new AddEditBookViewModel();
            model.BookGroups = _context.BookGroups.Select(bg => new SelectListItem
            {
                Text = bg.BookGroupName,
                Value = bg.BookGroupID.ToString()
            }).ToList();

            model.Authors = _context.Authors.Select(a => new SelectListItem
            {
                Text = a.AuthorName,
                Value = a.AuthorID.ToString()
            }).ToList();
            if (id != 0)
            {
                using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    Book book = _context.Books.SingleOrDefault(b => b.BookID == id);
                    if (book != null)
                    {
                        model.BookID = book.BookID;
                        model.BookName = book.BookName;
                        model.BookDescription = book.BookDescription;
                        model.BookPageCount = book.BookPageCount;
                        model.AuthorID = book.AuthorID;
                        model.BookGroupID = book.BookGroupID;
                        model.BookImage = book.BookImage;
                    }
                }
            }

            return PartialView("_AddEditBook", model);
        }

        [HttpGet]
        public IActionResult DeleteBook(int id)
        {
            var model = new Book();
            using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                model = db.Books.Find(id);
                if (model == null)
                {
                    return RedirectToAction("Index");
                }
            }

            return PartialView("_deleteBook", model.BookName);
        }

        [HttpPost]
        public IActionResult DeleteBook(int id, IFormCollection form)
        {
            using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                var model = _context.Books.Find(id);
                if (model.BookImage != "defaultPic.png")
                {
                    var uploadsNormal = Path.Combine(_appEnvironment.ContentRootPath, "upload\\normalImage\\") +
                                        model.BookImage;
                    if (System.IO.File.Exists(uploadsNormal))
                    {
                        System.IO.File.Delete(uploadsNormal);
                    }

                    var uploadsThumbnail = Path.Combine(_appEnvironment.ContentRootPath, "upload\\thumbnailImage\\") +
                                           model.BookImage;
                    if (System.IO.File.Exists(uploadsThumbnail))
                    {
                        System.IO.File.Delete(uploadsThumbnail);
                    }
                }

                db.Books.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult AddEditBook(int BookID, AddEditBookViewModel model, string redirectURL)
        public async Task<IActionResult> AddEditBook(int BookID, AddEditBookViewModel model,
            IEnumerable<IFormFile> files, string imgName)
        {
            if (ModelState.IsValid)
            {
                //upload image
                var uploads = Path.Combine(_appEnvironment.ContentRootPath, "upload\\normalImage\\");
                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        using (var fs = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fs);
                            model.BookImage = fileName;
                        }

                        ImageResizer img = new ImageResizer();
                        img.Resize(uploads + fileName,
                            Path.Combine(_appEnvironment.ContentRootPath, "upload\\thumbnailImage\\" + fileName));
                    }
                }

                //upload image
                if (BookID == 0)
                {
                    if (model.BookImage == null)
                    {
                        model.BookImage = "defaultPic.png";
                    }

                    using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        Book bookModel = Mapper.Map<AddEditBookViewModel, Book>(model);
                        db.Books.Add(bookModel);
                        db.SaveChanges();
                    }

                    return Json(new
                    {
                        status = "success",
                        message = "کتاب با موفقیت ایجاد شد"
                    });
                }
                else
                {
                    if (model.BookImage == null)
                    {
                        model.BookImage = imgName;
                    }

                    using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        Book bookModel = Mapper.Map<AddEditBookViewModel, Book>(model);
                        db.Books.Update(bookModel);
                        db.SaveChanges();
                    }

                    return Json(new
                    {
                        status = "success",
                        message = "کتاب با موفقیت ویرایش شد"
                    });
                }
            }

            model.BookGroups = _context.BookGroups.Select(bg => new SelectListItem
            {
                Text = bg.BookGroupName,
                Value = bg.BookGroupID.ToString()
            }).ToList();

            model.Authors = _context.Authors.Select(a => new SelectListItem
            {
                Text = a.AuthorName,
                Value = a.AuthorID.ToString()
            }).ToList();


            var list = new List<string>();
            foreach (var validation in ViewData.ModelState.Values)
            {
                list.AddRange(validation.Errors.Select(error => error.ErrorMessage));
            }

            return Json(new
            {
                status = "error",
                error = list
            });


//            if (ModelState.IsValid)
//            {
//                if (BookID == 0)
//                {
//                    using (var db = _serviceProvider.GetRequiredService<ApplicationDbContext>())
//                    {
//                        Book bookModel = AutoMapper.Mapper.Map<AddEditBookViewModel, Book>(model);
//                        db.Books.Add(bookModel);
//                        db.SaveChanges();
//                    }
//
//                    return PartialView("_successfulResponse", redirectURL);
//                }
//                else
//                {
//                    using (var  db=_serviceProvider.GetRequiredService<ApplicationDbContext>())
//                    {
//                        Book bookModel = AutoMapper.Mapper.Map<AddEditBookViewModel, Book>(model);
//                        db.Books.Update(bookModel);
//                        db.SaveChanges();
//                    }
//                }
//            }
//            model.BookGroups = _context.BookGroups.Select(bg => new SelectListItem
//            {
//                Text = bg.BookGroupName,
//                Value = bg.BookGroupID.ToString()
//            }).ToList();
//
//            model.Authors = _context.Authors.Select(a => new SelectListItem
//            {
//                Text = a.AuthorName,
//                Value = a.AuthorID.ToString()
//            }).ToList();
//            return PartialView("_AddEditBook", model);
        }
    }
}