using System.Diagnostics;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var assemblyLocation = typeof(project.webapi.WeatherForecast).Assembly.Location;
var productVersion = FileVersionInfo.GetVersionInfo(assemblyLocation).ProductVersion;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=> {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project WebAPI", Version = $"v{productVersion}" });
    c.AddServer(new OpenApiServer { Description= "Dev", Url = "https://billing.dev.greatcall.com" });
    c.AddServer(new OpenApiServer { Description= "Test", Url = "https://billing.test.greatcall.com" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
