using CKMSDotNetTraining.Database.Models;
using CKMSDotNetTraining.Domain.features.Blog;
using CKMSDotNetTraining.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CKMSDotNetTraining.MvcApp.Controllers
{
    public class BlogController : Controller
    {
       private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
            {
                var blogs = _blogService.GetAllBlogs();
                return View(blogs);
            }

        [ActionName("Create")]//route: /Blog/Create
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult BlogSave(BlogRequestModel blogRequestModel)
        {

            try
            {
                _blogService.CreateBlog(new TblBlog
                {
                    BlogTitle = blogRequestModel.Title,
                    BlogAuthor = blogRequestModel.Author,
                    BlogContent = blogRequestModel.Content
                });

                TempData["isSuccess"] = true;
                TempData["Message"] = "Blog Create Successfully !";


            }catch(Exception ex)
            {
                TempData["isSuccess"] = false;
                TempData["Message"] = ex.ToString();
            }


            return RedirectToAction("index");
        }

        [ActionName("Delete")]
        public  IActionResult BlogDelete(int id)
        {
            _blogService.DeleteBlog(id);
            return RedirectToAction("index");
        }

        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            var blog=_blogService.GetBlog( id);
            BlogRequestModel blogRequestModel = new BlogRequestModel
            {
                id = blog.BlogId,
                Title = blog.BlogTitle,
                Author = blog.BlogAuthor,
                Content = blog.BlogContent
            };
            

            return View("BlogEdit", blogRequestModel);
        }



        [HttpPost]
        [ActionName("Update")]
        public IActionResult BlogUpdate(int id,BlogRequestModel blogRequestModel)
        {

            try
            {
                _blogService.UpdateBlog(id,new TblBlog
                {
                    BlogTitle = blogRequestModel.Title,
                    BlogAuthor = blogRequestModel.Author,
                    BlogContent = blogRequestModel.Content
                });

                TempData["isSuccess"] = true;
                TempData["Message"] = "Blog Update Successfully !";


            }
            catch (Exception ex)
            {
                TempData["isSuccess"] = false;
                TempData["Message"] = ex.ToString();
            }


            return RedirectToAction("index");
        }
    }
}
