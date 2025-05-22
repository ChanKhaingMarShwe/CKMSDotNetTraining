// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
//Console.ReadLine();

// md = markdown

String connectionString = "Data Source=localhost;Initial Catalog=CKMSDotNetTraining;User ID=sa;Password=YourPassword123!";
Console.WriteLine("Connection String "+ connectionString);
SqlConnection connection = new SqlConnection(connectionString);
Console.WriteLine("Connection Opening.... ");
connection.Open();
Console.WriteLine("Connection Opened.... ");

String query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_blog] Where [DeleteFlag] = 0
";

SqlCommand cmd = new SqlCommand(query,connection);
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
Console.ReadKey();
