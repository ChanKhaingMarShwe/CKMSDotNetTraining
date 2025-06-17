using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKMSDotNetTraining.Shared
{
    public class RestClientService : IHttpClientService
    {
        private readonly RestClient _restClient; // Changed HttpClient to RestClient
        public RestClientService(String domainUrl)
        {
            _restClient = new RestClient(domainUrl);
        }

        public async Task<T?> SendAsync<T>(string url, HttpMethodType method, object? data = null)
        {
            RestRequest request = new RestRequest(url, (Method)method);

            if (data != null)
            {
                string jsonStr = System.Text.Json.JsonSerializer.Serialize(data);
                request.AddJsonBody(jsonStr);
            }

            var response = await _restClient.ExecuteAsync(request); // Specified type argument explicitly
            if (response.IsSuccessStatusCode)
            {
                var responseStr = response.Content!;
                return JsonConvert.DeserializeObject<T>(responseStr);
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode} - {response.ErrorMessage}");
            }
        }
    }
}
