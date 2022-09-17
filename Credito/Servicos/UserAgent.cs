using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Credito.Servicos
{
    public class UserAgent : IUserAgent
    {
        private readonly HttpClient _httpClient;

        public UserAgent(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            result = result.Replace("[", "").Replace("]", "");

            AssureSuccess(response, request.RequestUri);

            return JsonConvert.DeserializeObject<T>(result);
        }


        public async Task<byte[]> GetAsByteArrayAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);

            var result = await response.Content.ReadAsByteArrayAsync();

            AssureSuccess(response, request.RequestUri);

            return result;
        }

        public async Task<T> PostAsJsonAsync<T>(string url, T content)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, stringContent);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<T> PostAsJsonAsync<T>(string url, string content, bool useToken = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            AssureSuccess(response, request.RequestUri);

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, string content, Dictionary<string, string> headers, bool useToken = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            AssureSuccess(response, request.RequestUri);

            return response;
        }

        public async Task<T> PutAsJsonAsync<T>(string url, string content, bool useToken = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);

            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            AssureSuccess(response, request.RequestUri);

            return JsonConvert.DeserializeObject<T>(result);
        }

        #region Metodos Privados
        private void AssureSuccess(HttpResponseMessage message, Uri endpointPrimary)
        {
            if (message.IsSuccessStatusCode)
                return;

            var responseContent = message.Content?.ReadAsStringAsync().Result ?? string.Empty;
            throw new HttpRequestException(responseContent);
        }
        #endregion
    }
}