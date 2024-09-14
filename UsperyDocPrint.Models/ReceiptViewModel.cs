using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsperyDocPrint.Models
{
    public class ReceiptViewModel
    {
        public Recebedor Recebedor { get; set; }

        public Pagador Pagador { get; set; }

        public Informacoes Informacoes { get; set; }

        public List<Item> Itens { get; set; }

        public string NomeAplicacao { get; set; }
    }
}
