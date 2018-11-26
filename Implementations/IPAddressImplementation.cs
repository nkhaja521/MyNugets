using GI.Common.XF.Interfaces;
using GI.Common.XF.iOS.Implementation;
using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Xamarin.Forms;

[assembly: Dependency(typeof(IPAddressImplementation))]
namespace GI.Common.XF.iOS.Implementation
{
    class IPAddressImplementation : IIPAddress
    {
        public string GetIPAddress()
        {
            string ipAddress = "";

            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress = addrInfo.Address.ToString();

                        }
                    }
                }
            }

            return ipAddress;
        }
    }
}