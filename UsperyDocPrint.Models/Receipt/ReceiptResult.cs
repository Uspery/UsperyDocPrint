namespace UsperyDocPrint.Models
{
    public class ReceiptResult
    {
        public ReceiptViewModel ReceiptInfo { get; set; }

        public bool DisplaySaveOptions { get; set; } = false;

        public string AppName { get; set; }

        public string LogoContent { get; set; }
    }
}
