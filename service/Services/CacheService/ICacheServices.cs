using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.CacheService
{
    public interface ICacheServices
    {
        Task SetCacheResponseAsync(string key, object response, TimeSpan timeTolLive);
        Task <string> GetCacheResponseAsync(string key);
    }
}
