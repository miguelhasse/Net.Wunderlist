using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace System.Net.Wunderlist
{
    public static class Authorization
    {
        public static Uri GetAuthorizationEndpoint(string clientId, Uri redirectUri, string state)
        {
            var builder = new UriBuilder("https://www.wunderlist.com/oauth/authorize");
            builder.Query = String.Format("client_id={0}&redirect_uri={1}&state={2}",
                clientId, Uri.EscapeDataString(redirectUri.AbsoluteUri), state);
            return builder.Uri;
        }

        public static HttpMessageHandler GetAuthorizationHandler(HttpClientHandler innerHandler, string accessToken, string clientId)
        {
            return new AuthorizationHandler(innerHandler ?? new HttpClientHandler(), accessToken, clientId);
        }

        public static async Task<string> TokenExchangeAsync(string clientId, string clientSecret, string code)
        {
            dynamic data = new { client_id = clientId, client_secret = clientSecret, code = code };
            var requestContent = new StringContent(JsonConvert.SerializeObject(data));

            using (var client = new HttpClient(new HttpClientHandler { UseCookies = false, UseDefaultCredentials = false }))
            {
                var response = await client.PostAsync("https://www.wunderlist.com/oauth/access_token", requestContent);
                string responseJson = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return ((dynamic)JsonConvert.DeserializeObject(responseJson)).access_token;
            }
        }
    }
}
