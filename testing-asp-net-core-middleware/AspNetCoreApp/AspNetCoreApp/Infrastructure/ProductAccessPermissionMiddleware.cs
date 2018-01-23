using AspNetCoreApp.Services;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspNetCoreApp.Infrastructure
{
    public class ProductAccessPermissionMiddleware
    {
        private readonly RequestDelegate _next;
        private Regex _regex;

        public ProductAccessPermissionMiddleware(RequestDelegate next)
        {
            _regex = new Regex("\\/products\\/(\\d{1,}).*");
            _next = next;
        }

        public async Task Invoke(HttpContext context, IProductService productService)
        {
            var match = _regex.Match(context.Request.Path);

            if (match.Success)
            {
                var productId = int.Parse(match.Groups[1].Value);

                var hasAccess = productService.DoesLoggedUserHaveAccessForProduct(productId);

                if (!hasAccess)
                {
                    context.Response.StatusCode = 401;
                }
                else
                {
                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
