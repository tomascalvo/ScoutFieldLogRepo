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

        public IActionResult NoCompanyFound()
        {
            return View();
        }

        // ConnectorView & Partial Views
        [Authorize]
        public IActionResult ConnectorView2()
        {
            var allRecords = _context.StartUpCompanies.ToList();
            return View("ConnectorView", allRecords);
        }

        [Authorize]
        public IActionResult ConnectorView()
        {
            var allRecords = _context.StartUpCompanies.ToList();
            return View("ConnectorView2", allRecords);
        }

        public IActionResult _LeadDetails(int companyId)
        {
            var company = _context.StartUpCompanies.Find(companyId);
            if (company == null)
            {
                return NoCompanyFound();
            }
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
        public IActionResult SimilarStartupsPartialView(int companyId)
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
        [HttpPost]
        public async Task<IActionResult> ListStartUpProjects(string companyName, bool partial=false)
        {
            SeamlessProjectList spl = await DAL.GetProjects();
            List<SeamlessProject> results = spl.records.ToList();
            List<SeamlessProject> projectList = results.Where(x => x.fields.StartupEngaged == companyName).ToList<SeamlessProject>();
            if (partial)
                PartialView(projectList);
            return PartialView(projectList);
        }
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
            if(TempData["updateConfirmation"] != null)
            {
                ViewBag.Message = TempData["updateConfirmation"].ToString();
            }
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
            TempData["updateConfirmation"] = "The company record has been updated successfully.";
            return View(company);
        }

        public IActionResult CompanyDetailsPartial(int companyId)
        {
            var company = _context.StartUpCompanies.Find(companyId);
            return PartialView(company);
        }
        //retrieveing info from the database ****_context****
        public IActionResult EditCompanyPartial(int companyId)
        {
            ViewBag.partners = _context.PartnerCompany.Select(p => p.CompanyName).ToList();
            ViewBag.themes = _context.Theme.Select(t => t.Name).ToList();
            ViewBag.landscapes = _context.Landscape.Select(l => l.Name).ToList();
            ViewBag.technologyAreas = _context.TechnologyArea.Select(t => t.Name).ToList();
            var company = _context.StartUpCompanies.Find(companyId);
            return PartialView(company);
        }

        [HttpPost]
        public IActionResult EditCompany(StartUpCompanies company, string[] PartnerCompany, string[] selectedThemes, string[] selectedLandscapes, string[] selectedTechnologyAreas)
        {
            // Checkbox logic
            company.Alignments = StartupMatch.convertListToString(PartnerCompany, ", ");
            company.Themes = StartupMatch.convertListToString(selectedThemes, ", ");
            company.Landscapes = StartupMatch.convertListToString(selectedLandscapes, ", ");
            company.TechnologyAreas = StartupMatch.convertListToString(selectedTechnologyAreas, ", ");
            // Keywords assignment
            company.Keywords =
                StartupMatch.getKeywordString(company.TwoLineSummary);

            _context.Update(company);
            _context.SaveChanges();
            startupMatch.refreshCompanyCache();
            TempData["updateConfirmation"] = "The company record has been updated successfully.";
            return RedirectToAction("CompanyDetails", new { companyId = company.Id });
        }

        [HttpPost]
        public IActionResult EditCompanyPartial(StartUpCompanies company, string[] PartnerCompany, string[] selectedThemes, string[] selectedLandscapes, string[] selectedTechnologyAreas)
        {
            // Checkbox logic
            company.Alignments = StartupMatch.convertListToString(PartnerCompany, ",");
            company.Themes = StartupMatch.convertListToString(selectedThemes, ",");
            company.Landscapes = StartupMatch.convertListToString(selectedLandscapes, ",");
            company.TechnologyAreas = StartupMatch.convertListToString(selectedTechnologyAreas, ",");
            // Keywords assignment
            company.Keywords =
                StartupMatch.getKeywordString(company.TwoLineSummary);

            _context.Update(company);
            _context.SaveChanges();
            startupMatch.refreshCompanyCache();
            ViewBag.message = "Company record updated.";
            return RedirectToAction("CompanyDetails", new { companyId = company.Id });
        }

        //displays the scout form
        [HttpGet]
        public IActionResult ScoutForm()
        {
            return PartialView();
        }

        [HttpPost]
        //string token used for recaptcha
        public async Task<IActionResult> ScoutForm(StartUpCompanies startup, string token)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.message = "Certain required forms not completed";
                return View();
            }
            //server will have modelstate validation, client side will have html like min, max, input type.
            
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

        // Evaluation CRUD
        // Create Evaluation
        [HttpGet]
        public IActionResult CreateEvaluation(int companyId, bool partial=false)
        {
            var company = _context.StartUpCompanies.Find(companyId);
            ViewBag.CompanyId = company.Id;
            ViewBag.CompanyName = company.CompanyName;
            ViewBag.CompanySummary = company.TwoLineSummary;
            ViewBag.partners = _context.PartnerCompany.Select(p => p.CompanyName).ToList();
            ViewBag.themes = _context.Theme.Select(t => t.Name).ToList();
            ViewBag.landscapes = _context.Landscape.Select(l => l.Name).ToList();
            ViewBag.technologyAreas = _context.TechnologyArea.Select(t => t.Name).ToList();
            if (partial)
                return PartialView();
            else
                return View();
        }
        [HttpPost]
        public IActionResult CreateEvaluation(Evaluation newEvaluation, int companyId, string[] selectedAlignments, string[] selectedThemes, string[] selectedLandscapes, string[] selectedTechnologyAreas, bool partial=false)
        {
            newEvaluation.Alignments = StartupMatch.convertListToString(selectedAlignments, ", ");
            newEvaluation.Themes = StartupMatch.convertListToString(selectedThemes, ", ");
            newEvaluation.Landscapes = StartupMatch.convertListToString(selectedLandscapes, ", ");
            newEvaluation.TechnologyAreas = StartupMatch.convertListToString(selectedTechnologyAreas, ", ");
            _context.Add(newEvaluation);
            _context.SaveChanges();
            TempData["evaluationCreated"] = "Thank you for your evaluation, " + User.Identity.Name + ".";
            
            if (partial)
                return RedirectToAction("ReviewEvaluation", new { evaluationId = newEvaluation.Id, partial=true });
            else return RedirectToAction("ReviewEvaluation", new { evaluationId = newEvaluation.Id });
        }
        //Review Evaluation
        public IActionResult ReviewEvaluation(int evaluationId, bool partial=false)
        {
            if (TempData["evaluationCreated"] != null)
            {
                ViewBag.Message = TempData["evaluationCreated"].ToString();
            }
            var evaluationToReview = _context.Evaluation.First(e => e.Id.Equals(evaluationId));
            var company = _context.StartUpCompanies.First(c => c.Id.Equals(evaluationToReview.StartUpCompaniesId));
            ViewBag.CompanyId = company.Id;
            ViewBag.CompanyName = company.CompanyName;
            ViewBag.CompanySummary = company.TwoLineSummary;

            if (partial)
                return PartialView(evaluationToReview);
            return View(evaluationToReview);
        }
        // Review & Search Evaluations
        public IActionResult ReviewEvaluations(bool partial=false)
        {
            var allEvaluations = _context.Evaluation.ToList();
            if (partial)
                return PartialView(allEvaluations);
            return View(allEvaluations);
        }
        //Review Aggregate Evaluation Data
       public IActionResult AnalyzeStartup(int companyId, bool partial=false)
        {
            var startUpCo = _context.StartUpCompanies.First(s => s.Id.Equals(companyId));
            ViewBag.CompanyId = startUpCo.Id;
            ViewBag.CompanyName = startUpCo.CompanyName;
            ViewBag.CompanySummary = startUpCo.TwoLineSummary;
            var evaluations = _context.Evaluation.Where(e => e.StartUpCompaniesId.Equals(companyId)).ToList();
            var partners = _context.PartnerCompany.ToList();
            var themes = _context.Theme.ToList();
            var landscapes = _context.Landscape.ToList();
            var technologyAreas = _context.TechnologyArea.ToList();
            Analysis analysis = new Analysis(evaluations, partners, themes, landscapes, technologyAreas);

            if (partial)
                return PartialView(analysis);
            else
                return View(analysis);
        }

        // Update Evaluation

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
