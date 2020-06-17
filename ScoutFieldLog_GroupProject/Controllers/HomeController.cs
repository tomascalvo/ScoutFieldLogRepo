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
        private StartupMatch startupMatch;

        public HomeController(IConfiguration iconfig, ApplicationDbContext identityContext, ScoutFieldLogDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _identityContext = identityContext;
            _signInManager = signInManager;
            DAL = new SeamlessDAL(iconfig);
            startupMatch = new StartupMatch( _context );
            //startupMatch.refreshKeywords();
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

        public IActionResult ConnectorView()
        {
            // Leads Displays
            // All Records
            var allRecords = _context.StartUpCompanies.ToList();
            // Just Leads w/ Summaries
            var searchResults = _context.StartUpCompanies.Where(c => c.Status.Equals("Lead") && c.TwoLineSummary != null).ToList();
            // Fresh Leads
            var freshCutoffDay = DateTime.Today.AddDays(-7);
            var freshLeads = allRecords.Where(f => f.DateAssigned > freshCutoffDay); 
            // Recommended Leads
            string keyword = "medical";
            var recommendations = searchResults.Where(r => TextMatch.Program.getKeywords(r.TwoLineSummary).Contains(keyword));
            return View(allRecords);
        }

        public IActionResult _LeadDetails(int companyId)
        {
            if(companyId == null)
            {
                var firstResult = _context.StartUpCompanies.FirstOrDefault();
                return PartialView(firstResult);
            }
            var company = _context.StartUpCompanies.Find(companyId);
            return PartialView(company);
        }

        public IActionResult _SimilarStartupsPartialView()
        {
            var companies = _context.StartUpCompanies.ToList();
            return PartialView(companies);
        }
        [Route("Home/ListStartUpProjects/companyName={companyName}")]
        public async Task<IActionResult> ListStartUpProjects(string companyName)
        {
            SeamlessProjectList spl = await DAL.GetProjects();
            List<SeamlessProject> results = spl.records.ToList();
            List<SeamlessProject> projectList = results.Where(x => x.fields.StartupEngaged == companyName).ToList<SeamlessProject>();
            return View(projectList);
        }

        // Company CRUD

        [HttpPost]
        public IActionResult StartupSearch(string searchString)
        {
            List<StartUpCompanies> searchResults;
            if (searchString is null || searchString == "")
            {
                searchResults = _context.StartUpCompanies.ToList();
            }
            else
            {
                searchResults = _context.StartUpCompanies.Where(x => x.CompanyName.Contains(searchString)).ToList();
                // Search Parameters
                var twoLineSummaryMatches = _context.StartUpCompanies.Where(x => x.TwoLineSummary.Contains(searchString)).ToList();
                //searchResults = searchResults.Add(twoLineSummaryMatches);
            }
            return View(searchResults);
        }

        public IActionResult ListCompanies()
        {
            var companies = _context.StartUpCompanies.ToList();
            return View(companies);
        }


        public IActionResult CompanyDetails(int companyId)
        {
            var company = _context.StartUpCompanies.SingleOrDefault(c => c.Id == companyId);
            return View(company);
        }

        public IActionResult EditCompany(int companyId)
        {
            var company = _context.StartUpCompanies.Find(companyId);
            return View(company);
        }

        [HttpPost]
        public IActionResult EditCompany(StartUpCompanies company)
        {
            _context.Update(company);
            _context.SaveChanges();
            //ViewBag.message = "Company record updated.";
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
        public async Task<IActionResult> ScoutForm(StartUpCompanies startup, string token)
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
