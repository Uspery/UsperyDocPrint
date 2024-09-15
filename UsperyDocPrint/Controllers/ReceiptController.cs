using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
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
    }
}
