using CKMSDotNetTraining.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKMSDotNetTraining.Domain.features.Blog
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _db;

        public BlogService(AppDbContext db)
        {
            _db = db;
        }

        public List<TblBlog> GetAllBlogs()
        {
            var list = _db.TblBlogs.AsNoTracking().ToList();
            return list;
        }

        public TblBlog GetBlog(int id)
        {
            var blog = _db.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
            return blog;
        }

        public TblBlog CreateBlog(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return blog;
        }

        public TblBlog UpdateBlog(int id, TblBlog blog)
        {
            var existingBlog = _db.TblBlogs.FirstOrDefault(b => b.BlogId == id);

            if (existingBlog != null)
            {
                existingBlog.BlogTitle = blog.BlogTitle;
                existingBlog.BlogAuthor = blog.BlogAuthor;
                existingBlog.BlogContent = blog.BlogContent;
                _db.SaveChanges();


            }

            return existingBlog;
        }


        public TblBlog PatchBlog(int id, TblBlog blog)
        {
            var model = _db.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);

            if (model != null)
            {

                if (!string.IsNullOrEmpty(blog.BlogTitle))
                {
                    model.BlogTitle = blog.BlogTitle;
                }
                if (!string.IsNullOrEmpty(blog.BlogAuthor))
                {
                    model.BlogAuthor = blog.BlogAuthor;
                }
                if (!string.IsNullOrEmpty(blog.BlogContent))
                {
                    model.BlogContent = blog.BlogContent;
                }
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                return model;
            }
            return null; // Ensure all code paths return a value
        }
        public bool? DeleteBlog(int id)
        {
            var blog = _db.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);

            if (blog is null)
            {
                return null; // Blog not found
            }

            _db.Entry(blog).State = EntityState.Deleted;
            int result = _db.SaveChanges();
            return result > 0;
        }


    }




    public class BlogV2Service : IBlogService
    {
        private readonly AppDbContext _db;

        public BlogV2Service(AppDbContext db)
        {
            _db = db;
        }

        public List<TblBlog> GetAllBlogs()
        {
            var list = _db.TblBlogs.AsNoTracking().ToList();
            return list;
        }

        public TblBlog GetBlog(int id)
        {
            var blog = _db.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
            return blog;
        }

        public TblBlog CreateBlog(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return blog;
        }

        public TblBlog UpdateBlog(int id, TblBlog blog)
        {
            var existingBlog = _db.TblBlogs.FirstOrDefault(b => b.BlogId == id);

            if (existingBlog != null)
            {
                existingBlog.BlogTitle = blog.BlogTitle;
                existingBlog.BlogAuthor = blog.BlogAuthor;
                existingBlog.BlogContent = blog.BlogContent;
                _db.SaveChanges();


            }

            return existingBlog;
        }


        public TblBlog PatchBlog(int id, TblBlog blog)
        {
            var model = _db.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);

            if (model != null)
            {

                if (!string.IsNullOrEmpty(blog.BlogTitle))
                {
                    model.BlogTitle = blog.BlogTitle;
                }
                if (!string.IsNullOrEmpty(blog.BlogAuthor))
                {
                    model.BlogAuthor = blog.BlogAuthor;
                }
                if (!string.IsNullOrEmpty(blog.BlogContent))
                {
                    model.BlogContent = blog.BlogContent;
                }
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                return model;
            }
            return null; // Ensure all code paths return a value
        }
        public bool? DeleteBlog(int id)
        {
            var blog = _db.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);

            if (blog is null)
            {
                return null; // Blog not found
            }

            _db.Entry(blog).State = EntityState.Deleted;
            int result = _db.SaveChanges();
            return result > 0;
        }


    }

}
