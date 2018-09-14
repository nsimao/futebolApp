using Android.Webkit;
using FutebolAPP.App.Authentication;
using FutebolAPP.App.Droid.Authentication;
using FutebolAPP.App.Helpers;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthentication))]
namespace FutebolAPP.App.Droid.Authentication
{
    public class SocialAuthentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            Settings.AuthToken = string.Empty;
            Settings.UserId = string.Empty;

            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);
                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId ?? string.Empty;

                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> LogoutAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
        {
            try
            {

                CookieManager.Instance.RemoveAllCookie();
                await client.LogoutAsync();
                Settings.UserId = string.Empty;
                Settings.AuthToken = string.Empty;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}