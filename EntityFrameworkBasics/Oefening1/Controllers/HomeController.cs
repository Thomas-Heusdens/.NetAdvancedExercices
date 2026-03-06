using Microsoft.AspNetCore.Mvc;
using Oefening1.Data;
using Oefening1.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Oefening1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public HomeController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IActionResult Producten(Category? category)
        {
            var productsQuery = _context.Products.AsQueryable();

            if (category.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Category == category.Value);
                ViewBag.SelectedCategory = category;
            }
            else
            {
                ViewBag.SelectedCategory = null;
            }

            return View(productsQuery.ToList());
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Overzicht(List<ProductModel> ingediendeProducten)
        {
            var besteldeProducten = ingediendeProducten.Where(p => p.Quantity > 0).ToList();

            if (!besteldeProducten.Any())
            {
                return RedirectToAction("Producten");
            }

            return View(besteldeProducten);
        }

        [HttpPost]
        public async Task<IActionResult> BevestigBestelling(List<ProductModel> definitieveProducten)
        {
            var besteldeItems = definitieveProducten.Where(p => p.Quantity > 0).ToList();

            if (!besteldeItems.Any())
            {
                TempData["ErrorMessage"] = "Je hebt geen producten geselecteerd.";
                return RedirectToAction("Producten");
            }

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Nieuwe bestelling via de webshop!\n");
                sb.AppendLine("Klant: Thomas Heusdens");
                sb.AppendLine("Email: pilouthomas03@gmail.com\n");
                sb.AppendLine("Overzicht van de bestelling:");

                float totaalPrijs = 0;
                foreach (var item in besteldeItems)
                {
                    float subTotaal = (item.Price ?? 0) * item.Quantity;
                    totaalPrijs += subTotaal;

                    sb.AppendLine($"- {item.Quantity}x {item.Name} ({subTotaal.ToString("C")})");
                }

                sb.AppendLine($"\nTotaal te betalen: {totaalPrijs.ToString("C")}");

                string server = _config["SmtpSettings:Server"];
                int port = int.Parse(_config["SmtpSettings:Port"]);
                string senderEmail = _config["SmtpSettings:SenderEmail"];
                string password = _config["SmtpSettings:Password"];

                using (var client = new SmtpClient(server, port))
                {
                    client.Credentials = new NetworkCredential(senderEmail, password);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(senderEmail);
                    mailMessage.To.Add("pilouthomas03@gmail.com");

                    mailMessage.Subject = $"Nieuwe bestelling van Thomas Heusdens";
                    mailMessage.Body = sb.ToString();

                    await client.SendMailAsync(mailMessage);
                }

                TempData["SuccessMessage"] = "Bedankt voor je bestelling! De bevestigingsmail is verstuurd.";
                return RedirectToAction("Producten");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Er is iets misgegaan bij het verzenden van de email: " + ex.Message;
                return RedirectToAction("Producten");
            }
        }
    }
}
