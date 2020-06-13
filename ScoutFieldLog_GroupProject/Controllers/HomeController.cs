using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScoutFieldLog_GroupProject.Data;
using ScoutFieldLog_GroupProject.Models;

namespace ScoutFieldLog_GroupProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _identityContext;
        private readonly ScoutFieldLogDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ApplicationDbContext identityContext, ScoutFieldLogDbContext context, SignInManager<IdentityUser> signInManager)

        {
            _context = context;
            _identityContext = identityContext;
            _signInManager = signInManager;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StartupSearch(string CompanyName)
        {
            List<StartUp> startups;
            if (CompanyName is null || CompanyName == "")
            {
               startups = _context.StartUp.ToList();
            }
            else
            {
               startups = _context.StartUp.Where(x => x.CompanyName.Contains(CompanyName)).ToList();
            }
            return View("Index", startups);
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
