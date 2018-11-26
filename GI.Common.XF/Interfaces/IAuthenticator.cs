using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace GI.Common.XF.Interfaces
{
    public interface IAuthenticator
    {
        Task<AuthenticationResult> Authenticate(string tenantUrl, string graphResourceUri, string ApplicationID, string returnUri);
        void Logout();
        void CloseApp();
    }
}
