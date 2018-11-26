using CoreBluetooth;
using CoreGraphics;
using GI.Common.XF.Helpers;
using GI.Common.XF.Interfaces;
using GI.Common.XF.iOS.Implementation;
using MWBarcodeScanner;
using System;
using System.Collections;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(BarCodeScannerImplementation))]
namespace GI.Common.XF.iOS.Implementation
{
    public class BarCodeScannerImplementation : IBarcodeScanner//, IScanSuccessCallback
    {
        Scanner scanner;
        private CBCentralManager _centralManager;
        public string ScannedBarcodeText = String.Empty;
        public string BarcodeFormat = String.Empty;


        //     public void ScanEan()
        //     {
        //         //Make an instance of scanner
        //         scanner = new Scanner();
        //         scanner.setInterfaceOrientation("Portrait");
        //         scanner.ScanWithCallback(this);
        //         /*
        //* Customize the scanner by using options below
        //*/

        //         /* Select desired scanner interface orientation
        //* Available options are: LandscapeLeft, LandscapeRight, Portrait, All; Default: LandscapeLeft
        //*/
        //         //scanner.setInterfaceOrientation("All");

        //         /* Toggle visibility of Flash button
        //* Available options are: true, false; Default: true
        //*/
        //         //scanner.useFlash = true;

        //         /* Toggle high resolution scanning - 1280x720 vs normal resolution scaning - 640x480
        //* Available options are: true, false; Default: true
        //*/
        //         //scanner.useHiRes = true;

        //         /* Choose between overlay types
        //* Available options are: OM_NONE, OM_MW (dynamic viewfinder), OM_IMAGE (static image overlay); Default: OM_MW
        //*/
        //         //scanner.overlayMode = Scanner.OM_MW;

        //         /* Toggle visibility of Zoom button
        //* Available options are: true, false; Default: true
        //*/
        //         //scanner.useZoom = true;

        //         /* Choose desired zoom levels
        //* zoomLevel1, zoomLevel2 - zoom level in % ; Default: 150,300
        //* initialZoomLevel       - the initial zoom level index; Available options: 0 (no zoom), 1, 2; Default: 0
        //*/
        //         //scanner.setZoomLevels(200, 400, 1);


        //         /* Set the number of CPU cores to be used
        //* Available options are: 1,2; Default 2
        //*/
        //         //scanner.setMaxThreads(1);

        //         /* Close scanner after successful scan 
        //* Available options are: true, false (continuous scanning if set to false); Default: true; 
        //* if set to false:
        //* 		Use scanner.resumeScanning() - to resume after successful scan
        //* 		Use scanner.closeScanner()   - to close the scanner
        //*/
        //         //scanner.closeScannerOnDecode = false;


        //         /* Use 60 fps preview if available
        //* Available options are: true, false; Default: false;
        //*/
        //         //scanner.use60fps = true;

        //         /* 
        //* Set custom button frame
        //*/
        //         // Defaults:
        //         //			 scanner.setFlashButtonFrame(new CGRect(this.View.Frame.Size.Width-64,0,64,64));
        //         //			 scanner.setCloseButtonFrame(new CGRect(0,0,64,64));
        //         //			 scanner.setZoomButtonFrame(new CGRect(this.View.Frame.Size.Width-64,this.View.Frame.Size.Height-64,64,64));

        //         /* Show on screen location of scanned code
        //* Available options are: true, false; Default: true;
        //* 
        //* Customise line width and line color with the provided params below
        //*/
        //         // scanner.enableShowLocation = false;
        //         // MWOverlay.locationLineColor = 0xff0000;
        //         // MWOverlay.locationLineWidth = 2;
        //         /* Play beep sound on successful scan
        //         * Available options are: true, false; Default: true;
        //         */
        //         //scanner.enableBeepSound = false;


        //         customDecoderInit();

        //         scanner.ScanWithCallback(this);

        //         //scanner.ScanInView(this, new CGRect(0, 0, 100, 50));

        //     }

        //     public void barcodeDetected(MWResult result)
        //     {
        //         if (result != null)
        //         {
        //             try
        //             {
        //                 Settings.LastLogin = DateTime.Now.ToLocalTime();
        //                 ScannedBarcodeText = result.text.ToString();
        //                 BarcodeFormat = result.typeName;
        //             }
        //             catch (Exception ex)
        //             {
        //                 System.Diagnostics.Debug.WriteLine(ex.Message);
        //             }
        //         }
        //         else
        //         {
        //             //handle back pressed
        //         }
        //     }

        public static CGRect RECT_LANDSCAPE_1D = new CGRect(6, 20, 88, 60);
        public static CGRect RECT_LANDSCAPE_2D = new CGRect(20, 6, 60, 88);
        public static CGRect RECT_PORTRAIT_1D = new CGRect(20, 6, 60, 88);
        public static CGRect RECT_PORTRAIT_2D = new CGRect(20, 6, 60, 88);
        public static CGRect RECT_FULL_1D = new CGRect(6, 6, 88, 88);
        public static CGRect RECT_FULL_2D = new CGRect(20, 6, 60, 88);
        public static CGRect RECT_DOTCODE = new CGRect(30, 20, 40, 60);

        public static void customDecoderInit()
        {

            Console.WriteLine("Decoder initialization");

            //register your copy of library with given SDK key
            int registerResult = BarcodeConfig.MWB_registerSDK("Yw3AVpB+ETQzIvoYiNT9nHkBHiHN8gLomWvf4tifoCY=");

            switch (registerResult)
            {
                case BarcodeConfig.MWB_RTREG_OK:
                    Console.WriteLine("Registration OK");
                    break;
                case BarcodeConfig.MWB_RTREG_INVALID_KEY:
                    Console.WriteLine("Registration Invalid Key");
                    break;
                case BarcodeConfig.MWB_RTREG_INVALID_CHECKSUM:
                    Console.WriteLine("Registration Invalid Checksum");
                    break;
                case BarcodeConfig.MWB_RTREG_INVALID_APPLICATION:
                    Console.WriteLine("Registration Invalid Application");
                    break;
                case BarcodeConfig.MWB_RTREG_INVALID_SDK_VERSION:
                    Console.WriteLine("Registration Invalid  SDK Version");
                    break;
                case BarcodeConfig.MWB_RTREG_INVALID_KEY_VERSION:
                    Console.WriteLine("Registration Invalid Key Version");
                    break;
                case BarcodeConfig.MWB_RTREG_INVALID_PLATFORM:
                    Console.WriteLine("Registration Invalid Platform");
                    break;
                case BarcodeConfig.MWB_RTREG_KEY_EXPIRED:
                    Console.WriteLine("Registration Key Expired");
                    break;
                default:
                    break;
            }


            // choose code type or types you want to search for

            // Our sample app is configured by default to search all supported barcodes...
            BarcodeConfig.MWB_setActiveCodes(
                BarcodeConfig.MWB_CODE_MASK_25 |
                BarcodeConfig.MWB_CODE_MASK_39 |
                BarcodeConfig.MWB_CODE_MASK_93 |
                BarcodeConfig.MWB_CODE_MASK_128 |
                BarcodeConfig.MWB_CODE_MASK_AZTEC |
                BarcodeConfig.MWB_CODE_MASK_DM |
                BarcodeConfig.MWB_CODE_MASK_EANUPC |
                BarcodeConfig.MWB_CODE_MASK_PDF |
                BarcodeConfig.MWB_CODE_MASK_QR |
                BarcodeConfig.MWB_CODE_MASK_CODABAR |
                BarcodeConfig.MWB_CODE_MASK_11 |
                BarcodeConfig.MWB_CODE_MASK_MSI |
                BarcodeConfig.MWB_CODE_MASK_RSS |
                BarcodeConfig.MWB_CODE_MASK_MAXICODE |
                BarcodeConfig.MWB_CODE_MASK_POSTAL
            );

            // But for better performance, only activate the symbologies your application requires...
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_25 );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_39 );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_93 );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_128 );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_AZTEC );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_DM );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_EANUPC );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_PDF );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_QR );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_RSS );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_CODABAR );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_DOTCODE );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_11 );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_MSI );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_MAXICODE );
            // BarcodeConfig.MWB_setActiveCodes( BarcodeConfig.MWB_CODE_MASK_POSTAL );


            // Our sample app is configured by default to search both directions...
            BarcodeConfig.MWB_setDirection(BarcodeConfig.MWB_SCANDIRECTION_HORIZONTAL | BarcodeConfig.MWB_SCANDIRECTION_VERTICAL);
            // set the scanning rectangle based on scan direction(format in pct: x, y, width, height)
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_25, RECT_FULL_1D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_39, RECT_FULL_1D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_93, RECT_FULL_1D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_128, RECT_FULL_1D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_AZTEC, RECT_FULL_2D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_DM, RECT_FULL_2D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_EANUPC, RECT_FULL_1D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_PDF, RECT_FULL_1D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_QR, RECT_FULL_2D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_RSS, RECT_FULL_1D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_CODABAR, RECT_FULL_1D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_DOTCODE, RECT_DOTCODE);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_11, RECT_FULL_1D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_MSI, RECT_FULL_1D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_MAXICODE, RECT_FULL_2D);
            BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_POSTAL, RECT_FULL_1D);


            // But for better performance, set like this for PORTRAIT scanning...
            // BarcodeConfig.MWB_setDirection(BarcodeConfig.MWB_SCANDIRECTION_VERTICAL);
            // set the scanning rectangle based on scan direction(format in pct: x, y, width, height)
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_25,     RECT_PORTRAIT_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_39,     RECT_PORTRAIT_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_93,     RECT_PORTRAIT_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_128,    RECT_PORTRAIT_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_AZTEC,  RECT_PORTRAIT_2D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_DM,     RECT_PORTRAIT_2D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_EANUPC, RECT_PORTRAIT_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_PDF,    RECT_PORTRAIT_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_QR,     RECT_PORTRAIT_2D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_RSS,    RECT_PORTRAIT_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_CODABAR,RECT_PORTRAIT_1D);
            // BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_DOTCODE,RECT_DOTCODE);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_11,     RECT_PORTRAIT_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_MSI,    RECT_PORTRAIT_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_MAXICODE,RECT_PORTRAIT_2D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_POSTAL, RECT_PORTRAIT_1D);


            // or like this for LANDSCAPE scanning - Preferred for dense or wide codes...
            // BarcodeConfig.MWB_setDirection(BarcodeConfig.MWB_SCANDIRECTION_HORIZONTAL);
            // set the scanning rectangle based on scan direction(format in pct: x, y, width, height)
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_25,     RECT_LANDSCAPE_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_39,     RECT_LANDSCAPE_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_93,     RECT_LANDSCAPE_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_128,    RECT_LANDSCAPE_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_AZTEC,  RECT_LANDSCAPE_2D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_DM,     RECT_LANDSCAPE_2D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_EANUPC, RECT_LANDSCAPE_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_PDF,    RECT_LANDSCAPE_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_QR,     RECT_LANDSCAPE_2D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_RSS,    RECT_LANDSCAPE_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_CODABAR,RECT_LANDSCAPE_1D);
            // BarcodeConfig.MWBsetScanningRect(BarcodeConfig.MWB_CODE_MASK_DOTCODE,RECT_DOTCODE);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_11,     RECT_LANDSCAPE_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_MSI,    RECT_LANDSCAPE_1D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_MAXICODE,RECT_LANDSCAPE_2D);
            // BarcodeConfig.MWB_setScanningRect(BarcodeConfig.MWB_CODE_MASK_POSTAL, RECT_LANDSCAPE_1D);

            BarcodeConfig.MWB_setMinLength(BarcodeConfig.MWB_CODE_MASK_25, 5);
            BarcodeConfig.MWB_setMinLength(BarcodeConfig.MWB_CODE_MASK_MSI, 5);
            BarcodeConfig.MWB_setMinLength(BarcodeConfig.MWB_CODE_MASK_39, 5);
            BarcodeConfig.MWB_setMinLength(BarcodeConfig.MWB_CODE_MASK_CODABAR, 5);
            BarcodeConfig.MWB_setMinLength(BarcodeConfig.MWB_CODE_MASK_11, 5);

            MWScannerViewController.setActiveParserMask(BarcodeConfig.MWP_PARSER_MASK_NONE);

            //BarcodeConfig.MWB_setFlags(BarcodeConfig.MWB_CODE_MASK_39, BarcodeConfig.MWB_CFG_CODE39_CODE32_ENABLED | BarcodeConfig.MWB_CFG_CODE39_CODE32_PREFIX);
            //BarcodeConfig.MWB_setFlags(BarcodeConfig.MWB_CODE_MASK_EANUPC, BarcodeConfig.MWB_CFG_EANUPC_DONT_EXPAND_UPCE);

            //Enable verification for code93 barcode type
            //BarcodeConfig.MWB_setParam(BarcodeConfig.MWB_CODE_MASK_93, BarcodeConfig.MWB_PAR_ID_VERIFY_LOCATION, BarcodeConfig.MWB_PAR_VALUE_VERIFY_LOCATION_ON);

            // set decoder effort level (1 - 5)
            // for live scanning scenarios, a setting between 1 to 3 will suffice
            // levels 4 and 5 are typically reserved for batch scanning
            BarcodeConfig.MWB_setLevel(2);


            //get and print Library version
            int ver = BarcodeConfig.MWB_getLibVersion();
            int v1 = (ver >> 16);
            int v2 = (ver >> 8) & 0xff;
            int v3 = (ver & 0xff);
            String libVersion = v1.ToString() + "." + v2.ToString() + "." + v3.ToString();
            Console.WriteLine("Lib version: " + libVersion);
        }

        async Task IBarcodeScanner.BarcodeScannerImplementation()
        {
            customDecoderInit();
            //ScanEan();
            scanner = new Scanner();

            scanner.setInterfaceOrientation("Portrait");
            var result = await scanner.Scan();

            Settings.LastLogin = DateTime.Now.ToLocalTime();

            if (result != null)
            {
                Settings.ScannedBarcode = result.code;
                Settings.ScannedBarcodeFormat = result.type;
            }
        }

        public async Task PrintText(string input, string printerName, byte[] imageString)
        {

        }

        //public async Task PrintTicket(TicketSignatureRequest tktSignRequest, string printerName)
        //{


        //    //Scan for BLE Devices
        //    //    _cancellationTokenSource = new CancellationTokenSource();
        //    //    var adapter = new Plugin.BluetoothLE.Adapter();
        //    //    adapter.SetAdapterState(true);
        //    //    ScanConfig sConfig = new ScanConfig();
        //    //    sConfig.ScanType = BleScanType.LowLatency;
        //    //    List<IScanResult> devices = (List<IScanResult>)adapter.Scan(sConfig);

        //    //    //check if BL is available and on
        //    //    if (true)
        //    //    {
        //    //        //Look for WOOSIM in connected devices
        //    //        foreach (var discoveredDevice in devices)
        //    //        {
        //    //            var device = discoveredDevice.Device;
        //    //            if (device.Name == printerName)
        //    //            {
        //    //                IList<IGattService> srvs = (IList<IGattService>)device.DiscoverServices();
        //    //                foreach (var srvr in srvs)
        //    //                {
        //    //                    //if (srvr.Name.Contains(""))
        //    //                    //{
        //    //                    //    IService serv = await discoveredDevice.GetServiceAsync(discoveredDevice.Id, _cancellationTokenSource.Token);
        //    //                    //    IList<ICharacteristic> characs = await serv.GetCharacteristicsAsync();
        //    //                    //    foreach (var charac in characs)
        //    //                    //    {
        //    //                    //        if (charac.Name.Contains("write"))
        //    //                    //        {
        //    //                    //            byte LF = 0x0A;
        //    //                    //            byte[] byteArray;

        //    //                    //            byteArray = Encoding.ASCII.GetBytes(ETicketPrintout.Create(tktSignRequest.Person, tktSignRequest.Ticket, tktSignRequest.Vehicle, tktSignRequest.Location, tktSignRequest.Charges.ToList(), tktSignRequest.TicketMiscs?.ToList()));
        //    //                    //            await charac.WriteAsync(byteArray);

        //    //                    //        }
        //    //                    //    }
        //    //                    //}

        //    //                }

        //    //            }
        //    //        }

        //    //        _cancellationTokenSource.Dispose();
        //    //        _cancellationTokenSource = null;
        //    //    }
        //}

        //public async Task PrintText(string input, string printerName, byte[] imageString)
        //{
        //    using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
        //    {
        //        try
        //        {
        //            if (bluetoothAdapter == null)
        //            {
        //                throw new Exception("No default adapter");
        //                //return;
        //            }

        //            if (!bluetoothAdapter.IsEnabled)
        //            {
        //                throw new Exception("Bluetooth not enabled");
        //            }

        //            BluetoothDevice device = (from bd in bluetoothAdapter.BondedDevices
        //                                      where bd.Name == printerName
        //                                      select bd).FirstOrDefault();

        //            if (device == null)
        //                throw new Exception(printerName + " device not found.");

        //            BluetoothSocket socket = null;
        //            UUID applicationUUID = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");

        //            socket = device.CreateInsecureRfcommSocketToServiceRecord(applicationUUID);
        //            try
        //            {
        //                socket.Connect();
        //            }
        //            catch (Exception)
        //            {
        //            }

        //            var writer = socket.OutputStream;
        //            byte LF = 0x0A;

        //            if (input.Length > 0)
        //            {
        //                byte[] byteArray = Encoding.ASCII.GetBytes(input);
        //                writer.Write(byteArray, 0, byteArray.Length);
        //            }
        //            if (imageString != null)
        //            {
        //                writer.Write(PrinterCommands.SELECT_BIT_IMAGE_MODE, 0, PrinterCommands.SELECT_BIT_IMAGE_MODE.Length);
        //                byte[] str = ConvertImageToBitmap(imageString);
        //                writer.Write(str, 0, str.Length);
        //            }

        //            writer.WriteByte(LF);
        //            writer.WriteByte(LF);
        //            writer.WriteByte(LF);

        //            await writer.FlushAsync();
        //            writer.Close();
        //            socket.Close();
        //        }
        //        catch (Exception ex)
        //        {

        //            throw;
        //        }


        //    }

        //}


        //public async Task PrintTicket(TicketSignatureRequest tktSignRequest)
        //{
        //    var imgOffSign = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
        //    var imgPersonSign = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
        //    if (Helpers.Settings.OfficerSign.Length > 0)
        //    {
        //        imgOffSign = ConvertImageToBitmap(Convert.FromBase64String(Helpers.Settings.OfficerSign));
        //    }

        //    if (Helpers.Settings.PersonSign.Length > 0)
        //    {
        //        imgPersonSign = ConvertImageToBitmap(Convert.FromBase64String(Helpers.Settings.PersonSign));
        //    }

        //    try
        //    {
        //        using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
        //        {
        //            BluetoothSocket socket = GetBluetoothSocket(bluetoothAdapter);
        //            if (socket.IsConnected)
        //            {
        //                var writer = socket.OutputStream;
        //                byte LF = 0x0A;
        //                byte[] byteArray;

        //                byteArray = Encoding.ASCII.GetBytes(ETicketPrintout.Create(tktSignRequest.Person, tktSignRequest.Ticket, tktSignRequest.Vehicle, tktSignRequest.Location, tktSignRequest.Charges.ToList(), tktSignRequest.TicketMiscs?.ToList()));
        //                writer.Write(byteArray, 0, byteArray.Length);

        //                //Officer's Signature
        //                if (Helpers.Settings.OfficerSign.Length > 0)
        //                {
        //                    writer.Write(imgOffSign, 0, imgOffSign.Length);
        //                }
        //                else
        //                {
        //                    byteArray = Encoding.ASCII.GetBytes(Helpers.Settings.UserName);
        //                    writer.Write(byteArray, 0, byteArray.Length);
        //                }

        //                byteArray = Encoding.ASCII.GetBytes(ETicketPrintout.CreateETicketCourtInfo(tktSignRequest.Person, tktSignRequest.Ticket));
        //                writer.Write(byteArray, 0, byteArray.Length);

        //                writer.WriteByte(LF);

        //                if (!tktSignRequest.Ticket.Warning)
        //                {
        //                    byteArray = null;
        //                    //Person's Signature
        //                    if (Helpers.Settings.PersonSign.Length > 0)
        //                    {
        //                        //writer.Write(PrinterCommands.SELECT_BIT_IMAGE_MODE, 0, PrinterCommands.SELECT_BIT_IMAGE_MODE.Length);
        //                        writer.Write(imgPersonSign, 0, imgPersonSign.Length);
        //                    }
        //                    else
        //                    {
        //                        byteArray = Encoding.ASCII.GetBytes("** SIGNATURE WAS REFUSED **");
        //                        writer.Write(byteArray, 0, byteArray.Length);
        //                    }
        //                }

        //                byteArray = null;
        //                //string str = Environment.NewLine;
        //                string str = "";
        //                str += new string('_', 60) + "\r\n";
        //                str += "SIGNATURE IS NOT AN ADMISSION OF GUILT".PadRight(60);
        //                byteArray = Encoding.ASCII.GetBytes(str);
        //                writer.Write(byteArray, 0, byteArray.Length);

        //                byteArray = null;
        //                byteArray = Encoding.ASCII.GetBytes(ETicketPrintout.PrintEndOfTicket(tktSignRequest.Ticket));
        //                writer.Write(byteArray, 0, byteArray.Length);

        //                writer.WriteByte(LF);
        //                writer.WriteByte(LF);
        //                //writer.WriteByte(LF);

        //                Thread.Sleep(8000);

        //                await writer.FlushAsync();
        //                writer.Close();
        //                socket.Close();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}

        //public byte[] ConvertImageToBitmap(byte[] imgByteArray)
        //{
        //    BitmapData data = GetBitmapData(imgByteArray);
        //    BitArray dots = data.Dots;
        //    byte[] width = BitConverter.GetBytes(data.Width);

        //    int offset = 0;
        //    MemoryStream stream = new MemoryStream();
        //    BinaryWriter bw = new BinaryWriter(stream);

        //    bw.Write((char)0x1B);
        //    bw.Write('@');

        //    bw.Write((char)0x1B);
        //    bw.Write('3');
        //    bw.Write((byte)24);

        //    while (offset < data.Height)
        //    {
        //        bw.Write((char)0x1B);
        //        bw.Write('*');         // bit-image mode
        //        bw.Write((byte)33);    // 24-dot double-density
        //        bw.Write(width[0]);  // width low byte
        //        bw.Write(width[1]);  // width high byte

        //        for (int x = 0; x < data.Width; ++x)
        //        {
        //            for (int k = 0; k < 3; ++k)
        //            {
        //                byte slice = 0;
        //                for (int b = 0; b < 8; ++b)
        //                {
        //                    int y = (((offset / 8) + k) * 8) + b;
        //                    // Calculate the location of the pixel we want in the bit array.
        //                    // It'll be at (y * width) + x.
        //                    int i = (y * data.Width) + x;

        //                    // If the image is shorter than 24 dots, pad with zero.
        //                    bool v = false;
        //                    if (i < dots.Length)
        //                    {
        //                        v = dots[i];
        //                    }
        //                    slice |= (byte)((v ? 1 : 0) << (7 - b));
        //                }

        //                bw.Write(slice);
        //            }
        //        }
        //        offset += 24;
        //        bw.Write((char)0x0A);
        //    }
        //    // Restore the line spacing to the default of 30 dots.
        //    bw.Write((char)0x1B);
        //    bw.Write('3');
        //    bw.Write((byte)30);

        //    bw.Flush();
        //    byte[] bytes = stream.ToArray();
        //    return bytes;
        //}
        //public BitmapData GetBitmapData(byte[] imageByteArray)
        //{
        //    //BitmapFactory.Options options = new BitmapFactory.Options();
        //    //options.InMutable = true;
        //    //Bitmap bmp = BitmapFactory.DecodeByteArray(imageByteArray, 0, imageByteArray.Length, options);

        //    //using (var bitmap = bmp)
        //    //{
        //    //    var threshold = 55;
        //    //    var index = 0;
        //    //    double multiplier = 256; // this depends on your printer model.
        //    //    double scale = (double)(multiplier / (double)bitmap.Width);
        //    //    int xheight = (int)(bitmap.Height * scale);
        //    //    int xwidth = (int)(bitmap.Width * scale);
        //    //    var dimensions = xwidth * xheight;
        //    //    var dots = new BitArray(dimensions);

        //    //    for (var y = 0; y < xheight; y++)
        //    //    {
        //    //        for (var x = 0; x < xwidth; x++)
        //    //        {
        //    //            var _x = (int)(x / scale);
        //    //            var _y = (int)(y / scale);

        //    //            int pixel = bitmap.GetPixel(_x, _y);
        //    //            int r = Android.Graphics.Color.GetRedComponent(pixel);
        //    //            int g = Android.Graphics.Color.GetGreenComponent(pixel);
        //    //            int b = Android.Graphics.Color.GetBlueComponent(pixel);

        //    //            var luminance = (int)(r * 0.3 + g * 0.59 + b * 0.11);
        //    //            dots[index] = (luminance > threshold);
        //    //            index++;
        //    //        }
        //    //    }

        //    //    return new BitmapData()
        //    //    {
        //    //        Dots = dots,
        //    //        Height = (int)(bitmap.Height * scale),
        //    //        Width = (int)(bitmap.Width * scale)
        //    //    };
        //    //}
        //}
        //BluetoothSocket GetBluetoothSocket(BluetoothAdapter bluetoothAdapter)
        //{
        //    if (bluetoothAdapter == null)
        //    {
        //        throw new Exception("No default adapter");
        //        //return;
        //    }

        //    if (!bluetoothAdapter.IsEnabled)
        //    {
        //        throw new Exception("Bluetooth not enabled");
        //    }

        //    BluetoothDevice device = (from bd in bluetoothAdapter.BondedDevices
        //                              where bd.Name == printerName
        //                              select bd).FirstOrDefault();

        //    if (device == null)
        //        throw new Exception(printerName + " device not found.");

        //    BluetoothSocket socket = null;
        //    UUID applicationUUID = UUID.FromString("00001101-0000-1000-8000-00805F9B34FB");

        //    socket = device.CreateInsecureRfcommSocketToServiceRecord(applicationUUID);
        //    try
        //    {
        //        socket.Connect();
        //    }
        //    catch (Exception)
        //    {
        //    }

        //    return socket;
        //}
    }

    public class BitmapData
    {
        public BitArray Dots
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }
    }
}