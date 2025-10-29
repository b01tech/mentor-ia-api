using MentorIA.API.Services;
using OpenAI;

namespace MentorIA.API.Extensions;

public static class OpenAIExtensions
{
    public static IServiceCollection AddOpenAI(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        AddOpenAIClient(services, configuration);
        AddOpenAIService(services);
        return services;
    }

    private static void AddOpenAIClient(IServiceCollection services, IConfiguration configuration)
    {
        var apiKey = configuration["OpenAI:ApiKey"];
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("OpenAI: API Key is missing");
        }

        var openIAClient = new OpenAIClient(apiKey);
        services.AddSingleton(openIAClient);
    }

    private static void AddOpenAIService(IServiceCollection services)
    {
        services.AddScoped<ChatService>();
        services.AddScoped<RecipeService>();
        services.AddScoped<ImageService>();
    }
}
