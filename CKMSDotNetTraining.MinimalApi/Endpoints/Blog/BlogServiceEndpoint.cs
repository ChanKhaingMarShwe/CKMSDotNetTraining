using CKMSDotNetTraining.Database.Models;
using CKMSDotNetTraining.Domain.features.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CKMSDotNetTraining.MinimalApi.Endpoints.Blog;

public static class BlogServiceEndpoint
{
    public static void UseBlogServiceEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blog", ([FromServices] IBlogService service) =>
        {
            var lst = service.GetAllBlogs();
        return Results.Ok(lst);
        })
        .WithName("Getserviceblogs")
           .WithOpenApi();


        app.MapGet("/blog/{id}", ([FromServices] BlogService service, int id) =>
        {
           var model= service.GetBlog(id);
            return Results.Ok(model);
        })
            .WithName("GetServiceBlog")
            .WithOpenApi();

        app.MapPost("/blog", ([FromServices] BlogService service,TblBlog blog) =>
        {
            var model = service.CreateBlog(blog);
            return Results.Ok(model);
        })
            .WithName("CreteServiceBlog")
            .WithOpenApi();

        app.MapPut("/blog/{id}", ([FromServices] BlogService service,int id, TblBlog blog) =>
        {
            TblBlog model = service.UpdateBlog(id, blog);
            return Results.Ok(model);
        })
            .WithName("UpdateServiceBlog")
            .WithOpenApi();



        app.MapPatch("/blog/{id}", ([FromServices] BlogService service,int id, TblBlog blog) =>
        {
            TblBlog model = service.PatchBlog(id, blog);
            return Results.Ok(model);
        })
            .WithName("PatchServiceBlog")
            .WithOpenApi();


        app.MapDelete("/blog/{id}", ([FromServices] BlogService service,int id) =>
        {
            var model = service.GetBlog(id);
            return Results.Ok();
        })
            .WithName("DeleteServiceBlog")
            .WithOpenApi();


    }
}
