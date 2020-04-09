using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ExpenseManager.Util
{
    public class WebAPIConfiguration
    {
        protected static string IP = "192.168.1.13";
        protected static string PORT = "45455";
        protected static string WEB_API_BASE_URL = "https://" + IP + ":" + PORT + "/v1/api/";
        protected static string HOSTNAME = "CN=" + IP;
        protected static string LOCALHOST = "localhost";

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
                if (cert.Issuer.Equals(HOSTNAME) || cert.Issuer.Equals(LOCALHOST))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

    }
}
