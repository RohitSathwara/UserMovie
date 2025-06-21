using Microsoft.AspNetCore.Mvc;

namespace UserMovie.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBData _dbData;

        public HomeController(DBData dBData)
        {
            _dbData = dBData;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            ViewBag.Users = _dbData.GetAllUsers();
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            return View();
        }
    }
}
