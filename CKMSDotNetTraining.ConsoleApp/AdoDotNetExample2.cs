using CKMSDotNetTraining.Shared;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKMSDotNetTraining.ConsoleApp
{
    public class AdoDotNetExample2
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=CKMSDotNetTraining;User ID=sa;Password=YourPassword123!";

        private readonly AdoDotNetService _adoDotNetService;

        public AdoDotNetExample2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        public void Read()
        {
            string query = @"SELECT [BlogId]
                                ,[BlogTitle]
                                ,[BlogAuthor]
                                ,[BlogContent]
                                ,[DeleteFlag]
                                  FROM [dbo].[Tbl_blog] Where [DeleteFlag] = 0";

            DataTable dt = _adoDotNetService.Query(query);

            // Fix: Cast each row to DataRow before accessing columns
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("BlogId : " + dr["BlogId"]);
                Console.WriteLine("BlogTitle: " + dr["BlogTitle"]);
                Console.WriteLine("BlogAuthor: " + dr["BlogAuthor"]);
                Console.WriteLine("BlogContent: " + dr["BlogContent"]);
                Console.WriteLine("DeleteFlag : " + dr["DeleteFlag"]);
            }
        }

        public void Create()
        {
            Console.Write("Enter Title :");
            String blogTitle = Console.ReadLine();
            Console.Write("Enter Author :");
            String blogAuthor = Console.ReadLine();
            Console.Write("Enter Content :");
            String blogContent = Console.ReadLine();

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

            int result = _adoDotNetService.Execute(query,

                            new SqlParameterModel("@BlogTitle", blogTitle),
                            new SqlParameterModel("@BlogAuthor", blogAuthor),
                             new SqlParameterModel("@BlogContent", blogContent));


            Console.WriteLine(result == 1 ? "Save successfully !" : "Save fail !");



        }

        public void Edit()
        {
            Console.Write("Enter BlogId :");
            String blogId = Console.ReadLine();

            String query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                          ,[DeleteFlag]
                          FROM [dbo].[Tbl_blog] Where [BlogId]=@BlogId;
                        ";

            DataTable dt = _adoDotNetService.Query(query, new SqlParameterModel("@BlogId",blogId));

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("Blog Id  doesn't exist !");
                return;
            }

            DataRow dr = dt.Rows[0];


            Console.WriteLine("BlogId : " + dr["BlogId"]);
            Console.WriteLine("BlogTitle: " + dr["BlogTitle"]);
            Console.WriteLine("BlogAuthor: " + dr["BlogAuthor"]);
            Console.WriteLine("BlogContent: " + dr["BlogContent"]);
        }

        public void Update()
        {
            Console.Write("Enter BlogId :");
            String blogId = Console.ReadLine();
            Console.Write("Enter Title :");
            String blogTitle = Console.ReadLine();
            Console.Write("Enter Author :");
            String blogAuthor = Console.ReadLine();
            Console.Write("Enter Content :");
            String blogContent = Console.ReadLine();


            //String query = @"UPDATE [dbo].[Tbl_blog]
            //             SET [BlogTitle] = @BlogTitle
            //            ,[BlogAuthor] = @BlogAuthor
            //           ,[BlogContent] = @BlogContent
            // WHERE [BlogId]=@BlogId;
            //    ";
            //int result = _adoDotNetService.Execute(query,

            //                new SqlParameterModel("@BlogId", blogId),
            //               new SqlParameterModel("@BlogTitle", blogTitle),
            //               new SqlParameterModel("@BlogTitle", blogTitle),
            //                new SqlParameterModel("@BlogContent", blogContent));     
            //Console.WriteLine(result == 1 ? "Update successfully !" : "Update fail !");


            var setClauses = new List<string>();
            var parameters = new List<SqlParameterModel>();

            if (!string.IsNullOrEmpty(blogTitle))
            {
                setClauses.Add("[BlogTitle] = @BlogTitle");
                parameters.Add(new SqlParameterModel("@BlogTitle", blogTitle));
            }
            if (!string.IsNullOrEmpty(blogAuthor))
            {
                setClauses.Add("[BlogAuthor] = @BlogAuthor");
                parameters.Add(new SqlParameterModel("@BlogAuthor", blogAuthor));
            }
            if (!string.IsNullOrEmpty(blogContent))
            {
                setClauses.Add("[BlogContent] = @BlogContent");
                parameters.Add(new SqlParameterModel("@BlogContent", blogContent));
            }


            if (setClauses.Count == 0)
            {
                Console.WriteLine("Nothing to update !");
                return;
            }
            ;

            parameters.Add(new SqlParameterModel("@BlogId", blogId));

            

            string setClause = string.Join(", ", setClauses);

            string query = $@"
        UPDATE [dbo].[Tbl_blog]
        SET {setClause}
        WHERE [BlogId] = @BlogId;
    ";
           
           
            int result = _adoDotNetService.Execute(query,parameters.ToArray());

           Console.WriteLine(result == 1 ? "Update successfully !" : "Update fail !");


        }

        public void Delete()
        {
            Console.Write("Enter BlogId :");
            String blogId = Console.ReadLine();
           
            String query = @"DELETE FROM [dbo].[Tbl_blog]
                            WHERE [BlogId]=@BlogId";


            int result = _adoDotNetService.Execute(query, new SqlParameterModel("@BlogId", blogId));
            Console.WriteLine(result == 1 ? "Delete successfully !" : "Delete fail !");
            
        }


    }
}
