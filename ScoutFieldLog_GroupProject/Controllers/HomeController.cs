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

        public HomeController(IConfiguration iconfig, ApplicationDbContext identityContext, ScoutFieldLogDbContext context, StartupMatchDbContext smDBcontext, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _identityContext = identityContext;
            _signInManager = signInManager;
            DAL = new SeamlessDAL(iconfig);
            startupMatch = new StartupMatch( smDBcontext );
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

        // ConnectorView
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
        // ConnectorView Partial Views
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


        public IActionResult _SimilarStartupsPartialView(int companyId)

        {
            var company = _context.StartUpCompanies.Find(companyId);
            ViewBag.CompanyId = companyId;
            ViewBag.CompanyName = company.CompanyName;
            ViewBag.TwoLineSummary = company.TwoLineSummary;
            ViewBag.Keywords = company.Keywords;

            List<RecommendedAlignment> ra = 
                startupMatch.getSimilarCompanies(companyId, company.Keywords);
            return PartialView(ra);
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
            //var company = _context.StartUpCompanies.SingleOrDefault(c => c.Id == companyId);
            var company = _context.StartUpCompanies.Find(companyId);
            return View(company);
        }
        public IActionResult EditCompany(int companyId)
        {
            var company = _context.StartUpCompanies.Find(companyId);
            return View(company);
        }
        [HttpPost]

        public IActionResult EditCompany(StartUpCompanies company, string[] PartnerCompany)
        {
            company.Alignments = StartupMatch.convertListToString(PartnerCompany, ",");
            company.Keywords =
                StartupMatch.getKeywordString(company.TwoLineSummary);

            _context.Update(company);
            _context.SaveChanges();
            startupMatch.refreshCompanyCache();
            //ViewBag.message = "Company record updated.";
            return RedirectToAction("CompanyDetails", new { companyId = company.Id });
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
                startup.Keywords =
                    StartupMatch.getKeywordString(startup.TwoLineSummary);
                _context.Add(startup);
                _context.SaveChanges();
                token = null;
                startup = null;
                ViewBag.message = "Thank you for submitting your startup company!";
            } else
            {
                ViewBag.message = "Token not valid";
            }
            return View();
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
