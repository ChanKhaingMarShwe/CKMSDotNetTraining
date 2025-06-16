using CKMSDotNetTraining.Database.Models;
using CKMSDotNetTraining.Domain.features.Blog;
using CKMSDotNetTraining.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CKMSDotNetTraining.MvcApp.Controllers
{
    public class BlogAjaxController : Controller
    {
       private readonly IBlogService _blogService;

        public BlogAjaxController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [ActionName("Index")]
        public IActionResult BlogList()
            {
                var blogs = _blogService.GetAllBlogs();
                return View("BlogList",blogs);
            }


        [HttpGet]
        [ActionName("BlogList")]
        public IActionResult BlogGet() {
            var blogs = _blogService.GetAllBlogs();
            return Json(blogs);
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
            MessageModel model;
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


                model = new MessageModel(true, "Blog Create Successfully !"); 


            }catch(Exception ex)
            {
                TempData["isSuccess"] = false;
                TempData["Message"] = ex.ToString();
                model = new MessageModel(false, ex.ToString());
            }


            return Json(model);
        }



        public class MessageModel
        {
            public MessageModel()
            {
                isSuccess = false;
                Message = string.Empty;
            }
            public MessageModel(bool isSuccess, string message)
            {
                this.isSuccess = isSuccess;
                Message = message;
            }
            public bool isSuccess { get; set; }
            public string Message { get; set; } 
        }




        [HttpPost]
        [ActionName("Delete")]
        public  IActionResult BlogDelete(BlogRequestModel blogRequestModel)
        {
            MessageModel model;
            try
            {
                _blogService.DeleteBlog(blogRequestModel.id);
                model = new MessageModel(true, "Blog Delete Successfully !");


            }
            catch (Exception ex)
            {
                TempData["isSuccess"] = false;
                TempData["Message"] = ex.ToString();

                model = new MessageModel(false, ex.ToString());
            }


            return Json(model);
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
            MessageModel model;
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
                model = new MessageModel(true, "Blog Update Successfully !");


            }
            catch (Exception ex)
            {
                TempData["isSuccess"] = false;
                TempData["Message"] = ex.ToString();

                model = new MessageModel(false, ex.ToString());
            }


            return Json(model);
        }
    }
}
