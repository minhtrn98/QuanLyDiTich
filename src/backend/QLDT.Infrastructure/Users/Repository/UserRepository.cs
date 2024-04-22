using Mapster;
using Microsoft.AspNetCore.Identity;
using QLDT.Domain.Users;
using QLDT.Domain.Users.Repository;
using QLDT.Infrastructure.Identity;

namespace QLDT.Infrastructure.Users.Persistence;

internal sealed class UserRepository : IUserRepository
{
    private readonly UserManager<AppUser> _userManager;

    public UserRepository(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CheckPassword(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user.Adapt<AppUser>(), password);
    }

    public async Task<bool> Create(User user, string password)
    {
        AppUser appUser = user.Adapt<AppUser>();
        IdentityResult identityResult = await _userManager.CreateAsync(appUser, password);
        return identityResult.Succeeded;
    }

    public async Task<User?> FindByName(string userName)
    {
        AppUser? appUser = await _userManager.FindByNameAsync(userName);
        User? user = appUser.Adapt<User?>();
        return user;
    }
}
