namespace API.RickAndMorty.Interfaces.IServices
{
    public interface ICacheService
    {
        object? GetCacheByKey(string cacheKey);
        void AddCache(string cacheKey, object value, int timeFromMinutes = 10);
    }
}
