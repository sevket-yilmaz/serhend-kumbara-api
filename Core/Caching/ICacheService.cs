namespace SerhendKumbara.Core.Caching;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> generatorAsync, DateTime expiredTime = default);
    T Get<T>(string key);
    bool Any(string key);
    void Add(string key, object value);
    void Remove(string key);
    void Clear();
    void StartsWithClear(string prefix);
    List<string> GetAllKeys();
}