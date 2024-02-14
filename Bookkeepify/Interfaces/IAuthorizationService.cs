using Microsoft.AspNetCore.Components.Authorization;

namespace Bookkeepify.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> CheckPageAccessAsync(AuthenticationState authState);
        Task<string> CheckPageUriAsync(AuthenticationState authState);
    }
}
