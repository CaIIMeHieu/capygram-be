namespace capygram.Post.Services
{
    public interface ICacheService
    {
        Task SetCache(string cacheKey, object response, TimeSpan timeOut);
        Task<string> GetCache(string cacheKey);
        Task RemoveCacheResponseAsync(string pattern);
    }
}
