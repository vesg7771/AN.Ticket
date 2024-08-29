using AN.Ticket.Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AN.Ticket.Infra.Data.Identity.Services;
public class AuthenticateService : IAuthenticate
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthenticateService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<bool> Authentication(string email, string password, bool rememberMe)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return false;

        var passwordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!passwordValid)
            return false;

        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        var isSupport = await _userManager.IsInRoleAsync(user, "Support");

        if (!isAdmin && !isSupport) return false;

        var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: true);

        return result.Succeeded;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<(bool success, string msg)> RegisterUser(
        string fullName,
        string email,
        string password,
        bool isPersistent
    )
    {
        var applicationUser = new ApplicationUser(fullName, email);

        var result = await _userManager.CreateAsync(applicationUser, password);

        if (result.Succeeded)
        {
            await EnsureRolesExistAsync();

            await _userManager.AddToRoleAsync(applicationUser, "Support");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, applicationUser.UserName),
                new Claim(ClaimTypes.Email, applicationUser.Email)
            };

            await _userManager.AddClaimsAsync(applicationUser, claims);

            await _signInManager.SignInAsync(applicationUser, isPersistent);
        }

        var errorMsg = result.Errors.FirstOrDefault()?.Description ?? "Erro desconhecido.";
        return (result.Succeeded, errorMsg);
    }

    public async Task<bool> EmailExists(string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    public async Task<bool> UpdateUserProfile(string userId, string imgPath)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            user.UpdateProfilePicture(imgPath);
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        return false;
    }

    private async Task EnsureRolesExistAsync()
    {
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await _roleManager.RoleExistsAsync("Support"))
        {
            await _roleManager.CreateAsync(new IdentityRole("Support"));
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }
    }
}
