using DomeChallange.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices(builder.Configuration);
var app = builder.Build();
app.ConfigureMiddlewares();
