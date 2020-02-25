using Microsoft.AspNetCore.Http;

namespace Common.Extensions
{
    public static class WebExtensions
    {
        /// <summary>
        /// Extension for HttpContext to get base url
        /// </summary>
        /// <param name="httpContext">Current request context</param>
        /// <returns>Base url without / in the end. For example: https://example.com</returns>
        public static string GetBaseUrl(this HttpContext httpContext)
        {
            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
        }
    }
}