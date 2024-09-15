using System.Collections.Generic;

namespace UsperyDocPrint.Models
{
    public class ReceiptViewModel
    {
        public Recebedor Recebedor { get; set; }

        public Pagador Pagador { get; set; }

        public Informacoes Informacoes { get; set; }

        public List<Item> Itens { get; set; }
    }
}
