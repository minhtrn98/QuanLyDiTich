using Mapster;
using Microsoft.AspNetCore.Identity;
using QLDT.Domain.Users;
using QLDT.Domain.Users.Repository;
using QLDT.Infrastructure.Identity;
using XResult;

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

    public async Task<Result<Success>> Create(User user, string password)
    {
        AppUser appUser = user.Adapt<AppUser>();
        appUser.UserName = user.Email;
        IdentityResult identityResult = await _userManager.CreateAsync(appUser, password);
        if (identityResult.Succeeded)
        {
            return Result.Success;
        }
        List<Error> errors = identityResult.Errors
            .Select(e => new Error(e.Code, e.Description, ErrorType.Failure))
            .ToList();
        return new Result<Success>(errors);
    }

    public async Task<User?> FindByName(string userName)
    {
        AppUser? appUser = await _userManager.FindByNameAsync(userName);
        User? user = appUser.Adapt<User?>();
        return user;
    }
}
