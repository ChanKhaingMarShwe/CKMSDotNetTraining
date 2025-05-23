using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKMSDotNetTraining.ConsoleApp
{
    public class ADODotNetExample
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=CKMSDotNetTraining;User ID=sa;Password=YourPassword123!";
        

        public void Read()
        {
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

                Console.WriteLine();
            }
            //DataTable dt = new DataTable();
            //adapter.Fill(dt); 

            //foreach( DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine("BlogTilte"+dr["BlogTitle"]);
            //    Console.WriteLine("BlogAuthor"+dr["BlogAuthor"]);
            //    Console.WriteLine("BlogContent"+dr["BlogContent"]);

            //} 
            connection.Close();


            Console.WriteLine("Connection Closed.... ");

        }

        public void Create()
        {
            Console.Write("Enter Title :");
            String blogTitle = Console.ReadLine();
            Console.Write("Enter Author :");
            String blogAuthor = Console.ReadLine();
            Console.Write("Enter Content :");
            String blogContent = Console.ReadLine();


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
            SqlCommand cmd = new SqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blogContent);

            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result == 1 ? "Save successfully !" : "Save fail !");


            connection.Close();

        }
      
        public void Edit()
        {
            Console.Write("Enter BlogId :");
            String blogId = Console.ReadLine();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();


            String query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_blog] Where [BlogId]=@BlogId;

";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("BlogId: " + reader["BlogId"]);
                Console.WriteLine("BlogTitle: " + reader["BlogTitle"]);
                Console.WriteLine("BlogAuthor: " + reader["BlogAuthor"]);
                Console.WriteLine("BlogContent: " + reader["BlogContent"]);
                Console.WriteLine();
            }


            connection.Close();
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

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            String query = @"UPDATE [dbo].[Tbl_blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId]=@BlogId;
";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            cmd.Parameters.AddWithValue("@BlogTitle", blogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blogContent);

            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result == 1 ? "Update successfully !" : "Update fail !");

            connection.Close();
        }

        public void Delete()
        {
            Console.Write("Enter BlogId :");
            String blogId = Console.ReadLine();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            String query = @"DELETE FROM [dbo].[Tbl_blog]
      WHERE [BlogId]=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result == 1 ? "Delete successfully !" : "Delete fail !");
            connection.Close();
        }
        }
}
