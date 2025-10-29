using MentorIA.API.DTOs;
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
                    async (ChatRequestDTO request, ChatService chatService) =>
                    {
                        if (string.IsNullOrWhiteSpace(request?.Prompt))
                            return Results.BadRequest("Prompt must be provided");
                        var response = await chatService.GetResponseAsync(request.Prompt);
                        return Results.Ok(response);
                    }
                )
                .WithDescription("AskChatAI");
        }

        public static void MapRecipeEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("/recipe").WithTags("Recipe");
            group
                .MapPost(
                    "/",
                    async (RecipeRequestDTO request, RecipeService recipeService) =>
                    {
                        if (string.IsNullOrWhiteSpace(request?.Ingredients))
                            return Results.BadRequest("Ingredients must be provided");

                        var cuisine = request.Cuisine ?? "Any";
                        var restrictions = request.Restrictions ?? "None";

                        var response = await recipeService.GetRecipeAsync(
                            request.Ingredients,
                            cuisine,
                            restrictions
                        );
                        return Results.Ok(response);
                    }
                )
                .WithDescription("GetRecipe");
        }

        public static void MapImageEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("/image").WithTags("Image");
            group
                .MapPost(
                    "/",
                    async (ImageRequestDTO request, ImageService imageService) =>
                    {
                        if (string.IsNullOrWhiteSpace(request?.Prompt))
                            return Results.BadRequest("Prompt must be provided");

                        var response = await imageService.GetImageAsync(
                            request.Prompt,
                            request.Quality,
                            request.Height,
                            request.Width
                        );
                        return Results.Ok(response);
                    }
                )
                .WithDescription("GetImage");
        }
    }
}
