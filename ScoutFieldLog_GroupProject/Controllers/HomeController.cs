using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Mvc;
using ScoutFieldLog_GroupProject.Data;
using ScoutFieldLog_GroupProject.Models;
using Microsoft.Extensions.Configuration;

namespace ScoutFieldLog_GroupProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _identityContext;
        private readonly ScoutFieldLogDbContext _context;
        private SeamlessDAL DAL;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(IConfiguration iconfig, ApplicationDbContext identityContext, ScoutFieldLogDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _identityContext = identityContext;
            _signInManager = signInManager;
            DAL = new SeamlessDAL(iconfig);
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

        public IActionResult CompanyList()
        {
            var companies = _context.StartUp.ToList();
            return View(companies);
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
