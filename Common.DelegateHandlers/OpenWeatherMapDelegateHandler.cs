using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Common.DelegateHandlers
{
    public class OpenWeatherMapDelegateHandler : DelegatingHandler
    {
        private readonly IConfiguration _configuration;

        public OpenWeatherMapDelegateHandler(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = this._configuration["OpenWeatherMap:ApiKey"];
            
            if (!string.IsNullOrEmpty(token))
            {
                var uriBuilder = new UriBuilder(request.RequestUri);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["appId"] = token;
                uriBuilder.Query = query.ToString();
                request.RequestUri = uriBuilder.Uri;
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}