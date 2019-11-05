using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookGroup
    {
        [Key] public int BookGroupID { get; set; }

        [DisplayName("عنوان گروه")]
        [Required(ErrorMessage = "لطفا نام گروه را وارد نمایید")]
        public string BookGroupName { get; set; }

        [DisplayName("توضیحات گروه")]
        [Required(ErrorMessage = "لطفا توضیحات گروه را وارد نمایید")]
        public string BookGroupDescription { get; set; }

        public virtual ICollection<Book> books { get; set; }
    }
}