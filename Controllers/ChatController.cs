using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;

namespace AzureSemanticKernel.AI.Controllers
{
    public class ChatController : Controller
    {
        private string key = Environment.GetEnvironmentVariable("AZURE_OPENAI_KEY");
        private string model = Environment.GetEnvironmentVariable("AZURE_OPENAI_MODEL");
        private string endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> GetChatKernelResponse(string userMessage, string tone)
        {
            try
            {
                var kernel = GetKernel();

                string input = @$"You are an helpful who always gives a/an {tone} answer to every question. Here is your question \""{userMessage}\""";

                var result = await kernel.InvokePromptAsync(input);

                return result.ToString();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Kernel GetKernel()
        {
            var kernelBuilder = Kernel.CreateBuilder();
            kernelBuilder.Services.AddAzureOpenAIChatCompletion(model, endpoint, key);
            var kernel = kernelBuilder.Build();

            return kernel;
        }
    }
}
