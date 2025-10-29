using OpenAI;
using OpenAI.Images;

namespace MentorIA.API.Services
{
    public class ImageService
    {
        private readonly OpenAIClient _openAIClient;
        private readonly string _model = "dall-e-3";

        public ImageService(OpenAIClient openAIClient, IConfiguration configuration)
        {
            _openAIClient = openAIClient;
            _model = configuration["OpenAI:ImageModel"] ?? _model;
        }

        public async Task<IEnumerable<string>> GetImageAsync(
            string prompt,
            string quality,
            int height = 1024,
            int width = 1024
        )
        {
            var imageClient = _openAIClient.GetImageClient(_model);
            var imageOptions = new OpenAI.Images.ImageGenerationOptions
            {
                Size = GetSize(height, width),
                Quality = quality.Equals("hd", StringComparison.OrdinalIgnoreCase)
                    ? GeneratedImageQuality.High
                    : GeneratedImageQuality.Standard,
            };
            var imageResponse = await imageClient.GenerateImageAsync(prompt, imageOptions);
            return new[] { imageResponse.Value.ImageUri.ToString() };
        }

        private GeneratedImageSize GetSize(int height, int width)
        {
            return (height, width) switch
            {
                (1024, 1024) => GeneratedImageSize.W1024xH1024,
                (512, 512) => GeneratedImageSize.W512xH512,
                (256, 256) => GeneratedImageSize.W256xH256,
                _ => GeneratedImageSize.W1024xH1024,
            };
        }
    }
}
