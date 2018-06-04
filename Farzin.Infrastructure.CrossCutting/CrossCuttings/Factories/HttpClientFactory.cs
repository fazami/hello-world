using System.Net;
using System.Net.Http;

namespace Farzin.Infrastructure.CrossCutting.Factories
{
    public static class HttpClientFactory
    {
        public static HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            return httpClient;
        }
        public static HttpClient CreateHttpClient(ProxyConfiguration proxyConfig)
        {
            string proxyUri = proxyConfig.Address + ":" + proxyConfig.Port;
            HttpClient httpClient;

            if (proxyConfig == null)
            {
                httpClient = new HttpClient();
            }
            else
            {
                var proxyCreds = new NetworkCredential();
                if (string.IsNullOrEmpty(proxyConfig.Username) == false)
                {
                    proxyCreds.UserName = proxyConfig.Username;
                    proxyCreds.Password = proxyConfig.Password;
                }
                var proxy = new WebProxy(proxyUri, false)
                {
                    UseDefaultCredentials = false,
                    Credentials = proxyCreds,
                };

                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = proxy,
                    PreAuthenticate = true,
                    UseDefaultCredentials = false,
                };

                httpClient = new HttpClient(httpClientHandler);
            }
            return httpClient;
        }
    }
    public class ProxyConfiguration
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
