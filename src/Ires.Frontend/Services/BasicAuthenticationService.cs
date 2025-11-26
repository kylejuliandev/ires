using Ires.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ires.Frontend.Services;

public class BasicAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IresDbContext _dbContext;

    public BasicAuthenticationService(IHttpContextAccessor httpContextAccessor, IresDbContext dbContext)
    {
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    public async Task<bool> AuthenticateAsync(string username, string password, bool rememberMe)
    {
        var httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is not available.");

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user is null)
        {
            return false;
        }

        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);

        if (result == PasswordVerificationResult.Failed)
        {
            return false;
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, "Admin")
        };
        var claimsIdentity = new ClaimsIdentity(claims, "cookie");

        await httpContext.SignInAsync("cookie", new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties()
        {
            AllowRefresh = rememberMe,
            IsPersistent = rememberMe,
            ExpiresUtc = rememberMe ? DateTimeOffset.UtcNow.AddDays(7) : null
        });

        // User is redirected after successful authentication, we
        // return true to satisfy the compiler.
        return true; 
    }

    public async Task SignOutAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is not available.");

        await httpContext.SignOutAsync("cookie", new AuthenticationProperties()
        {
            RedirectUri = "/"
        });
    }
}