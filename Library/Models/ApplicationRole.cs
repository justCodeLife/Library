using Microsoft.AspNetCore.Identity;

namespace Library.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}