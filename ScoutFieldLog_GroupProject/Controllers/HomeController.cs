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
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        public IActionResult ConnectorView()
        {
            var allRecords = _context.StartUpCompanies.ToList();
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
            return View(ra);
        }
        [HttpPost]
        public async Task<IActionResult> ListStartUpProjects(string companyName)
        {
            SeamlessProjectList spl = await DAL.GetProjects();
            List<SeamlessProject> results = spl.records.ToList();
            List<SeamlessProject> projectList = results.Where(x => x.fields.StartupEngaged == companyName).ToList<SeamlessProject>();
            //List<SeamlessProject> projectList = results.ToList<SeamlessProject>();
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
                var twoLineSummaryMatches =
                    _context.StartUpCompanies.Where(x => x.TwoLineSummary.Contains(searchString)).ToList();
                searchResults.AddRange(twoLineSummaryMatches);
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
            if(company.Alignments == null)
            {
                company.Alignments = "";
            }
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
