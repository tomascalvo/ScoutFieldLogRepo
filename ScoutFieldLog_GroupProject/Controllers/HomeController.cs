using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ScoutFieldLog_GroupProject.Models;

namespace ScoutFieldLog_GroupProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScoutFieldLogDbContext _context;

        public HomeController(ScoutFieldLogDbContext context)

        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ScoutForm()
        {          
            return View();
        }

        [HttpPost]
        public IActionResult ScoutForm(StartUp startup)
        {
            _context.Add(startup);
            _context.SaveChanges();
            ViewBag.message = "Thank you for submitting your startup company!";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
