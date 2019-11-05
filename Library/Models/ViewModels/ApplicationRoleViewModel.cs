using System.ComponentModel.DataAnnotations;

namespace Library.Models.ViewModels
{
    public class ApplicationRoleViewModel
    {
        public string Id { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا عنوان نقش را وارد نمایید")]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا توضیحات نقش را وارد نمایید")]
        public string Description { get; set; }

        [Display(Name = "تعداد کاربران")] public int NumberOfUsers { get; set; }
    }
}