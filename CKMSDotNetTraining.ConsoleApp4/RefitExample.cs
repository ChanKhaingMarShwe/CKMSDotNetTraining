using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKMSDotNetTraining.ConsoleApp4
{
    internal class RefitExample
    {
        public async Task Run()
        {
            var blogApi = RestService.For<IBlogApi>("https://localhost:7271");
            var blogs = await blogApi.GetBlogs();
            foreach (var blog in blogs)
            {
                Console.WriteLine(blog.BlogId);
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogAuthor);
                Console.WriteLine(blog.BlogContent);
            }





            //Read
            var blog1 = await blogApi.GetBlog(2);
            try
            {
                var blog2 = await blogApi.GetBlog(100);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No Data found !");
                }
            }

            //Create
            var blog3 = await blogApi.CreateBlog(new BlogModel
            {
                BlogTitle = "Refit Title",
                BlogAuthor = "Refit Author",
                BlogContent = "Refit Content"
            });

            if (blog3 != null)
            {
                Console.WriteLine("Create Blog Successfully! " + blog3.BlogId);
            }
            else
            {
                Console.WriteLine("CreateBlog fail.");
            }



            //Update
            var blog4 = await blogApi.UpdateBlog(2013, new BlogModel { 
            
                BlogTitle="Update Refit Title",
                BlogAuthor= "Update Refit Author",
                BlogContent="Update Refit Content"

            });

            Console.WriteLine("Update Blog Successfully !" + blog4.BlogId);
            try
                {
                var blog5 = await blogApi.UpdateBlog(100, new BlogModel

                {

                    BlogTitle = "Update Refit Title",
                    BlogAuthor = "Update Refit Author",
                    BlogContent = "Update Refit Content"
                });

                Console.WriteLine("Update Blog Successfully !" + blog5.BlogId);
            }
            catch (ApiException ex)
                {
                    if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Console.WriteLine("No Data found !");
                    }
                }
            

           


            //Delete

            var blog6 = await blogApi.DeleteBlog(2015);
            Console.WriteLine("Delete Blog Successfully !");
            try
            {
                var blog7 = await blogApi.DeleteBlog(100);
                Console.WriteLine("Delete Blog Successfully !");
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No Data found !");
                }
            }
            
        }
    }
}
