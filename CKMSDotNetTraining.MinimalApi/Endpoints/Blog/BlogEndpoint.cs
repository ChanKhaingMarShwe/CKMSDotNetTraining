using CKMSDotNetTraining.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CKMSDotNetTraining.MinimalApi.Endpoints.Blog;

public static class BlogEndpoint
{
    public static void UseBlogEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", () =>
        {
            AppDbContext db = new AppDbContext();
            var models = db.TblBlogs.AsNoTracking().ToList();
            return Results.Ok(models);
        })
.WithName("Getblogs")
.WithOpenApi();


        app.MapGet("/blogs/{id}", (int id) =>
        {
            AppDbContext db = new AppDbContext();
            var model = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (model is null)
            {
                return Results.BadRequest("Not found");
            }
            return Results.Ok(model);
        })
            .WithName("GetBlog")
            .WithOpenApi();

        app.MapPost("/blogs", (TblBlog model) =>
        {
            AppDbContext db = new AppDbContext();
            db.TblBlogs.Add(model);
            db.SaveChanges();
            return Results.Ok(model);
        })
            .WithName("CreteBlog")
            .WithOpenApi();

        app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
        {
            AppDbContext db = new AppDbContext();
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

        app.MapDelete("/blogs/{id}", (int id) =>
        {
            AppDbContext db = new AppDbContext();
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
