using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Import;
using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Westwind.WebView.HtmlToPdf;

namespace UsperyDocPrint.Actions
{
    public class HtmlToPdfConverter
    {
        public string HtmlContent { get; set; }

        public HtmlToPdfConverter(string htmlContent)
        {
            this.HtmlContent = htmlContent;
        }

        public async Task<string> ConvertHtmlToPdfBase64(string title)
        {
            var tempFile = Path.Combine(Path.GetTempPath(), string.Concat(Guid.NewGuid().ToString(), ".html"));
            File.WriteAllText(tempFile, HtmlContent);

            var host = new HtmlToPdfHost();
            var pdfPrintSettings = new WebViewPrintSettings()
            {
                ShouldPrintHeaderAndFooter = true,
                HeaderTitle = string.Concat("Uspery - ", title),
                ShouldPrintBackgrounds = false
            };

            var result = await host.PrintToPdfStreamAsync(tempFile, pdfPrintSettings);

            if (!result.IsSuccess)
                throw new Exception("Failed to convert HTML to PDF", new Exception(result.Message));

            var pdfStrem = result.ResultStream;

            byte[] pdfBytes = new byte[pdfStrem.Length];
            pdfStrem.Read(pdfBytes, 0, pdfBytes.Length);

            return Convert.ToBase64String(pdfBytes);
        }
    }
}
