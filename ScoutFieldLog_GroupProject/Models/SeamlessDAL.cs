using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private readonly string reCaptchaSecret;
        public SeamlessDAL(IConfiguration Configuration)
        {
            APIKey = Configuration.GetSection("APIKeys")["SeamlessAPIKey"];
            reCaptchaSecret = Configuration.GetSection("APIKeys")["reCAPTCHA"];
        }

        public async Task<bool> Recaptcha(string clientresponse)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.google.com/recaptcha/api/");

            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("secret", reCaptchaSecret));
            pairs.Add(new KeyValuePair<string, string>("response", clientresponse));

            FormUrlEncodedContent postData = new FormUrlEncodedContent(pairs);
            HttpResponseMessage response = await client.PostAsync("siteverify", postData);
            string output = await response.Content.ReadAsStringAsync();
            Console.WriteLine("json="+output);
            JObject json = JObject.Parse(output);
            Console.WriteLine("success=" + json["success"].ToString());
            bool success = json["success"].ToString().Equals("True");
            return success;
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
            SeamlessMasterList masterlistRoot = System.Text.Json.JsonSerializer.Deserialize<SeamlessMasterList>(seamlessMasterRootJSON);
            return masterlistRoot;
        }
    }
}
