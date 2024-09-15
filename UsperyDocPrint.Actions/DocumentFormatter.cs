using System;
using System.Text.RegularExpressions;

namespace UsperyDocPrint.Actions
{
    public static class DocumentFormatter
    {
        public static string FormatDocument(string document)
        {
            if (string.IsNullOrWhiteSpace(document))
            {
                throw new ArgumentException("Document cannot be null or empty", nameof(document));
            }

            document = Regex.Replace(document, @"\D", ""); // Remove all non-digit characters

            if (document.Length == 11) // CPF
            {
                return Convert.ToUInt64(document).ToString(@"000\.000\.000\-00");
            }
            else if (document.Length == 14) // CNPJ
            {
                return Convert.ToUInt64(document).ToString(@"00\.000\.000\/0000\-00");
            }
            else
            {
                throw new ArgumentException("Invalid document length", nameof(document));
            }
        }
    }
}
