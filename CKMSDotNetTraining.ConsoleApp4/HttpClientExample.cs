using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CKMSDotNetTraining.ConsoleApp4
{
    public class HttpClientExample
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "https://jsonplaceholder.typicode.com/posts";

        public HttpClientExample()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // For testing only
            };
            _client = new HttpClient(handler);
        }
        public async Task Read()
        {
            HttpResponseMessage response = await _client.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                String jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }

        public async Task Edit(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{_baseUrl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Post with ID {id} not found.");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                String jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }

        public async Task Create(string tile, string body, int userId)
        {
            PostModel requestModel = new PostModel()
            {
                userId = userId,
                title = tile,
                body = body
            };

            var JsonRequest = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(JsonRequest, Encoding.UTF8, Application.Json);
            
            using HttpResponseMessage response = await _client.PostAsync(_baseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse =  await response.Content.ReadAsStringAsync(); // Fix: Await the ReadAsStringAsync call to get the response content.
                Console.WriteLine("Post created successfully: " + jsonResponse);
            }
        }



        public async Task Update(int id,string tile, string body, int userId)
        {
            PostModel requestModel = new PostModel()
            {
                id = id, 
                userId = userId,
                title = tile,
                body = body
            };

            var JsonRequest = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(JsonRequest, Encoding.UTF8, Application.Json);

            
            using HttpResponseMessage response = await _client.PatchAsync($"{_baseUrl}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse =  await response.Content.ReadAsStringAsync(); // Fix: Await the ReadAsStringAsync call to get the response content.
                Console.WriteLine("Post Updated successfully: " + jsonResponse);
            }
        }


        public async Task Delete(int id)
        {
            using HttpResponseMessage response = await _client.DeleteAsync($"{_baseUrl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Delete with ID {id} not found.");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                String jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }


    }
}


public class PostModel
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
}



