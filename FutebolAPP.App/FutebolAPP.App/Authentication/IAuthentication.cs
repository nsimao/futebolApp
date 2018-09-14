using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutebolAPP.App.Authentication
{
    public interface IAuthentication
    {
        Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null);
        Task<bool> LogoutAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider);
    }
}
