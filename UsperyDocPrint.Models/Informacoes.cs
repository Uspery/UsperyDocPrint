using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsperyDocPrint.Models
{
    public class Informacoes
    {
        public int NumeroRecibo { get; set; }

        public DateTime DataEmissao { get; set; }

        public string Moeda { get; set; }

        public string Observacoes { get; set; }
    }
}
