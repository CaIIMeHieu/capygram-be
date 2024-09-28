namespace capygram.Common.Services
{
    public interface IExternalService
    {
        Task<T> GetExternalDataAsync<T>( string endPoint );
        Task<List<T>> GetExternalDataListAsync<T>( string endPoint );
    }
}
