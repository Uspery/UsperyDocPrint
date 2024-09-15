using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsperyDocPrint.Actions
{
    public class HtmlToPdfConverter
    {
        private readonly IConverter _converter;

        public HtmlToPdfConverter(IConverter converter)
        {
            _converter = converter;
        }

        public string ConvertHtmlToPdfBase64(string htmlContent)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4
            },
                Objects = {
                new ObjectSettings() {
                    PagesCount = true,
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Footer" }
                }
            }
            };

            byte[] pdfBytes = _converter.Convert(doc);
            return Convert.ToBase64String(pdfBytes);
        }
    }
}
