using DinkToPdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UsperyDocPrint.Actions;
using UsperyDocPrint.Models;

namespace UsperyDocPrint.Controllers.API
{
    [RoutePrefix("api/receipt")]
    public class ApiReceiptController : ApiController
    {
        [HttpPost]
        [Route("Generate")]
        public async Task<IHttpActionResult> Generate([FromBody] ReceiptViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var result = await GenerateHtml(model);
                return Ok(result);

            }catch(Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        [HttpPost]
        [Route("GeneratePDF")]
        public async Task<IHttpActionResult> GeneratePDF([FromBody] ReceiptViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var result = await GenerateHtml(model); 
                
                var converter = new SynchronizedConverter(new PdfTools());
                var htmlToPdfConverter = new HtmlToPdfConverter(converter);

                string pdfBase64 = htmlToPdfConverter.ConvertHtmlToPdfBase64(result);

                return Ok(new { PdfBase64 = pdfBase64 });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        private async Task<string> GenerateHtml(ReceiptViewModel model)
        {
            var receiver = JsonConvert.SerializeObject(model.Receiver);
            var payer = JsonConvert.SerializeObject(model.Payer);
            var information = JsonConvert.SerializeObject(model.Informacoes);
            var items = JsonConvert.SerializeObject(model.Items);

            var postData = new Dictionary<string, string>
            {
                { "receiver", receiver },
                { "payer", payer },
                { "information", information },
                { "items", items },
                { "displaySaveOptions", "true" },
                { "fromApi", "true" }
            };

            using (var client = new HttpClient())
            {
                var baseUrl = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Authority}";
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/html"));
                var response = await client.PostAsync($"{baseUrl}/Receipt/Result", new FormUrlEncodedContent(postData));

                if (response.IsSuccessStatusCode)
                {
                    var resultContentHtml = await response.Content.ReadAsStringAsync();
                    return resultContentHtml;
                }

                throw new Exception("Failed to generate receipt.");
            }
        }
    }
}
