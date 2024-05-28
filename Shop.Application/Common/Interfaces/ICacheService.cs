namespace Shop.Application.Common.Interfaces;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(T data, string key, TimeSpan? slidingExpiration = null);
}