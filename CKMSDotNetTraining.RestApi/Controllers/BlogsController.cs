﻿using CKMSDotNetTraining.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace CKMSDotNetTraining.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {

        private readonly AppDbContext _db;


        public BlogsController(AppDbContext db)
        {
            _db = db;
        }



        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst=_db.TblBlogs
                   .AsNoTracking()
                   .Where(x=>x.DeleteFlag==false)
                   .ToList();
            return Ok(lst);
        }

        [HttpGet("id")]
        public IActionResult GetBlog(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x=>x.BlogId==id);


            if(item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return Ok(blog);
        }


        [HttpPut("id")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id,TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);

            if(item is null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }

        //[HttpDelete("{id}")]
        //public IActionResult DeleteBlog(int id)
        //{

        //    var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        //    if (item is null)
        //    {
        //        return NotFound();
        //    }

        //    item.DeleteFlag = true;

        //    _db.Entry(item).State = EntityState.Modified;
        //    _db.SaveChanges();
        //    return Ok(item);
        //}



        [HttpDelete("id")]
        public IActionResult DeleteBlog(int id)
        {
            var item = _db.TblBlogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }

            _db.TblBlogs.Remove(item);  // <-- This deletes the record from the DB
            _db.SaveChanges();

            return Ok(item);
        }


    }
}
