using CKMSDotNetTraining.ConsoleApp.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKMSDotNetTraining.ConsoleApp
{
    public class DapperExample

    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=CKMSDotNetTraining;User ID=sa;Password=YourPassword123!";
        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "  Select * From Tbl_blog where DeleteFlag=0;";
                db.Open();
                List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
                foreach (BlogDataModel item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                }
            }
        }

        public void Create(String blogTitle, String blogAuthor, String blogContent)
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO [dbo].[Tbl_blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";

                db.Open();
                int result=db.Execute(query, new BlogDataModel
                {
                    BlogTitle=blogTitle,
                    BlogAuthor=blogAuthor,
                    BlogContent=blogContent
                });
                Console.WriteLine(result == 1 ? "Saving Successfully !":"Saving fail !");
                db.Close();

            }
        }

        public void Edit(int id)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "Select * From Tbl_blog where BlogId=@BlogId and DeleteFlag=0;";
                db.Open();
                BlogDataModel item = db.Query<BlogDataModel>(query, new { BlogId = id }).FirstOrDefault();
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Update(int blogId,String blogTitle,string blogAuthor,string blogContent)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                String query = @"UPDATE [dbo].[Tbl_blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId]=@BlogId;
";

                db.Open();
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId= blogId,
                    BlogTitle = blogTitle,
                    BlogAuthor = blogAuthor,
                    BlogContent = blogContent
                });
                Console.WriteLine(result == 1 ? "Updating Successfully !" : "Updating fail !");
                db.Close();

            }
        }
       
    }
}
