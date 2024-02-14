using Bookkeepify.Data;
using Bookkeepify.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bookkeepify.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly AppDbContext _context;
        private readonly NavigationManager _navigationManager;

        public AuthorizationService(AppDbContext context, NavigationManager navigationManager)
        {
            _context = context;
            _navigationManager = navigationManager;
        }

        public async Task<bool> CheckPageAccessAsync(AuthenticationState authState)
        {
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Implement your logic to check if the user has access to the current page based on their permissions
            var currentUrl = _navigationManager.Uri;

            var hasAccess = await _context.Permissions
                .AnyAsync(mp => mp.UserId == userId && currentUrl.Contains(mp.Menu.Route));

            return hasAccess;
        }
        public async Task<string> CheckPageUriAsync(AuthenticationState authState)
        {
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Implement your logic to check if the user has access to the current page based on their permissions
            var currentUrl = _navigationManager.Uri;

            var hasAccess = await _context.Permissions
                .AnyAsync(mp => mp.UserId == userId && currentUrl.Contains(mp.Menu.Route));

            return currentUrl.ToString();
        }
    }
}
