using Newtonsoft.Json;
using System.Text.Json.Serialization;

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

app.MapGet("/birds", () =>
{
    string folderPath = "Data/Birds.json";
   var json= File.ReadAllText(folderPath);
  var result=  JsonConvert.DeserializeObject<BirdResponseModel>(json)!;
    return Results.Ok(result);
})
    .WithName("GetBirds")
    .WithOpenApi();


app.MapGet("/birds/{id}", (int id) =>
{
    string folderPath = "Data/Birds.json";
    var json = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(json)!;
    var item = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if(item is null) { return Results.BadRequest("Data not found !"); }
    return Results.Ok(item);
})
    .WithName("GeetBird")
    .WithOpenApi();


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class BirdResponseModel
{
    public List<BirdModel> Tbl_Bird { get; set; } 
    
}


public class BirdModel
{
    public int Id { get; set; }
    public string? BirdMyanmarname { get; set; }

    public string? BirdEnglishname { get; set; }

    public string? BirdDescription { get; set; }

    public string? ImagePath { get; set; }

}
