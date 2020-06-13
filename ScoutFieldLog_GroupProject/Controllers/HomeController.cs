using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ScoutFieldLog_GroupProject.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ScoutFieldLog_GroupProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScoutFieldLogDbContext _context;
        private SeamlessDAL DAL;

        public HomeController(IConfiguration iconfig, ScoutFieldLogDbContext context)

        {
            _context = context;
            DAL = new SeamlessDAL(iconfig);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CompanyDetails(int? companyId)
        {
            if (companyId == null)
            {
                throw new System.Exception("Company ID is needed to view company details.");
            }
            StartUp company = _context.StartUp.Find(companyId);
            if(company == null)
            {
                throw new Exception("There is no record of a company with that ID number.");
            }
            return View(company);
        }
        [HttpGet]
        public IActionResult CompanyEdit()
        {
            return View();
        }
        [HttpPut]
        public IActionResult CompanyEdit(StartUp company)
        {
            var existingCompany = _context.StartUp.Where(c => c.Id == company.Id);
            _context.Update(company);
            _context.SaveChanges();
            return View(company);
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
        public async Task<IActionResult> ScoutForm(StartUp startup, string token)
        {
            //ViewBag.message = DAL.Recaptcha(startup.Status);
            if (await DAL.Recaptcha(token)) {
                startup.Status = "";//clear the token so we don't save to DB
                _context.Add(startup);
                _context.SaveChanges();
                ViewBag.message = "Thank you for submitting your startup company!";
            } else
            {
                ViewBag.message = "Token not valid";
            }
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
