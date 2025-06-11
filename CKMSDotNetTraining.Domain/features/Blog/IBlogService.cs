using CKMSDotNetTraining.Database.Models;

namespace CKMSDotNetTraining.Domain.features.Blog
{
    public interface IBlogService
    {
        TblBlog CreateBlog(TblBlog blog);
        bool? DeleteBlog(int id);
        List<TblBlog> GetAllBlogs();
        TblBlog GetBlog(int id);
        TblBlog PatchBlog(int id, TblBlog blog);
        TblBlog UpdateBlog(int id, TblBlog blog);
    }
}