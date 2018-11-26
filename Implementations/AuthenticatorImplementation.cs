using Foundation;
using GI.Common.XF.Interfaces;
using GI.Common.XF.iOS.Implementation;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Security;
using System;
using System.Linq;
using System.Threading.Tasks;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticatorImplementation))]
namespace GI.Common.XF.iOS.Implementation
{
    class AuthenticatorImplementation : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string tenantUrl, string graphResourceUri, string ApplicationID, string returnUri)
        {
            var authContext = new AuthenticationContext(tenantUrl);
            if (authContext.TokenCache.ReadItems().Any())
            {
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            }

            var uri = new Uri(returnUri);
            var platformParams = new PlatformParameters(UIApplication.SharedApplication.KeyWindow.RootViewController);
            try
            {
                var authResult = await authContext.AcquireTokenAsync(graphResourceUri, ApplicationID, uri, platformParams);
                return authResult;
            }
            catch (AdalException)
            {
                return null;
            }
        }

        public void Logout()
        {
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;

            foreach (var cookie in CookieStorage.Cookies)
            {
                CookieStorage.DeleteCookie(cookie);
            }

            var query = new SecRecord(SecKind.GenericPassword);
            SecKeyChain.Remove(query);
        }

        public void CloseApp()
        {
        }
    }
}