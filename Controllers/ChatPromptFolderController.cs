using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Plugins.Core;

namespace AzureSemanticKernel.AI.Controllers
{
    public class ChatPromptFolderController : Controller
    {
        ChatHistory history = [];

        [HttpPost]
        public async Task<string> GetChatKernelResponse(string userMessage, string userTone)
        {
            try
            {
                var kernel = Common.initializeKernel();

                kernel.ImportPluginFromType<ConversationSummaryPlugin>();
                var prompts = kernel.ImportPluginFromPromptDirectory("Prompts");

                history.AddUserMessage(userMessage);

                var result = await kernel.InvokeAsync<string>(prompts["GeneralPluggin"],
                            new() { { "input", userMessage }, { "tone", userTone } });

                history.AddAssistantMessage(result.ToString());

                return result.ToString();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
