using ApplicationFramework.Presentation.Web;
using Microsoft.AspNetCore.Mvc;
using ServiceSample.Api.Configuration;
using ServiceSample.Api.Configuration.Extensions;

[assembly: ApiConventionType(typeof(ApiConventions))]

var builder = WebApplication.CreateBuilder(args);

// Configure Logging
builder.Logging.SetupSerilog(builder.Configuration);

// Add services to the container.
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
