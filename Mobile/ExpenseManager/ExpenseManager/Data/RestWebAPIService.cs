using ExpenseManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Data
{
    public class RestWebAPIService
    {
        HttpClient httpClient;

        public RestWebAPIService() {
            var handler = GetInsecureHandler();
            httpClient = new HttpClient(handler);
        }

        public async Task createUserAsync(User user)
        {
            string url = "https://10.0.2.2:5001/v1/api/user/create";
            //var uri = new Uri(string.Format(url, string.Empty));           
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\User created successfully.");
            }
            else {
                Debug.WriteLine("Error Occured!");
            }
        }

        public HttpClientHandler GetInsecureHandler()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

    }
}
