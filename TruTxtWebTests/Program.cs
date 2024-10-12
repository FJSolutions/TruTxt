using TruTxtWebTests.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTruConfiguration(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();