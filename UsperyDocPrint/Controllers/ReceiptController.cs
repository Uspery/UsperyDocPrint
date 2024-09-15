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
        public ActionResult Result(string recebedor, string pagador, string informacoes, string itens)
        {
            var receiptData = new ReceiptViewModel
            {
                Recebedor = JsonConvert.DeserializeObject<Recebedor>(recebedor),
                Pagador = JsonConvert.DeserializeObject<Pagador>(pagador),
                Informacoes = JsonConvert.DeserializeObject<Informacoes>(informacoes),
                Itens = JsonConvert.DeserializeObject<List<Item>>(itens)
            };

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

            return View(result);
        }

    }
}