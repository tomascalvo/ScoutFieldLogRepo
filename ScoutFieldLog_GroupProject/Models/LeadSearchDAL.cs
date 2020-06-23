using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace ScoutFieldLog_GroupProject.Models
{
    public class LeadSearchDAL
    {
        private readonly string APIKey;

        public LeadSearchDAL(IConfiguration Configuration)
        {
            APIKey = Configuration.GetSection("APIKeys")["StartupSearchAPIKey"];
        }
        public HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.googleapis.com");
            //client.DefaultRequestHeaders.Authorization =
            //    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", APIKey);
            return client;
        }
        public async Task<LeadSearch> GetLeadSearchJSON()
        {
            string resource = $"/customsearch/v1?key=AIzaSyC-R_jQ4FQ6YQh8CSdH8RciCWVvgvvwoII&cx=001253566831710220080:3uzfdcbfm2y&q=startup";
            var client = GetClient();
            var response = await client.GetAsync(resource);
            var leadSearchJSON = await response.Content.ReadAsStringAsync();
            LeadSearch leadSearch = JsonSerializer.Deserialize<LeadSearch>(leadSearchJSON);
            return leadSearch;
        }
    }
}
