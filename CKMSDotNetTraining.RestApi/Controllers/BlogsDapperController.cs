using CKMSDotNetTraining.RestApi.DataModels;
using CKMSDotNetTraining.RestApi.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;


namespace CKMSDotNetTraining.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=CKMSDotNetTraining;User ID=sa;Password=YourPassword123!;TrustServerCertificate=true;";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT 
    BlogId AS Id,
    BlogTitle AS Title,
    BlogAuthor AS Author,
    BlogContent AS Content,
    DeleteFlag AS DeleteFlag
FROM Tbl_blog
WHERE DeleteFlag = 0;
";
                List<BlogViewModel> list = db.Query<BlogViewModel>(query).ToList();

                if (list == null || list.Count == 0)
                {
                    return NotFound("No blogs found.");
                }

                return Ok(list);
            }
        }


        [HttpGet("id")]

        public IActionResult GetBlog(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT 
    BlogId AS Id,
    BlogTitle AS Title,
    BlogAuthor AS Author,
    BlogContent AS Content,
    DeleteFlag AS DeleteFlag
FROM Tbl_blog
WHERE DeleteFlag = 0 and BlogId=@id;
";
                BlogViewModel blog = db.QuerySingleOrDefault<BlogViewModel>(query, new { id });


                return Ok(blog);
            }
        }


        [HttpPost]

        public IActionResult CreateBlog(BlogViewModel blog)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[Tbl_blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@Title
           ,@Author
           ,@Content
           ,0)";

                db.Open();
                int result = db.Execute(query, new BlogViewModel
                {
                    Title = blog.Title,
                    Author = blog.Author,
                    Content = blog.Content
                });

                db.Close();
                return Ok(result == 1 ? "Saving Successfully !" : "Saving fail !");
            }



        }



        [HttpPut("id")]

        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                String query = @"UPDATE [dbo].[Tbl_blog]
                                 SET [BlogTitle] = @Title
                                    ,[BlogAuthor] = @Author
                                 ,[BlogContent] = @Content
                                 WHERE [BlogId]=@Id;
                                ";

                
                int result = db.Execute(query, new BlogViewModel
                {
                    Id = id,
                    Title = blog.Title,
                    Author = blog.Author,
                    Content = blog.Content
                });

                
                return Ok(result == 1 ? "Updating Successfully !" : "Updating fail !");
            }
        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {

            string condition = "";
            var paramenters = new DynamicParameters();
            if (!string.IsNullOrEmpty(blog.Title))
            {
                condition += " [BlogTitle] = @Title, ";
                paramenters.Add("Title", blog.Title);
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                condition += " [BlogAuthor] = @Author, ";
                paramenters.Add("Author", blog.Author);
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                condition += " [BlogContent] = @Content, ";
                paramenters.Add("Content", blog.Content);
            }


            if (condition.Length == 0)
            {
                return BadRequest("Invalid Parameters !");
            }

            condition = condition.Substring(0, condition.Length - 2);

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                String query = $@"UPDATE [dbo].[Tbl_blog]
                           SET {condition}
                             WHERE [BlogId]=@Id;
                                ";


                paramenters.Add("Id", id);
                int result = db.Execute(query,paramenters);



                return Ok(result > 0 ? "Updating successfully !" : "Updating fail !");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {


            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                String query = @"DELETE FROM [dbo].[Tbl_blog]
                         WHERE [BlogId]=@Id";

                db.Open();
                int result = db.Execute(query, new {Id = id});

                db.Close();
                return Ok(result == 1 ? "Delete Successfully !" : "Delete fail !");
            }

        }

    }

}
