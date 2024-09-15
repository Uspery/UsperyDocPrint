using System.Collections.Generic;

namespace UsperyDocPrint.Models
{
    public class ReceiptViewModel
    {
        public Receiver Receiver { get; set; }

        public Payer Payer { get; set; }

        public Information Informacoes { get; set; }

        public List<Item> Items { get; set; }
    }
}
