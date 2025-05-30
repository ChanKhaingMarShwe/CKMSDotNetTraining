// See https://aka.ms/new-console-template for more information
using CKMSDotNetTraining.Database.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Hello, World!");

//AppDbContext db = new AppDbContext();
//var lst=db.TblBlogs.ToList();


var blog = new BlogModel
{
    Id = 1,
    Title = "Sample Blog",
    Content = "This is a sample blog content.",
    Author = "John Doe"
};

//string jsonStr = JsonConvert.SerializeObject(blog);
string jsonStr=blog.Jsonstr();

string jsoStr2 ="""{"Id":1,"Title":"Sample Blog","Content":"This is a sample blog content.","Author":"John Doe"}""";

var blog2=JsonConvert.DeserializeObject<BlogModel>(jsoStr2);

Console.WriteLine(blog2.Title);
Console.WriteLine(jsonStr); 
Console.ReadLine();
public class BlogModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? Author { get; set; }
}


public static class Exentations
{
    public static string Jsonstr(this Object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj);
        return jsonStr;
    }
}