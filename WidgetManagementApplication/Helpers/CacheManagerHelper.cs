using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace WidgetManagementApplication.Helpers
{
    public class CacheManagerHelper<T>
    {
        private readonly string cacheKey;
        private readonly ObjectCache cache;

        public CacheManagerHelper(int recordCount)
        {
            cacheKey = GetCacheKey(recordCount);
            cache = MemoryCache.Default;
        }

        public string GetCacheKey(int recordCount)
        {
            return $"WidgetMGMT_{recordCount}";
        }
        public List<T> GetData()
        {
            List<T> data = cache.Get(cacheKey) as List<T>;

            return data;
        }

        public void CacheData(List<T> data)
        {
            cache.Set(cacheKey, data, new CacheItemPolicy());
        }

        public void InvalidateCache()
        {
            cache.Remove(cacheKey);
        }

    }

}