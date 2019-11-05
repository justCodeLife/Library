using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Book
    {
        [Key] public int BookID { get; set; }
        public string BookName { get; set; }
        public string BookDescription { get; set; }
        public int BookPageCount { get; set; }
        public string BookImage { get; set; }
        public int AuthorID { get; set; }
        [ForeignKey("AuthorID")] public virtual Author authors { get; set; }
        public int BookGroupID { get; set; }
        [ForeignKey("BookGroupID")] public virtual BookGroup bookGroups { get; set; }
    }
}