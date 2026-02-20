using Microsoft.AspNetCore.Mvc;

namespace FormData.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string? email, string? mailclient, string? password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(mailclient) || string.IsNullOrEmpty(password))
            {
                return View();
            }
            ViewBag.Success = "Registratie succesvol";
            return View();
        }
    }
}
