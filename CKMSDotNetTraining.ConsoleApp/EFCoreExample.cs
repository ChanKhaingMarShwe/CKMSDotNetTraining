using CKMSDotNetTraining.ConsoleApp.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKMSDotNetTraining.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();

            var lst = db.Blogs.Where(x => x.DeleteFlag == false).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine("Blog ID: " + item.BlogId);
                Console.WriteLine("Blog Title :" + item.BlogTitle);
                Console.WriteLine("Blog Author: " + item.BlogAuthor);
                Console.WriteLine("Blog Content: " + item.BlogContent);
                Console.WriteLine("-----------------------------");
            }


        }


        public void Create(String blogTitle, String blogAuthor, String blogContent)
        {

            BlogEFCoreDataModel blog = new BlogEFCoreDataModel
            {
                BlogTitle = blogTitle,
                BlogAuthor = blogAuthor,
                BlogContent = blogContent
            };

            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            int result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Saving Successfully !" : "Saving fail !");



        }

        public void Edit(int id)
        {

            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item == null)
            {
                Console.WriteLine("Data not found !");
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }



        public void Update(int id, String blogTitle, String blogAuthor, String blogContent)
        {

            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);

            if (item == null)
            {
                Console.WriteLine("Data not found !");
                return;
            }


            if (!string.IsNullOrEmpty(blogTitle))
            {
                item.BlogTitle = blogTitle;
            }
            if (!string.IsNullOrEmpty(blogAuthor))
            {
                item.BlogAuthor = blogAuthor;
            }
            if (!string.IsNullOrEmpty(blogContent))
            {
                item.BlogContent = blogContent;
            }

            db.Entry(item).State = EntityState.Modified;

            int result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Update Successfully !" : "Update fail !");



        }


        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("Data not found !");
                return;
            }

            db.Entry(item).State = EntityState.Deleted;
            int result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Delete Successfully !" : "Delete fail !");
        }
    }
}
