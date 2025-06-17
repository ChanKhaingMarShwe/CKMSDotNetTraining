using CKMSDotNetTraining.Project1.MvcApp.Models;
using CKMSDotNetTraining.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CKMSDotNetTraining.Project1.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientService _httpClientService;

        public HomeController(ILogger<HomeController> logger,IHttpClientService httpClientService)
        {
            _logger = logger;
            _httpClientService = httpClientService;

        }

        public async Task<IActionResult> Index()
        {
           var lst= await _httpClientService.SendAsync<List<Blog>>("api/blog", HttpMethodType.Get);
            return View(lst);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class Blog
    {

        public string Title { get; set; }
        public string Content { get; set; }

    }
}
