using MentorIA.API.Services;

namespace MentorIA.API.Extensions
{
    public static class EndpointExtensions
    {
        public static void MapChatEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("/chat").WithTags("Chat");
            group
                .MapPost(
                    "/",
                    async (string prompt, ChatService chatService) =>
                    {
                        var response = await chatService.GetResponseAsync(prompt);
                        return Results.Ok(new { Response = response });
                    }
                )
                .WithDescription("AskChatAI");
        }
    }
}
