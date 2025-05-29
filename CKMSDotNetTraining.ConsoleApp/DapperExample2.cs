using CKMSDotNetTraining.ConsoleApp.Models;
using CKMSDotNetTraining.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKMSDotNetTraining.ConsoleApp
{
    public class DapperExample2
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=CKMSDotNetTraining;User ID=sa;Password=YourPassword123!;TrustServerCertificate=true;";

        private readonly DapperService _dapperService;

        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);
        }
        public void Read()
        {

            string query = "SELECT * FROM Tbl_blog WHERE DeleteFlag = 0;";

            _dapperService.Query<BlogDapperDataModel>(query).ForEach(item =>
             {
                 Console.WriteLine("BlogId : " + item.BlogId);
                 Console.WriteLine("BlogTitle: " + item.BlogTitle);
                 Console.WriteLine("BlogAuthor: " + item.BlogAuthor);
                 Console.WriteLine("BlogContent: " + item.BlogContent);
                 Console.WriteLine("DeleteFlag : " + item.DeleteFlag);
             });

        }

        public void Create(string blogTitle, string blogAuthor, string blogContent)
        {
            string query = @"INSERT INTO [dbo].[Tbl_blog]
            ([BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag])
            VALUES
            (@BlogTitle, @BlogAuthor, @BlogContent, 0)";
            int result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogTitle = blogTitle,
                BlogAuthor = blogAuthor,
                BlogContent = blogContent
            });
            Console.WriteLine(result == 1 ? "Saving Successfully!" : "Saving failed!");
        }

       public void  Edit(int id)
        {
            string query = @"SELECT * FROM [dbo].[Tbl_blog] WHERE [BlogId] = @BlogId AND DeleteFlag = 0;";
            BlogDapperDataModel? item = _dapperService.QueryFirstOrDefault<BlogDapperDataModel>(query, new { BlogId = id });
            if (item != null)
            {
                Console.WriteLine("BlogId: " + item.BlogId);
                Console.WriteLine("BlogTitle: " + item.BlogTitle);
                Console.WriteLine("BlogAuthor: " + item.BlogAuthor);
                Console.WriteLine("BlogContent: " + item.BlogContent);
            }
            else
            {
                Console.WriteLine("No record found with the given BlogId.");
            }
        }

        public void Update(int id, string blogTitle, string blogAuthor, string blogContent)
        {
            string query = @"UPDATE [dbo].[Tbl_blog]
                             SET [BlogTitle] = @BlogTitle,
                                 [BlogAuthor] = @BlogAuthor,
                                 [BlogContent] = @BlogContent
                             WHERE [BlogId] = @BlogId AND DeleteFlag = 0;";
            int result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogId = id,
                BlogTitle = blogTitle,
                BlogAuthor = blogAuthor,
                BlogContent = blogContent
            });
            Console.WriteLine(result == 1 ? "Update Successfully!" : "Update failed!");
        }


        public void Delete(int id)
        {
            String query = @"DELETE FROM [dbo].[Tbl_blog] WHERE BlogId= @BlogId";
            int result = _dapperService.Execute(query, new { BlogId = id });
            Console.WriteLine(result == 1 ? "Delete Successfully!" : "Delete failed!");
        }
    }
}
