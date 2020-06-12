using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScoutFieldLog_GroupProject.Models
{
    public class SeamlessDAL
    {
        private readonly string APIKey;
        public SeamlessDAL(IConfiguration Configuration)
        {
            APIKey = Configuration.GetSection("APIKeys")["SeamlessAPIKey"];
        }
        public HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.airtable.com");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", APIKey);
            return client;
        }
        public async Task<SeamlessMasterList> GetMasterListJSON()
        {
            string resource = $"/v0/appFo187B73tuYhyg/Master%20List";
            var client = GetClient();
            var response = await client.GetAsync(resource);
            var seamlessMasterRootJSON = await response.Content.ReadAsStringAsync();
            SeamlessMasterList masterlistRoot = JsonSerializer.Deserialize<SeamlessMasterList>(seamlessMasterRootJSON);
            return masterlistRoot;
        }
    }
}
