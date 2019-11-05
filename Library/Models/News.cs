using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class News
    {
        [Key] public int NewsID { get; set; }

        [Display(Name = "عنوان خبر")]
        [Required(ErrorMessage = "عنوان خبر را وارد نمایید")]
        public string newsTitle { get; set; }

        [Display(Name = "متن خبر")]
        [Required(ErrorMessage = "متن خبر را وارد نمایید")]
        public string newsContent { get; set; }

        [Display(Name = "تاریخ خبر")]
        [Required(ErrorMessage = "تاریخ خبر را وارد نمایید")]
        public string newsDate { get; set; }

        [Display(Name = "تصویر خبر")]
        [Required(ErrorMessage = "تصویر خبر را وارد نمایید")]
        public string newsImage { get; set; }
    }
}