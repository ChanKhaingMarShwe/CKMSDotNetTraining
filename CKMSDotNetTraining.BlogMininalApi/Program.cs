using CKMSDotNetTraining.Database.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();



app.MapGet("/blogs", () =>
{
    string folderPath = "Data/blogs.json";
    var json = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BlogResponseModel>(json)!;
    return Results.Ok(result);
})
    .WithName("GetBlogs")
    .WithOpenApi();


app.MapGet("/blogs/{id}", (int id) =>
{
    string folderPath = "Data/blogs.json";
    var json = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BlogResponseModel>(json)!;
    var item = result.Tbl_Blog.FirstOrDefault(x => x.BlogId == id);
    if (item is null) { return Results.BadRequest("Data not found !"); }
    return Results.Ok(item);
})
    .WithName("GetBlog")
    .WithOpenApi();



app.MapPost("/blogs", (TblBlog requestBlog) =>
{
    string folderPath = "Data/blogs.json";
    var json = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BlogResponseModel>(json)!;

    requestBlog.BlogId = result.Tbl_Blog.Count==0?1:  result.Tbl_Blog.Max(x => x.BlogId) + 1;
    result.Tbl_Blog.Add(requestBlog);
    string JsonStrWrt = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, JsonStrWrt);

    return Results.Ok(requestBlog);

})
    .WithName("CreateBlog")
   .WithOpenApi();



app.MapPut("/blogs", (int id,TblBlog requestBlog) =>
{
    string folderPath = "Data/blogs.json";
    var json = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BlogResponseModel>(json)!;
    var item = result.Tbl_Blog.FirstOrDefault(x => x.BlogId == id);

    if (item is null) { return Results.BadRequest("Data not found !"); }

    item.BlogTitle = requestBlog.BlogTitle;
    item.BlogAuthor = requestBlog.BlogAuthor;
    item.BlogContent = requestBlog.BlogContent;

    string JsonStrWrt = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, JsonStrWrt);

    return Results.Ok(requestBlog);

})
    .WithName("UpdteBlog")
   .WithOpenApi();



app.MapDelete("/blogs", (int id) =>
{
    string folderPath = "Data/blogs.json";
    var json = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BlogResponseModel>(json)!;
    var item = result.Tbl_Blog.FirstOrDefault(x => x.BlogId == id);

    if (item is null) { return Results.BadRequest("Data not found !"); }

    result.Tbl_Blog.Remove(item);
    
    string JsonStrWrt = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, JsonStrWrt);
    
    return Results.Ok(result);

})
    .WithName("DeleteBlog")
   .WithOpenApi();



app.Run();



public class BlogResponseModel
{
    public List<TblBlog> Tbl_Blog { get; set; }

}


internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
