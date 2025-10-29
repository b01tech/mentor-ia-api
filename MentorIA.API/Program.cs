using MentorIA.API.Extensions;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCorsConfiguration().AddDocumentationApi().AddOpenAI(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll");
app.UseDocumentationApi();
app.MapChatEndpoint();
app.UseHttpsRedirection();

app.Run();
