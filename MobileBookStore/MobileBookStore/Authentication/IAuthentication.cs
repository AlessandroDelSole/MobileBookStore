using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace MobileBookStore.Authentication
{
    public interface IAuthentication
    {
        Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider);
    }
}
