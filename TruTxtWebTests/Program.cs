using TruTxtWebTests.Configuration;

var builder = WebApplication.CreateBuilder(args);

//* Extension method for adding TruAspNetCore reader  
builder.AddTruConfiguration();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();