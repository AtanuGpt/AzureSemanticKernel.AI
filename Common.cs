using AzureSemanticKernel.AI.Pluggins;
using Microsoft.SemanticKernel;

namespace AzureSemanticKernel.AI
{
    public static class Common
    {
        private static string key = Environment.GetEnvironmentVariable("AZURE_OPENAI_KEY");
        private static string model = Environment.GetEnvironmentVariable("AZURE_OPENAI_MODEL");
        private static string endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");

        private static string open_ai_key = Environment.GetEnvironmentVariable("OPEN_AI_KEY");
        private static string open_ai_org = Environment.GetEnvironmentVariable("OPEN_AI_ORG_ID");

        public static Kernel initializeKernel()
        {
            var kernelBuilder = Kernel.CreateBuilder();
            kernelBuilder.Services.AddAzureOpenAIChatCompletion(model, endpoint, key);
            var kernel = kernelBuilder.Build();

            return kernel;
        }

        public static Kernel initializeOpenAIKernel()
        {
            var kernelBuilder = Kernel.CreateBuilder();

            kernelBuilder.AddOpenAIChatCompletion
                (
                    "gpt-4o",
                    open_ai_key,
                    open_ai_org
                );

            var kernel = kernelBuilder.Build();

            return kernel;
        }
    }
}
