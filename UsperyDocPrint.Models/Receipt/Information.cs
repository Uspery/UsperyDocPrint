using System;

namespace UsperyDocPrint.Models
{
    public class Information
    {
        public int Code { get; set; }

        public DateTime Issuance { get; set; }

        public DateTime Expiration { get; set; }

        public string PaymentType { get; set; }

        public decimal Discount { get; set; }

        public decimal Addition { get; set; }

        public decimal FullValue { get; set; }

        public decimal FinalValue
        {
            get
            {
                return (FullValue - Discount) + Addition;
            }
        }

        public string CostCenter { get; set; }

        public string Responsible { get; set; }

        public string Currency { get; set; }

        public string Observations { get; set; }
    }
}
