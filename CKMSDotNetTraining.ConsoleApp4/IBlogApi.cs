using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKMSDotNetTraining.ConsoleApp4
{
    public interface IBlogApi
    {
        [Get("/api/blogs")]
        Task<List<BlogModel>> GetBlogs();

        [Get("/api/blogs/id")]
        Task<BlogModel> GetBlog([Query] int id);


        [Post("/api/blogs")]
        Task <BlogModel> CreateBlog(BlogModel blog);

        [Put("/api/blogs/id")]
        Task<BlogModel> UpdateBlog([Query]int id, BlogModel blog);

        [Delete("/api/blogs/id")]
        Task<BlogModel> DeleteBlog([Query]int id);



    }

    public class BlogModel
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public string BlogAuthor { get; set; } = null!;

        public string BlogContent { get; set; } = null!;

        public bool DeleteFlag { get; set; }
    }
}
