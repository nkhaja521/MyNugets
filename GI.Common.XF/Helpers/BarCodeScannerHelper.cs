using GI.Common.XF.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GI.Common.XF.Helpers
{
    public class BarCodeScannerHelper
    {
        public async Task GetScannedBarCode()
        {
            await DependencyService.Get<IBarcodeScanner>().BarcodeScannerImplementation();
        }
    }
}
