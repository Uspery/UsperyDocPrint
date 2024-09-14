using Newtonsoft.Json;
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

        public ActionResult Result()
        {
            var jsonData = @"{
    'recebedor': {
        'nome': 'USPERY TECNOLOGIA LTDA',
        'cpf': '57.022.730/0001-00',
        'cep': '06230150',
        'cidade': 'Osasco',
        'rua': 'Ali',
        'complemento': 'aa'
    },
    'pagador': {
        'nome': 'Kauã Lima',
        'cpf': '433.333.498-50'
    },
    'informacoes': {
        'numeroRecibo': '5156',
        'dataEmissao': '2024-09-14T03:00:00.000Z',
        'moeda': 'real',
        'observacoes': 'u,tnrgbtmn th m'
    },
    'itens': [
        {
            'servico': 'bfdbdfb',
            'quantidade': '1',
            'valor': 3455
        }
    ]
}";


            var receiptData = JsonConvert.DeserializeObject<ReceiptViewModel>(jsonData);

            // Obter o nome da aplicação a partir do Assembly
            var assembly = Assembly.GetExecutingAssembly();
            var titleAttribute = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
            var applicationTitle = titleAttribute != null ? titleAttribute.Title : assembly.GetName().Name;
            receiptData.NomeAplicacao = applicationTitle;

            return View(receiptData);
        }
    }
}