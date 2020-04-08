using ExpenseManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Service
{
    public class RestAPIService
    {
        private static string IP = "192.168.1.13";
        private static string PORT = "45455";
        private static string WEB_API_BASE_URL = "https://"+IP+":"+PORT+"/v1/api/";
        private static string HOSTNAME = "CN="+IP;
        HttpClient httpClient;

        public RestAPIService()
        {
            var handler = getInsecureHandler();
            httpClient = new HttpClient(handler);
        }

        public HttpClientHandler getInsecureHandler()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals(HOSTNAME))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        private async Task<Response> getHTTPResponse(HttpResponseMessage response) {
            string result = await response.Content.ReadAsStringAsync();
            Response responseObject = JsonConvert.DeserializeObject<Response>(result);
            return responseObject;
        }

        private User getUserFromResponse(Response response) {
            string userString = response.data;
            User user = JsonConvert.DeserializeObject<User>(userString);
            return user;
        }

        //404, 500, 200
        public async Task<bool> createUserAsync(User user)
        {
            string url = WEB_API_BASE_URL+"user/create";
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"\User created successfully.");
                Response responseObject = await getHTTPResponse(response);
                return responseObject.status;                          
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return false;                
            }
        }

    }
}
