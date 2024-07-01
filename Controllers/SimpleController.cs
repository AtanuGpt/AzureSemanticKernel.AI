using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;

namespace AzureSemanticKernel.AI.Controllers
{
    public class SimpleController : Controller
    {
        [HttpPost]
        public async Task<string> GetChatKernelResponse(string userMessage)
        {
            try
            {
                var kernel = Common.initializeKernel();

                var result = await kernel.InvokePromptAsync(userMessage);

                return result.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
