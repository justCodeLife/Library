using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Models.ViewModels
{
    public class AddEditBookViewModel
    {
        [Key] public int BookID { get; set; }

        [Display(Name = "نام کتاب")]
        [Required(ErrorMessage = "لطفا نام کتاب را وارد نمایید")]
        public string BookName { get; set; }

        [Display(Name = "توضیحات کتاب")]
        [Required(ErrorMessage = "لطفا توضیحات کتاب را وارد نمایید")]
        public string BookDescription { get; set; }

        [Display(Name = "تعداد صفحات کتاب")]
//        [Required(ErrorMessage = "لطفا تعداد صفحات کتاب را وارد نمایید")]
        public int BookPageCount { get; set; }

        [Display(Name = "تصویر کتاب")]
//        [Required(ErrorMessage = "لطفا تصویر کتاب را وارد نمایید")]
        public string BookImage { get; set; }

        [Display(Name = "گروه بندی")] public int BookGroupID { get; set; }
        public List<SelectListItem> BookGroups { get; set; }
        [Display(Name = "نام نویسنده")] public int AuthorID { get; set; }
        public List<SelectListItem> Authors { get; set; }
        
    }
}