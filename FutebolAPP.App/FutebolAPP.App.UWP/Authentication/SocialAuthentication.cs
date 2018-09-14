using FutebolAPP.App.Authentication;
using FutebolAPP.App.Helpers;
using FutebolAPP.App.UWP.Authentication;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthentication))]
namespace FutebolAPP.App.UWP.Authentication
{
    public class SocialAuthentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            Settings.AuthToken = string.Empty;
            Settings.UserId = string.Empty;

            try
            {
                var user = await client.LoginAsync(provider);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId ?? string.Empty;

                return user;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> LogoutAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
        {
            try
            {
                await client.LogoutAsync();
                Settings.UserId = string.Empty;
                Settings.AuthToken = string.Empty;
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
