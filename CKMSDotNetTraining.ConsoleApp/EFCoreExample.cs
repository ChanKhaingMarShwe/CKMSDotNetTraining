using System;
using System.Collections.Generic;
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

            var lst = db.Blogs.Where(x=>x.DeleteFlag==false).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine("Blog ID: " + item.BlogId);
               Console.WriteLine("Blog Title :"+item.BlogTitle);
                Console.WriteLine("Blog Author: " + item.BlogAuthor);
                 Console.WriteLine("Blog Content: " + item.BlogContent);
                 Console.WriteLine("-----------------------------");
            }


        }
    }
}
