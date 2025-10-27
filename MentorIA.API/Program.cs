using MentorIA.API.Extensions;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDocumentationApi();

var app = builder.Build();

app.UseDocumentationApi();
app.UseHttpsRedirection();

app.Run();
