
namespace CKMSDotNetTraining.Shared
{
    public interface IHttpClientService
    {
        Task<T?> SendAsync<T>(string url, HttpMethodType method, object? data = null);
    }
}