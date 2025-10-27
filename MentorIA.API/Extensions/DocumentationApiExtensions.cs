using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace MentorIA.API.Extensions;

public static class DocumentationApiExtensions
{
    public static IServiceCollection AddDocumentationApi(this IServiceCollection services)
    {
        services.AddOpenApi("v1", opt =>
        {
            opt.AddDocumentTransformer((doc, context, cancellationToken) =>
            {
                doc.Info = new OpenApiInfo
                {
                    Title = "Mentor AI API",
                    Version = "v1",
                    Description = "API de comunicação com Open AI",
                };
                return Task.CompletedTask;
            });
        });
        return services;
    }

    public static void UseDocumentationApi(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference(opt =>
        {
            opt.Title = "Mentor AI API";
            opt.Theme = ScalarTheme.Solarized;
            opt.DarkMode = true;
        });
    }
}
