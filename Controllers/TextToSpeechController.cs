using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using Microsoft.SemanticKernel;

namespace AzureSemanticKernel.AI.Controllers
{
    public class TextToSpeechController : Controller
    {
        const string AZURE_SPEECH_KEY = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        const string AZURE_SPEECH_REGION = "XXXXXXXXXX";

        [HttpGet]
        public async Task<string> GetTextReponse(string userMessage)
        {
            try
            {
                var kernel = Common.initializeKernel();

                string input = @$"You are an helpful assistant who always give answers in 
                                not more than three sentence to every user question. Here is your question \""{userMessage}\""";

                var result = await kernel.InvokePromptAsync(input);

                return result.ToString();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet]
        public async Task GetAudioReponse(string result)
        {
            SpeechConfig speechConfig = SpeechConfig.FromSubscription(AZURE_SPEECH_KEY, AZURE_SPEECH_REGION);
            speechConfig.SpeechSynthesisVoiceName = "en-US-AriaNeural";
            using SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer(speechConfig);

            SpeechSynthesisResult speak = await speechSynthesizer.SpeakTextAsync(result);
        }
    }
}
