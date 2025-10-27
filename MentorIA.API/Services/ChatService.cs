using OpenAI;

namespace MentorIA.API.Services
{
    public class ChatService
    {
        private readonly OpenAIClient _openIAClient;
        private readonly string _model;

        public ChatService(OpenAIClient openIAClient, IConfiguration configuration)
        {
            _openIAClient = openIAClient;
            _model = configuration["OpenAI:ChatModel"] ?? "gpt-4o";
        }

        public async Task<string> GetResponseAsync(string prompt)
        {
            var chatClient = _openIAClient.GetChatClient(_model);
            var response = await chatClient.CompleteChatAsync(prompt);
            return response.Value.Content[^1].Text ?? "No response from OpenAI";
        }
    }
}
