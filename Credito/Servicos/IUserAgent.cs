using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Credito.Servicos
{
    public interface IUserAgent
    {

        Task<T> GetAsync<T>(string url);
        Task<byte[]> GetAsByteArrayAsync(string url);
        Task<T> PostAsJsonAsync<T>(string url, T content);
        Task<T> PostAsJsonAsync<T>(string url, string content, bool useToken = false);
        Task<T> PutAsJsonAsync<T>(string url, string content, bool useToken = false);
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, string content, Dictionary<string, string> headers, bool useToken = false);
    }
}
