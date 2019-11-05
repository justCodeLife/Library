using Microsoft.AspNetCore.Mvc;

namespace Library.Areas.User.Controllers
{
    public class UserProfileController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}