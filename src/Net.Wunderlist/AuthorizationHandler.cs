using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Wunderlist
{
	internal sealed class AuthorizationHandler : DelegatingHandler
	{
		private readonly string accessToken, clientId;

		public AuthorizationHandler(HttpClientHandler innerHandler, string accessToken, string clientId)
			: base(innerHandler)
		{
			if (innerHandler != null)
			{
				innerHandler.AllowAutoRedirect = false;
				innerHandler.UseCookies = false;
			}
			this.accessToken = accessToken;
			this.clientId = clientId;
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			request.Headers.Add("X-Client-ID", clientId);
			request.Headers.Add("X-Access-Token", accessToken);
			return base.SendAsync(request, cancellationToken);
		}
	}
}
