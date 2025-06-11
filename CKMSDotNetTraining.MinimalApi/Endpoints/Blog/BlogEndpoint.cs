
using CKMSDotNetTraining.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CKMSDotNetTraining.MinimalApi.Endpoints.Blog;

public static class BlogEndpoint
{
    public static void UseBlogEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", ([FromServices] AppDbContext db) =>
        {
            var models = db.TblBlogs.AsNoTracking().ToList();
            return Results.Ok(models);
        })
            .WithName("Getblogs")
            .WithOpenApi();


        app.MapGet("/blogs/{id}", ([FromServices] AppDbContext  db,int id) =>
        {
            var model = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (model is null)
            {
                return Results.BadRequest("Not found");
            }
            return Results.Ok(model);
        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapPost("/blogs", ([FromServices] AppDbContext db, TblBlog model) => // Explicitly specify the namespace
        {
            db.TblBlogs.Add(model);
            db.SaveChanges();
            return Results.Ok(model);
        })
            .WithName("CreteBlog")
            .WithOpenApi();

        app.MapPut("/blogs/{id}", ([FromServices] AppDbContext db, int id, TblBlog blog) =>
        {
            var model = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (model is null)
            {
                return Results.BadRequest("Not found");
            }
            model.BlogTitle = blog.BlogTitle;
            model.BlogAuthor = blog.BlogAuthor;
            model.BlogContent = blog.BlogContent;

            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return Results.Ok(model);
        })
            .WithName("UpdateBlog")
            .WithOpenApi();

        app.MapDelete("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
        {
            var model = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (model is null)
            {
                return Results.BadRequest("Not found");
            }
            db.Entry(model).State = EntityState.Deleted;
            db.SaveChanges();
            return Results.Ok();
        })
            .WithName("DeleteBlog")
            .WithOpenApi();


    }
}
