// See https://aka.ms/new-console-template for more information
using CKMSDotNetTraining.Database.Models;
using Newtonsoft.Json;
using System.Reflection.Metadata;

Console.WriteLine("Hello, World!");

var program = new CKMSDotNetTraining.ConsoleApp.Program();
program.FetchBlogs();
namespace CKMSDotNetTraining.ConsoleApp
{
    public class Program
    {
        private AppDbContext db;

        public Program()
        {
            db = new AppDbContext();
        }

        public void FetchBlogs()
        {
            var lst = db.TblBlogs.Where(x => x.DeleteFlag == false).ToList();
            string jsonStr = JsonConvert.SerializeObject(lst, Formatting.Indented);

            string filePath = "Data/blogs.json";

            File.WriteAllText(filePath, jsonStr);


            Console.WriteLine($"JSON saved to {filePath}");
        }



    }


}