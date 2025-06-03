using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CKMSDotNetTraining.ConsoleApp4
{
    public class RestClientExample
    {

        private readonly RestClient _client;
        private readonly string _baseUrl = "https://jsonplaceholder.typicode.com/posts";

        public RestClientExample()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true // For testing only
            };
            _client = new RestClient(handler);
        }
        public async Task Read()
        {
           
            RestRequest request = new RestRequest(_baseUrl, Method.Get);
            var response = await _client.ExecuteAsync(request); 
            if (response.IsSuccessStatusCode)
            {
                String jsonStr =  response.Content!;
                Console.WriteLine(jsonStr);
            }
        }

        public async Task Edit(int id)
        {
            

            RestRequest request = new RestRequest($"{_baseUrl}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Post with ID {id} not found.");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                String jsonStr =  response.Content!;
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

        
            RestRequest request = new RestRequest(_baseUrl, Method.Post);
            request.AddJsonBody(requestModel); // Add the request model as JSON body
            var response = await _client.ExecuteAsync(request); // Execute the request asynchronously

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse =  response.Content!; // Fix: Await the ReadAsStringAsync call to get the response content.
                Console.WriteLine("Post created successfully: " + jsonResponse);
            }
        }



        public async Task Update(int id, string tile, string body, int userId)
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

            RestRequest request = new RestRequest($"{_baseUrl}/{id}", Method.Patch);
            request.AddJsonBody(requestModel); // Add the request model as JSON body
            var response = await _client.ExecuteAsync(request); // Execute the request asynchronously

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse =  response.Content!; // Fix: Await the ReadAsStringAsync call to get the response content.
                Console.WriteLine("Post Updated successfully: " + jsonResponse);
            }
        }


        public async Task Delete(int id)
        {
           
            RestRequest request = new RestRequest($"{_baseUrl}/{id}", Method.Delete);
            var response = await _client.ExecuteAsync(request); // Execute the request asynchronously
         

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Delete with ID {id} not found.");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                String jsonStr = response.Content!;
                Console.WriteLine(jsonStr);
            }
        }

    }
}
