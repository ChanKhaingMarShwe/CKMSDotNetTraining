using CKMSDotNetTraining.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CKMSDotNetTraining.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Hellow from viewbag !";

            HomeResponseModel model = new HomeResponseModel();
            model.Message = "Hello from model !";
            return View(model);
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
}
