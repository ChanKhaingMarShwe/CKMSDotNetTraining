using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CKMSDotNetTraining.Shared
{
    public class HttpClientService: IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(string domainUrl)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(domainUrl)
            };
        }

        public async Task<T?> SendAsync<T>(string url, HttpMethodType method, Object? data = null)
        {
            var request = new HttpRequestMessage(new HttpMethod(method.ToString()), url);

            if (data != null)
            {
                string jsonStr = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(jsonStr, Encoding.UTF8, Application.Json);
            }

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseStr);
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }

    public enum HttpMethodType
    {
        Get,
        Post,
        Put,
        Patch,
        Delete
    }
}
