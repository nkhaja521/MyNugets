using System.Threading.Tasks;

namespace GI.Common.XF.Interfaces
{
    public interface IBarcodeScanner
    {
        Task BarcodeScannerImplementation();
        Task PrintText(string input, string printerName, byte[] imageString);
        //Task PrintTicket(TicketSignatureRequest tktSignRequest, string printerName);
        //Task PrintSummon(TicketSignatureRequest tktSignRequest);
    }
}
