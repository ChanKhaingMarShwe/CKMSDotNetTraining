using CKMSDotNetTraining.Database.Models;
using CKMSDotNetTraining.Domain.features.Blog;
using Microsoft.EntityFrameworkCore;

namespace CKMSDotNetTraining.MinimalApi.Endpoints.Blog;

public static class BlogServiceEndpoint
{
    public static void UseBlogServiceEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blog", () =>
        {
            BlogService service = new BlogService();
            var lst = service.GetAllBlogs();
        return Results.Ok(lst);
        })
        .WithName("Getserviceblogs")
           .WithOpenApi();


        app.MapGet("/blog/{id}", (int id) =>
        {
           BlogService service = new BlogService();
           var model= service.GetBlog(id);
            return Results.Ok(model);
        })
            .WithName("GetServiceBlog")
            .WithOpenApi();

        app.MapPost("/blog", (TblBlog blog) =>
        {
            BlogService service = new BlogService();
            var model = service.CreateBlog(blog);
            return Results.Ok(model);
        })
            .WithName("CreteServiceBlog")
            .WithOpenApi();

        app.MapPut("/blog/{id}", (int id, TblBlog blog) =>
        {
           BlogService blogService = new BlogService();
            TblBlog model = blogService.UpdateBlog(id, blog);
            return Results.Ok(model);
        })
            .WithName("UpdateServiceBlog")
            .WithOpenApi();



        app.MapPatch("/blog/{id}", (int id, TblBlog blog) =>
        {
            BlogService blogService = new BlogService();
            TblBlog model = blogService.PatchBlog(id, blog);
            return Results.Ok(model);
        })
            .WithName("PatchServiceBlog")
            .WithOpenApi();


        app.MapDelete("/blog/{id}", (int id) =>
        {
           BlogService blogService = new BlogService();
            var model = blogService.GetBlog(id);
            return Results.Ok();
        })
            .WithName("DeleteServiceBlog")
            .WithOpenApi();


    }
}
