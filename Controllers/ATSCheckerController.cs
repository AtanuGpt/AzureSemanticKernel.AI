using AzureSemanticKernel.AI.Pluggins;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;

namespace AzureSemanticKernel.AI.Controllers
{
    public class ATSCheckerController : Controller
    {
        [HttpPost]
        public async Task<string> MatchCandidateProfileScore(string strJD)
        {
            var kernel = Common.initializeKernel();

            kernel.ImportPluginFromType<AtsLibraryPluggin>();

            string prompt = @"
                                You are an expert resume match maker. You match a resume with the job description provided and come up 
                                with the following answers after detail analysis. 

                                -- Candidate Name :
                                -- Percentage Match :
                                -- Recommendatiion : Based on percentage match, Proceed (>= 80%)/Hold(60-79%)/Reject(<60%) 
                                -- Short Summary of the cv in respect to to the AI and RAG skills and technology only for quick understanding purpose. Do not provide any extra or irrelevant information

                                Here is the Job desciption:
                                " + strJD + @"
                                
                                Here is the content of the resume:
                                {{AtsLibraryPluggin.GeResumeDetails}}
                            ";

            var result = await kernel.InvokePromptAsync(prompt);

            return result.ToString();
        }
    }
}
