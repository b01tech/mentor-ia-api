using OpenAI;

namespace MentorIA.API.Extensions;

public static class OpenAIExtensions
{
    public static IServiceCollection AddOpenAI(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var apiKey = configuration["OpenAI:ApiKey"];
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("OpenAI: API Key is missing");
        }

        var openIAClient = new OpenAIClient(apiKey);
        services.AddSingleton(openIAClient);
        return services;
    }
}
