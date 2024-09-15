using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using UsperyDocPrint.Actions;
using UsperyDocPrint.Models;

namespace UsperyDocPrint.Controllers
{
    public class ReceiptController : Controller
    {
        // GET: Receipt
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Result(string receiver, string payer, string information, string items, bool displaySaveOptions = true, bool fromApi = false)
        {
            var receiptData = new ReceiptViewModel
            {
                Receiver = JsonConvert.DeserializeObject<Receiver>(receiver),
                Payer = JsonConvert.DeserializeObject<Payer>(payer),
                Informacoes = JsonConvert.DeserializeObject<Information>(information),
                Items = JsonConvert.DeserializeObject<List<Item>>(items)
            };

            receiptData.Payer.Document = DocumentFormatter.FormatDocument(receiptData.Payer.Document);
            receiptData.Receiver.Document = DocumentFormatter.FormatDocument(receiptData.Receiver.Document);

            var result = new ReceiptResult
            {
                ReceiptInfo = receiptData,
                DisplaySaveOptions = displaySaveOptions
            };

            // Obter o nome da aplicação a partir do Assembly
            var assembly = Assembly.GetExecutingAssembly();
            var titleAttribute = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
            var applicationTitle = titleAttribute != null ? titleAttribute.Title : assembly.GetName().Name;
            result.AppName = applicationTitle;

            result.LogoContent = GetLogoUri();

            if (fromApi)
            {
                // Utilize o nome da view aqui, por exemplo "Result"
                var viewString = RenderViewToString("Result", result, ControllerContext);
                return Content(viewString, "text/html");
            }
            else
            {
                return View(result);
            }
        }

        [HttpGet]
        public ActionResult ResultExample()
        {
            var receiptData = new ReceiptViewModel
            {
                Receiver = new Receiver()
                {
                    Name = "Uspery Example",
                    Document = "00000000000000",
                    Zipcode = "00000-000",
                    City = "City",
                    Street = "Street",
                    Complement = "Complement"
                },
                Payer = new Payer()
                {
                    Name = "Uspery 2 Example",
                    Document = "00000000000000",
                },
                Informacoes = new Information()
                {
                    Code = 123456,
                    Issuance = DateTime.Now,
                    Currency = "BRL",
                    Observations = "Observations",

                    Expiration = DateTime.Now.AddDays(30),
                    PaymentType = "Payment Type",
                    Discount = 10,
                    Addition = 5,
                    FullValue = 100,
                    CostCenter = "Cost Center",
                    Category = "Category",
                    Responsible = "Responsible"
                },
                Items = new List<Item>()
                {
                    new Item()
                    {
                        Service = "Service 1",
                        Amount = "1",
                        Value = 95
                    }
                }
            };

            receiptData.Payer.Document = DocumentFormatter.FormatDocument(receiptData.Payer.Document);
            receiptData.Receiver.Document = DocumentFormatter.FormatDocument(receiptData.Receiver.Document);

            var result = new ReceiptResult
            {
                ReceiptInfo = receiptData,
                DisplaySaveOptions = true
            };

            // Obter o nome da aplicação a partir do Assembly
            var assembly = Assembly.GetExecutingAssembly();
            var titleAttribute = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
            var applicationTitle = titleAttribute != null ? titleAttribute.Title : assembly.GetName().Name;
            result.AppName = applicationTitle;

            result.LogoContent = GetLogoUri();

            return View("Result", result);
        }

        private string RenderViewToString(string viewName, object model, ControllerContext context)
        {
            var viewData = new ViewDataDictionary(model);
            var tempData = new TempDataDictionary();

            var viewEngineResult = ViewEngines.Engines.FindView(context, viewName, null);
            var view = viewEngineResult.View;

            using (var sw = new StringWriter())
            {
                var viewContext = new ViewContext(context, view, viewData, tempData, sw);
                view.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        private string GetLogoUri()
        {
            var logo = Properties.Resource.Logo;

            byte[] imageArray;
            using (MemoryStream ms = new MemoryStream())
            {
                logo.Save(ms);
                imageArray = ms.ToArray();
            }
            string base64Image = Convert.ToBase64String(imageArray);

            return $"data:image/x-icon;base64,{base64Image}";
        }
    }
}
