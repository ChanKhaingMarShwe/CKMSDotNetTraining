using CKMSDotNetTraining.RestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CKMSDotNetTraining.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {

        private readonly string _connectionString;

        public BlogsAdoDotNetController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection")!;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            String query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_blog] Where [DeleteFlag] = 0
";

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("BlogId: " + reader["BlogId"]);
                Console.WriteLine("BlogTitle: " + reader["BlogTitle"]);
                Console.WriteLine("BlogAuthor: " + reader["BlogAuthor"]);
                Console.WriteLine("BlogContent: " + reader["BlogContent"]);

                var blog = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag=Convert.ToBoolean(reader["DeleteFlag"])
                };
                lst.Add(blog);
            }
            
            connection.Close();
            return Ok(lst);
        }





        [HttpGet("id")]
        public IActionResult GetBlog(int id)
        {
            BlogViewModel item = new BlogViewModel();
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            String query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_blog] Where [DeleteFlag] = 0 And [BlogId]=@BlogId
";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("BlogId: " + reader["BlogId"]);
                Console.WriteLine("BlogTitle: " + reader["BlogTitle"]);
                Console.WriteLine("BlogAuthor: " + reader["BlogAuthor"]);
                Console.WriteLine("BlogContent: " + reader["BlogContent"]);

                var blog = new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                };
                item = blog;

            }

            connection.Close();
            return Ok(item);
        }




        [HttpPost]
        public IActionResult CreateBlog(BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            String query = @"INSERT INTO [dbo].[Tbl_blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)
";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);

            int result = cmd.ExecuteNonQuery();
     
            connection.Close();
            return Ok(result == 1 ? "Save successfully !" : "Save fail !");
        }


        [HttpPut("id")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            String query = @"UPDATE [dbo].[Tbl_blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId]=@BlogId;
";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);

            int result = cmd.ExecuteNonQuery();
           

            connection.Close();
            return Ok(result == 1 ? "Update successfully !" : "Update fail !");
        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {

            string condition = "";
            if(!string.IsNullOrEmpty(blog.Title))
            {
                condition += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                condition += " [BlogAuthor] = @BlogAuthor, ";
            }       
            if (!string.IsNullOrEmpty(blog.Content))
            {
                condition += " [BlogContent] = @BlogContent, ";
            }


             if (condition.Length == 0)
            {
                return BadRequest("Invalid Parameters !");
            } 

              condition = condition.Substring(0,condition.Length-2);


            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            String query = $@"UPDATE [dbo].[Tbl_blog]
                           SET {condition}
                             WHERE [BlogId]=@BlogId;
                                ";



            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            if (!string.IsNullOrEmpty(blog.Title))
            {
               cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
               cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);  
            }


            int result = cmd.ExecuteNonQuery();
           

            connection.Close();
            return Ok(result>0?"Updating successfully !":"Updating fail !");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            String query = @"DELETE FROM [dbo].[Tbl_blog]
      WHERE [BlogId]=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            
            connection.Close();
            return Ok(result == 1 ? "Delete successfully !" : "Delete fail !");
        }
    }
}
