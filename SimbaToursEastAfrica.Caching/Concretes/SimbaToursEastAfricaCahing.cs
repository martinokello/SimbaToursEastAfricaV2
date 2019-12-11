using SimbaToursEastAfrica.Caching.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace SimbaToursEastAfrica.Caching.Concretes
{
    public class SimbaToursEastAfricaCahing: ICaching
    {
        public MemoryCache CacheObject { get; set; } = new MemoryCache(new MemoryCacheOptions());


        public T GetOrSaveToCache<T>(T cachedObject,string key, int timeInMinutes, Func<T> ResolveCache)
        {
            object result = CacheObject.Get(key);
            if (object.Equals(result, default(T)))
            {
                T fromCache = ResolveCache.Invoke();
                CacheObject.Set(key, fromCache, DateTime.Now.AddMinutes(timeInMinutes));
                return fromCache;
            }
            return (T) result; 
        }
    }
}
