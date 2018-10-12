using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Caching.Interfaces
{
    public interface ICaching
    {
        MemoryCache CacheObject { set; get; }
        T GetOrSaveToCache<T>(T cachedObject, string key, int timeInMinutes, Func<T> ResolveCache);
    }
}
