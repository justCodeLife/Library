using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Models.ViewModels
{
    public class EditUserViewModel
    {
        public int ID { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا نام را وارد نمایید")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد نمایید")]
        public string Lastname { get; set; }

        [Display(Name = "تلفن")]
        [Required(ErrorMessage = "لطفا تلفن را وارد نمایید")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا ایمیل را وارد نمایید")]
        [EmailAddress(ErrorMessage = "آدرس ایمیل معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "جنسیت")]
        public byte gender { get; set; }
        
        public List<SelectListItem> ApplicationRoles { get; set; }
        [Display(Name = "نقش")] public string ApplicationRoleID { get; set; }
    }
}