using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Author
    {
        [Key] public int AuthorID { get; set; }

        [Display(Name = "نام نویسنده")]
        [Required(ErrorMessage = "لطفا نام نویسنده را وارد نمایید")]
        public string AuthorName { get; set; }

        [Display(Name = "توضیحات نویسنده")]
        [Required(ErrorMessage = "لطفا توضیحات نویسنده را وارد نمایید")]
        public string AuthorDescription { get; set; }

        public virtual ICollection<Book> books { get; set; }
    }
}