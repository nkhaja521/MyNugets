using GI.Common.XF.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GI.Common.XF.Helpers
{
    public static class IPAddressHelper
    {
        public static string GetIPAddress()
        {
            return DependencyService.Get<IIPAddress>().GetIPAddress();
        }
    }
}
