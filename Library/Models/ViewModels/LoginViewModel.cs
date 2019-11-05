using System.ComponentModel.DataAnnotations;

namespace Library.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری : ")]
        [Required(ErrorMessage = "لطفا نام کاربری خود را وارد کنید")]
        public string Username { get; set; }

        [Display(Name = "رمز عبور : ")]
        [Required(ErrorMessage = "لطفا رمز عبور خود را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "مرا بخاطر بسپار")] public bool RememberMe { get; set; }
    }
}