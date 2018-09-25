using SimbaToursEastAfrica.Caching.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.Caching.Concretes
{
    public class SimbaToursEastAfricaCahing : ICaching
    {
        public ObjectCache CacheObject { get; set; } = new MemoryCache("SimbaToursEastAfricaCaching");


        public T GetOrSaveToCache<T>(T cachedObject,string key, int timeInMinutes, Func<T> ResolveCache)
        {
            var result = CacheObject.Get(key);
            if (result.Equals(default(T)))
            {
                T fromCache = ResolveCache.Invoke();
                CacheObject.Add(key, fromCache, DateTime.Now.AddMinutes(timeInMinutes));
                return fromCache;
            }
            return default(T); 
        }
    }
}
