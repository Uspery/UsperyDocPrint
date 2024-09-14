using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
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
                'cpf': '433.333.498-50',
                'cep': '06230150',
                'cidade': 'Osasco',
                'rua': 'Ali',
                'complemento': 'aa'
            },
            'pagador': { 'nome': 'Kauã Lima', 'cpf': '433.333.498-50' },
            'informacoes': {
                'numeroRecibo': '5156',
                'dataEmissao': '1215818',
                'moeda': 'real',
                'observacoes': 'AAAA'
            },
            'itens': [
                { 'servico': 'Nao sei', 'quantidade': '1', 'valor': '4248' },
                { 'servico': 'Oka', 'quantidade': '2', 'valor': '1000' }
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