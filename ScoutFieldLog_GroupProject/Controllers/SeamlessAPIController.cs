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
    public class SeamlessAPIController : ControllerBase
    {
        private SeamlessDAL DAL;
        public SeamlessAPIController(IConfiguration Configuration)
        {
            DAL = new SeamlessDAL(Configuration);
        }
       //[Authorize]
       [HttpGet]
       public async Task<SeamlessMasterList> GetMasterList()
        {
            SeamlessMasterList masterList = await DAL.GetMasterListJSON();
            return masterList;
        }
    }
}