using OpenAI;
using OpenAI.Chat;

namespace MentorIA.API.Services;

public class RecipeService
{
    private readonly OpenAIClient _openAIClient;
    private readonly string _model;

    public RecipeService(OpenAIClient openAiClient, IConfiguration configuration)
    {
        _openAIClient = openAiClient;
        _model = configuration["OpenAI:ChatModel"] ?? "gpt-4o";
    }

    public async Task<string> GetRecipeAsync(
        string ingredients,
        string cuisine,
        string restrictions
    )
    {
        var chatClient = _openAIClient.GetChatClient(_model);
        var systemMessage = new SystemChatMessage(
            "You are a professional chef that provides creative and easy to follow recipes"
        );
        var userMessage = new UserChatMessage(
            $"""
                I want to create a recipe using the following ingredients: {ingredients},
                The cusine type that I prefer is: {cuisine},
                Please consider the following dietary restrictions: {restrictions}.
                Please provide me with a detailed recipe include title, list od ingredients and
                cooking instructions.
            """
        );
        var messages = new List<ChatMessage> { systemMessage, userMessage };
        var options = new ChatCompletionOptions { MaxOutputTokenCount = 500, Temperature = 0.4f, };
        var response = await chatClient.CompleteChatAsync(messages, options);
        return response.Value.Content[^1].Text ?? "No response generated.";
    }
}
