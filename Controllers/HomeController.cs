using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        public ActionResult Index(int? categoryid, string sortby)
        {
            ViewData["categories"] = _context.ProductCategory.ToList();
            if (categoryid == null)
            {
                ViewData["products"] = _context.Product.ToList();
            }
            else
            {
                ViewData["products"] = _context.Product.Where(p => p.ProductCategoryId == categoryid).ToList();
            }

            if (sortby == "price-high")
            {
                ViewData["products"] = _context.Product.OrderByDescending(p => p.Price).ToList();
            }
            else if (sortby == "price-low")
            {
                ViewData["products"] = _context.Product.OrderBy(p => p.Price).ToList();
            }


            return View();
        }
        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}