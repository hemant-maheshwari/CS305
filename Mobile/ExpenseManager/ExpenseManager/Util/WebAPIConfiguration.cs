using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ExpenseManager.Util
{
    public class WebAPIConfiguration
    {
        protected static string IP = "localhost";
        protected static string PORT = "44384";
        protected static string WEB_API_BASE_URL = "https://" + IP + ":" + PORT + "/v1/api/";
        protected static string HOSTNAME = "CN=" + IP;
        protected static string LOCALHOST = "CN=localhost";

        protected HttpClient httpClient;
        

        public WebAPIConfiguration() {
            var handler = getInsecureHandler();
            httpClient = new HttpClient(handler);
        }

        public HttpClientHandler getInsecureHandler()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
               // if (cert.Issuer.Equals(HOSTNAME) || cert.Issuer.Equals(LOCALHOST))
                    return true;
               // return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

    }
}
