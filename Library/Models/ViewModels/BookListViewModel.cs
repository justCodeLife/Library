using System.ComponentModel.DataAnnotations;

namespace Library.Models.ViewModels
{
    public class BookListViewModel
    {
        public int BookID { get; set; }
        [Display(Name = "نام کتاب")] public string BookName { get; set; }
        [Display(Name = "تعداد صفحات")] public int BookPageCount { get; set; }
        [Display(Name = "تصویر")] public string BookImage { get; set; }
        public int AuthorID { get; set; }
        public int BookGroupID { get; set; }
        [Display(Name = "نویسنده")] public string AuthorName { get; set; }
        [Display(Name = "گروه بندی")] public string BookGroupName { get; set; }
    }
}