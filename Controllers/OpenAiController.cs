using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Org.BouncyCastle.Utilities.Collections;

namespace AzureSemanticKernel.AI.Controllers
{
    public class OpenAiController : Controller
    {
        [HttpPost]
        public async Task<string> GetGpt4oResponse(string imgUrl)
        {
            try
            {
                var kernel = Common.initializeOpenAIKernel();

                var chat = kernel.GetRequiredService<IChatCompletionService>();

                var history = new ChatHistory();
                history.AddSystemMessage("You are a friendly and helpful assistant that responds to questions directly");

                var message = new ChatMessageContentItemCollection
{
                    new TextContent("Can you do a detail analysis and tell me all the minute details that present in this image?"),
                    new ImageContent(new Uri(imgUrl))
                };

                history.AddUserMessage(message);

                var result = await chat.GetChatMessageContentAsync(history);

                return result.Content;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
