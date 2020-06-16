using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ScoutFieldLog_GroupProject.Models;

namespace ScoutFieldLog_GroupProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadSearchController : ControllerBase
    {
        private LeadSearchDAL DAL;
        public LeadSearchController(IConfiguration Configuration)
        {
            DAL = new LeadSearchDAL(Configuration);
        }
        //[Authorize]
        [HttpGet]
        public async Task<LeadSearch> GetLeadSearch()
        {
            LeadSearch leadSearch = await DAL.GetLeadSearchJSON();
            return leadSearch;
        }

    }
}