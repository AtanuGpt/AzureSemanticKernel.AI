using AzureSemanticKernel.AI.Pluggins;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Planning.Handlebars;

namespace AzureSemanticKernel.AI.Controllers
{
    public class CloudApiController : Controller
    {
        public async Task<string> GetResponse(string userQuestion)
        {
            var kernel = Common.initializeKernel();

            var cloudPlugin = kernel.ImportPluginFromType<CloudApiLibraryPluggin>();

            var arguments = new KernelArguments();

            arguments["input"] = userQuestion;

            OpenAIPromptExecutionSettings openAiPromptExecutionSetting = new()
            {
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            #pragma warning disable SKEXP0060 
            var planner = new HandlebarsPlanner();

            var originalPlan = await planner.CreatePlanAsync(kernel, userQuestion);

            //Console.WriteLine(originalPlan);

            var originalPlanResult = await originalPlan.InvokeAsync(kernel, new KernelArguments(openAiPromptExecutionSetting));
            
            #pragma warning restore SKEXP0060

            return originalPlanResult.ToString();
        }
    }
}
