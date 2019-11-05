using System.Collections.Generic;

namespace Library.Models.ViewModels
{
    public class MultiModel
    {
        public List<Book> LastBook { get; set; }
        public List<Book> MoreViewerBook { get; set; }
        public List<Book> ScientificBooks { get; set; }

        public List<ApplicationUser> Users { get; set; }
    }
}