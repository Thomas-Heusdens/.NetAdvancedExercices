using Microsoft.AspNetCore.Mvc;
using PassingModelData.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace PassingModelData.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Producten(Category? category)
        {
            List<ProductModel> products = new List<ProductModel>
            {
                // --- SHOES ---
                new ProductModel { Id = 1, Name = "Retro High-Tops", Category = Category.SHOES, Description = "Classic leather sneakers.", Price = 120.00f },
                new ProductModel { Id = 2, Name = "Performance Runners", Category = Category.SHOES, Description = "Breathable mesh with arch support.", Price = 89.99f },
                new ProductModel { Id = 3, Name = "Casual Loafers", Category = Category.SHOES, Description = "Suede slip-ons for business casual.", Price = 75.00f },
                new ProductModel { Id = 4, Name = "Hiking Boots", Category = Category.SHOES, Description = "Waterproof and rugged terrain ready.", Price = 145.50f },

                // --- T-SHIRTS ---
                new ProductModel { Id = 5, Name = "Vintage Band Tee", Category = Category.T_SHIRTS, Description = "Soft-wash cotton graphic tee.", Price = 25.00f },
                new ProductModel { Id = 6, Name = "Basic V-Neck", Category = Category.T_SHIRTS, Description = "Plain white essential tee.", Price = 15.00f },
                new ProductModel { Id = 7, Name = "Graphic Streetwear", Category = Category.T_SHIRTS, Description = "Limited edition artist print.", Price = 45.00f },
                new ProductModel { Id = 8, Name = "Long Sleeve Jersey", Category = Category.T_SHIRTS, Description = "Lightweight layering piece.", Price = 30.00f },

                // --- PANTS ---
                new ProductModel { Id = 9, Name = "Slim Fit Chinos", Category = Category.PANTS, Description = "Stretch khaki for all-day comfort.", Price = 55.00f },
                new ProductModel { Id = 10, Name = "Selvedge Denim", Category = Category.PANTS, Description = "Raw indigo denim, straight leg.", Price = 110.00f },
                new ProductModel { Id = 11, Name = "Cargo Work Pants", Category = Category.PANTS, Description = "Multi-pocket durable canvas.", Price = 65.00f },
                new ProductModel { Id = 12, Name = "Dress Slacks", Category = Category.PANTS, Description = "Tailored wool blend.", Price = 95.00f },

                // --- SHORTS ---
                new ProductModel { Id = 13, Name = "Athletic Mesh Shorts", Category = Category.SHORTS, Description = "Quick-dry fabric for gym sessions.", Price = 22.50f },
                new ProductModel { Id = 14, Name = "Flat-Front Chino Shorts", Category = Category.SHORTS, Description = "Classic 7-inch inseam.", Price = 35.00f },
                new ProductModel { Id = 15, Name = "Cargo Shorts", Category = Category.SHORTS, Description = "Rugged outdoor shorts.", Price = 38.00f },
                new ProductModel { Id = 16, Name = "Board Shorts", Category = Category.SHORTS, Description = "", Price = 40.00f },

                // --- SWEATSHIRTS ---
                new ProductModel { Id = 17, Name = "Heavyweight Hoodie", Category = Category.SWEATSHIRT, Description = "Fleece-lined charcoal hoodie.", Price = 60.00f },
                new ProductModel { Id = 18, Name = "Quarter-Zip Pullover", Category = Category.SWEATSHIRT, Description = "Pique knit sporty layer.", Price = 52.00f },
                new ProductModel { Id = 19, Name = "Crewneck Sweater", Category = Category.SWEATSHIRT, Description = "Minimalist embroidered logo.", Price = 48.00f },
                new ProductModel { Id = 20, Name = "Zip-up Windbreaker", Category = Category.SWEATSHIRT, Description = "Water-resistant tech fleece.", Price = 75.00f },
            };
            if (category.HasValue)
            {
                products = products.Where(p => p.Category == category.Value).ToList();
                ViewBag.SelectedCategory = category;
            }
            else
            {
                ViewBag.SelectedCategory = null;
            }
            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Openingsuren()
        {
            var hours = new List<BusinessDay>
            {
                new BusinessDay { Day = DayOfWeek.Monday, OpenTime = new TimeSpan(9, 0, 0), CloseTime = new TimeSpan(18, 0, 0) },
                new BusinessDay { Day = DayOfWeek.Tuesday, OpenTime = new TimeSpan(9, 0, 0), CloseTime = new TimeSpan(18, 0, 0) },
                new BusinessDay { Day = DayOfWeek.Wednesday, OpenTime = new TimeSpan(9, 0, 0), CloseTime = new TimeSpan(18, 0, 0) },
                new BusinessDay { Day = DayOfWeek.Thursday, OpenTime = new TimeSpan(9, 0, 0), CloseTime = new TimeSpan(20, 0, 0) },
                new BusinessDay { Day = DayOfWeek.Friday, OpenTime = new TimeSpan(9, 0, 0), CloseTime = new TimeSpan(18, 0, 0) },
                new BusinessDay { Day = DayOfWeek.Saturday, OpenTime = new TimeSpan(10, 0, 0), CloseTime = new TimeSpan(16, 0, 0) },
                new BusinessDay { Day = DayOfWeek.Sunday, IsClosed = true }
            };

            // Logic to check if currently open
            var now = DateTime.Now;
            var today = hours.FirstOrDefault(h => h.Day == now.DayOfWeek);
            bool isOpenNow = false;

            if (today != null && !today.IsClosed)
            {
                isOpenNow = now.TimeOfDay >= today.OpenTime && now.TimeOfDay <= today.CloseTime;
            }

            ViewBag.IsOpenNow = isOpenNow;
            return View(hours);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(string Name, string Email, string Message)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Message))
            {
                ViewBag.ErrorMessage = "Vul aub alle velden in.";
                return View();
            }

            try
            {
                // Pull the variables from appsettings.json and User Secrets
                string server = _config["SmtpSettings:Server"];
                int port = int.Parse(_config["SmtpSettings:Port"]);
                string senderEmail = _config["SmtpSettings:SenderEmail"];
                string password = _config["SmtpSettings:Password"];
                string destinationEmail = _config["SmtpSettings:DestinationEmail"];

                using (var client = new SmtpClient(server, port))
                {
                    client.Credentials = new NetworkCredential(senderEmail, password);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(senderEmail);
                    mailMessage.To.Add(destinationEmail);
                    mailMessage.ReplyToList.Add(new MailAddress(Email));

                    mailMessage.Subject = $"Nieuw contactformulier van {Name}";
                    mailMessage.Body = $"Je hebt een nieuw bericht ontvangen via de website.\n\n" +
                                       $"Naam: {Name}\n" +
                                       $"Email: {Email}\n\n" +
                                       $"Bericht:\n{Message}";

                    await client.SendMailAsync(mailMessage);
                }

                ViewBag.SuccessMessage = "Bedankt voor je bericht! We nemen zo snel mogelijk contact met je op.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Er is iets misgegaan bij het verzenden van de email. Probeer het later opnieuw.";
                return View();
            }
        }
    }
}
