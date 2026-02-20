using Microsoft.AspNetCore.Mvc;

namespace FormData.Controllers
{
    public class DerdeOefeningController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }
            HttpContext.Session.SetString("name", name);
            return RedirectToAction("Name");
        }

        public IActionResult Name()
        {
            ViewBag.Name = HttpContext.Session.GetString("name");
            return View();
        }
    }
}
