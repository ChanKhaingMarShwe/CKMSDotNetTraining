using CKMSDotNetTraining.Database.Models;
using CKMSDotNetTraining.Domain.features.Blog;
using Microsoft.AspNetCore.Mvc;

namespace CKMSDotNetTraining.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServiceController : Controller
    {
        private readonly BlogService _blogService;


        public BlogServiceController()
        {
            _blogService = new BlogService();
        }



        [HttpGet]
        public IActionResult GetAllBlogs()
        {
            var blogs = _blogService.GetAllBlogs();
            return Ok(blogs);
        }



        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var blog = _blogService.GetBlog(id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }



        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
          
            var model = _blogService.CreateBlog(blog);
            return Ok(model);
        }



        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
          var model= _blogService.UpdateBlog(id, blog);
            
            if(model is null)
            {
                return NotFound();
            }
            return Ok(model);
        }



        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {


            var model = _blogService.PatchBlog(id,blog);
            if (model is null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var blog = _blogService.DeleteBlog(id);
            if (blog is null)
            {
                return NotFound();
            }
            return Ok(); 
        }
    }
}
