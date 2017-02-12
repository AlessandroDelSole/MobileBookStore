using System;
using MobileBookStore.Authentication;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using MobileBookStore.Droid;

[assembly: Dependency(typeof(Authentication))]
namespace MobileBookStore.Droid
{
    public class Authentication: IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
        {
            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);
                return user;
            }
            catch (Exception e)
            {
                e.Data["method"] = "LoginAsync";
            }

            return null;
        }
    }
}