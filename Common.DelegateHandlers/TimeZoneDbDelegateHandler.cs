using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace Common.DelegateHandlers
{
    public class TimeZoneDbDelegateHandler : DelegatingHandler
    {
        private readonly IConfiguration _configuration;

        public TimeZoneDbDelegateHandler(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = this._configuration["TimeZoneDb:ApiKey"];

            if (!string.IsNullOrEmpty(token))
            {
                var uriBuilder = new UriBuilder(request.RequestUri);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["key"] = token;
                query["format"] = "json";
                uriBuilder.Query = query.ToString();
                request.RequestUri = uriBuilder.Uri;
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}