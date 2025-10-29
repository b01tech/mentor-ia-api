using OpenAI;
using OpenAI.Chat;

namespace MentorIA.API.Services
{
    public class ChatService
    {
        private readonly OpenAIClient _openAIClient;
        private readonly string _model;

        public ChatService(OpenAIClient openIAClient, IConfiguration configuration)
        {
            _openAIClient = openIAClient;
            _model = configuration["OpenAI:ChatModel"] ?? "gpt-4o";
        }

        public async Task<string> GetResponseAsync(string prompt)
        {
            var chatClient = _openAIClient.GetChatClient(_model);
            var messages = new List<ChatMessage> { new UserChatMessage(prompt) };
            var options = new ChatCompletionOptions
            {
                Temperature = 0.4f,
                MaxOutputTokenCount = 200
            };
            var response = await chatClient.CompleteChatAsync(messages, options);
            return response.Value.Content[^1].Text ?? "No response from OpenAI";
        }
    }
}
