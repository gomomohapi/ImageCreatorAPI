using Azure.AI.OpenAI;
using Azure;

namespace ImageCreatorAPI
{
    public class ImageCreator
    {
        //Initializing the Endpoint and Key
        static string endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
        static string key = Environment.GetEnvironmentVariable("AZURE_OPENAI_KEY");

        //Instantiate OpenAI Client
        static OpenAIClient openaiClient = new OpenAIClient(
            new Uri(endpoint),
            new AzureKeyCredential(key));

        //Create Image with DallE
        public static async Task<GeneratedImage> GenerateImage(string prompt)
        {
            var generationOptions = new ImageGenerationOptions()
            {
                Prompt = prompt + ", high-quality digital art",
                ImageCount = 1,
                Size = ImageSize.Size1024x1024,
                Style = ImageGenerationStyle.Vivid,
                Quality = ImageGenerationQuality.Hd,
                DeploymentName = "dalle3",
                User = "1",
            };

            Response<ImageGenerations> imageGenerations =
                await openaiClient.GetImageGenerationsAsync(generationOptions);

            var generatedImage = new GeneratedImage()
            {
                ImageUrl = imageGenerations.Value.Data[0].Url.ToString()
            };

            return generatedImage;
        }
    }

    public class GeneratedImage
    {
        public string ImageUrl { get; set; }
    }
}
