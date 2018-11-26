using GI.Common.XF.Interfaces;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GI.Common.XF.Helpers
{
    public class LoginHelper
    {
        public async Task<AuthenticationResult> AuthenticateLogin(string tenantUrl, string GraphResourceUri, string ApplicationID, string ReturnUri)
        {
            var data = await DependencyService.Get<IAuthenticator>().Authenticate(tenantUrl, GraphResourceUri, ApplicationID, ReturnUri);
            return data;
        }

        public void LogOut(string tenantUrl)
        {
            AuthenticationContext authContext = new AuthenticationContext(tenantUrl);
            authContext.TokenCache.Clear();
            DependencyService.Get<IAuthenticator>().Logout();
        }

        public async Task<bool> GetUserToken(string userName, string uri, string key)
        {
            var nvc = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", userName),
                    new KeyValuePair<string, string>("password", userName + key),
                };

            using (var client = new HttpClient())
            {
                try
                {
                    //var response = await client.PostAsync(uri, new FormUrlEncodedContent(nvc));
                    //UserLoginToken userLoginToken = new UserLoginToken();
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    var getTokenInfo = response.Content.ReadAsStringAsync();
                    //    userLoginToken = JsonConvert.DeserializeObject<UserLoginToken>(getTokenInfo.Result);
                    //    Settings.Token = userLoginToken.access_token;
                    //    Settings.OfficerId = userLoginToken.EnforcementOfficialId;
                    //    return true;
                    //}
                    //else
                    //{
                    //    return false;
                    //}
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
