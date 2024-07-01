using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace AzureSemanticKernel.AI.Pluggins
{

    public class AtsLibraryPluggin
    {
        [KernelFunction, Description("Gets the content of the CV")]
        public static string GeResumeDetails()
        {
            string dir = Directory.GetCurrentDirectory();
            string path = dir + "\\docs\\cv.pdf";
            PdfReader reader = new PdfReader(path);

            string text = string.Empty;
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                text += PdfTextExtractor.GetTextFromPage(reader, page);
            }
            reader.Close();

            return text;
        }
    }
}
