namespace SerhendKumbara.Core.Caching;

public static class CacheExtensions
{
    public static T Get<T>(this ICacheService cacheService, string key, Func<T> acquire)
    {
        if (cacheService.Any(key))
        {
            return cacheService.Get<T>(key);
        }

        var result = acquire();
        cacheService.Add(key, result);
        return result;
    }
}