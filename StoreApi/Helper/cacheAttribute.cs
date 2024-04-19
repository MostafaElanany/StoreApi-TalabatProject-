using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using service.Services.CacheService;
using System.Text;

namespace StoreApi.Helper
{
    public class cacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timetoliveinsecs;

        public cacheAttribute(int timetoliveinsecs)
        {
           _timetoliveinsecs = timetoliveinsecs;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _cacheservice = context.HttpContext.RequestServices.GetRequiredService<ICacheServices>();
            var cacheKey = GenerateCacheKeyRequest(context.HttpContext.Request);
            var cacheresponse = await _cacheservice.GetCacheResponseAsync(cacheKey);

            if(!string.IsNullOrEmpty(cacheresponse))
            {

                var contentResult = new ContentResult
                {
                    Content = cacheresponse,
                    ContentType = "application/json",
                    StatusCode = 200

                };
                context.Result= contentResult;
                return;
            }

            var executedContext = await next();
            if(executedContext.Result is OkObjectResult response ) 
             {
                await _cacheservice.SetCacheResponseAsync(cacheKey, response.Value, TimeSpan.FromSeconds(_timetoliveinsecs));

             }
        }

        private string GenerateCacheKeyRequest(HttpRequest request)
        {
            StringBuilder cachekey = new StringBuilder();
            cachekey.Append($"{request.Path}");
            foreach (var (key,value)in request.Query.OrderBy(x=>x.Key))
            {
                cachekey.Append($"|{key}-{value}");
            }
            return cachekey.ToString();
        }
     }
}
