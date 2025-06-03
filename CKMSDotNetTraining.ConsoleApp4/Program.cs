// See https://aka.ms/new-console-template for more information
using CKMSDotNetTraining.ConsoleApp4;
using System.Net;

// Set before making any HTTP requests
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
Console.WriteLine("Hello, World!");
Console.ReadLine();


HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.Read();
//await httpClientExample.Edit(1);
//await httpClientExample.Edit(1000); 
//await httpClientExample.Create("New Post", "This is the body of the new post.", 1);
//await httpClientExample.Update(1, "Updated Title", "This is the updated body of the post.", 1);
//await httpClientExample.Delete(1);

RestClientExample restClientExample = new RestClientExample();
//await restClientExample.Read();
//await restClientExample.Edit(1);
//await restClientExample.Update(1,"Updated title","Update Body",1);
//await restClientExample.Create("New Post", "This is the body of the new post.", 1);
//await restClientExample.Delete(1);

RefitExample refitExample = new RefitExample();
await refitExample.Run();