using FormData.Models;
using Microsoft.AspNetCore.Mvc;

namespace FormData.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Toon(string? besteller, string? pizzaNaam, string? kaas, string? betaalWijze)
        {
            int? index = HttpContext.Session.GetInt32("Index");

            if (index == null)
            {
                index = 1;
            }
            else
            {
                index++;
            }
            HttpContext.Session.SetInt32("Index", index.Value);

            if (string.IsNullOrEmpty(kaas))
            {
                kaas = "no";
            }

            if (string.IsNullOrEmpty(besteller) || string.IsNullOrEmpty(pizzaNaam) || string.IsNullOrEmpty(betaalWijze))
            {
                return BadRequest();
            }

            Pizza pizza = new Pizza()
            {
                Id = index.Value,
                Besteller = besteller,
                Name = pizzaNaam,
                Kaas = kaas,
                BetaalWijze = betaalWijze
            };

            return View(pizza);
        }
    }
}