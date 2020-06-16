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

        // Leads display page for connectors. Has a list of leads and lead details using AJAX.______________________________

        //public IActionResult LeadsDisplay()
        //{
        //    var companies = _context.StartUp.ToList();
        //    return View(companies);
        //}

        //[Route("ListLeads")]
        //public IActionResult ListLeads()
        //{
        //    var companies = _context.StartUp.ToList();
        //    return PartialView("ListLeads",companies);
        //}
        
        //[Route("LeadDetails/{companyId}")]
        //public IActionResult LeadDetails(int companyId)
        //{
        //    var company = _context.StartUp.SingleOrDefault(c => c.Id == companyId);
        //    return new JsonResult(company);
        //}

        // End leads display page for connectors.___________________

        // Company CRUD

        [HttpPost]
        public IActionResult StartupSearch(string searchString)
        {
            List<StartUp> searchResults;
            if (searchString is null || searchString == "")
            {
                searchResults = _context.StartUp.ToList();
            }
            else
            {
                searchResults = _context.StartUp.Where(x => x.CompanyName.Contains(searchString)).ToList();
                // Search Parameters
                var twoLineSummaryMatches = _context.StartUp.Where(x => x.TwoLineSummary.Contains(searchString)).ToList();
                //searchResults = searchResults.Add(twoLineSummaryMatches);
            }
            return View("Index", searchResults);
        }

        public IActionResult ListCompanies()
        {
            var companies = _context.StartUp.ToList();
            return View(companies);
        }

        [Route("Home/CompanyDetails/companyId={companyId}")]
        public IActionResult CompanyDetails(int companyId)
        {
            var company = _context.StartUp.SingleOrDefault(c => c.Id == companyId);
            return View(company);
        }

        [Route("Home/EditCompany/companyId={companyId}")]
        public IActionResult EditCompany(int companyId)
        {
            var company = _context.StartUp.Find(companyId);
            return View(company);
        }

        [HttpPost]
        public IActionResult EditCompany(StartUp company)
        {
            _context.Update(company);
            _context.SaveChanges();
            //ViewBag.message = "Company record updated.";
            return RedirectToAction("Index");
        }

        // Evaluation CRUD

        [HttpGet]
        public IActionResult CreateEvaluation(int companyId)
        {
            var company = _context.StartUp.Find(companyId);
            return View(company);
        }

        [HttpPost]
        public IActionResult CreateEvaluation(StartUp company, Evaluation evaluation)
        {
            company.Evaluations.Add(evaluation);
            _context.Update(company);
            _context.SaveChanges();
            //ViewBag.message = "Company review added."
            return RedirectToAction("Index");
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
