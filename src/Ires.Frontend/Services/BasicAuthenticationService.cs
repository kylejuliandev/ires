using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Ires.Frontend.Services;

public class BasicAuthenticationOptions
{
    /// <summary>
    /// Gets a collection of username and hashed base64 password pairs.
    /// </summary>
    public Dictionary<string, string> Users { get; set; } = [];
}

public class BasicAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly BasicAuthenticationOptions _options;

    public BasicAuthenticationService(IHttpContextAccessor httpContextAccessor, IOptions<BasicAuthenticationOptions> options)
    {
        _httpContextAccessor = httpContextAccessor;
        _options = options.Value;
    }

    public async Task<bool> AuthenticateAsync(string username, string password, bool rememberMe)
    {
        var httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is not available.");

        var userExists = _options.Users.Keys.Any(key => key == username);
        if (!userExists)
        {
            return false;
        }

        var passwordHasher = new PasswordHasher<User>();
        var user = new User() { Username = username };
        var result = passwordHasher.VerifyHashedPassword(user, _options.Users[username], password);

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

public record User
{
    public required string Username { get; init; }
}