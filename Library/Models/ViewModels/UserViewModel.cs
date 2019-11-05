using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Models.ViewModels
{
    public class UserViewModel
    {
        public string ID { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا نام کاربری را وارد نمایید")]
        public string Username { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا رمز عبور را وارد نمایید")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "رمز عبور باید بین 6 تا 20 رفم باشد")]

        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا تکرار رمز عبور را وارد نمایید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور با تکرارش برابر نیست")]
        public string ConfirmPassword { get; set; }

//        [Display(Name = "نام و نام خانوادگی")]
//        [Required(ErrorMessage = "لطفا نام و نام خانوادگی را وارد نمایید")]
//        public string Fullname { get; set; }

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