using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CKMSDotNetTraining.Project1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var blogs = new List<Blog>
            {
                new Blog { Title = "First Blog", Content = "This is the content of the first blog." },
                new Blog { Title = "Second Blog", Content = "This is the content of the second blog." }
            };
            return Ok(blogs);

        }
    }

    public class Blog
    {
       
        public string Title { get; set; }
        public string Content { get; set; }
       
    }
}
