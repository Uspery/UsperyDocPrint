using Newtonsoft.Json;
using System.Collections.Generic;
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
        public ActionResult Result(string receiver, string payer, string information, string items, bool displaySaveOptions = true)
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

            return View(result);
        }

    }
}