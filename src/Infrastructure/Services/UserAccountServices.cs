using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;
using RentalApp.Application.UserAccount.Queries.UserInfo;
using RentalApp.Infrastructure.Identity;

namespace RentalApp.Infrastructure.Services;
public class UserAccountServices(UserManager<ApplicationUser> userManager, IEmailSenderServices emailSenderServices,IUser user,IApplicationDbContext context,IMapper mapper) : IUserAccountServices
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IEmailSenderServices _emailSender = emailSenderServices;
    private readonly IUser _user=user;
    private readonly IApplicationDbContext _context=context;
    private readonly IMapper _mapper=mapper;

    public async Task<(string, Result)> ForgetPasswordVarificationLink(string email, string ClientURI)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return ("", Result.Success());
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.HtmlEncode(token);
            var resetLink = $"{ClientURI}?token={encodedToken}&email={email}";

            var htmlMessage = $@"
                        <p>Hello,</p>
                        <p>Click the link below to reset your password:</p>
                        <p><a href='{resetLink}'>Reset Password</a></p>
                        <p>If you did not request this, ignore this email.</p>";

            //await _emailSender.SendEmailAsync(email, "Reset Password", htmlMessage);

            return (resetLink, Result.Success());

        }
        catch (Exception ex)
        {
            return ("", Result.Failure([ex.Message]));
        }
    }
    public async Task<Result> resetPassword(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return Result.Failure(new[] { "User not found" });
        var decodedToken = WebUtility.HtmlDecode(token);
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        if (result.Succeeded)
        {
            return Result.Success();
        }

        var errors = result.Errors.Select(e => e.Description).ToArray();
        return Result.Failure(errors);
    }

    public async Task<(Result,UserInfoVM?)> GetMyInfo()
    {
        try
        {
            if(string.IsNullOrEmpty(_user.Id))
            {
                return (Result.Failure(["User not found"]),null);
            }

            var user = await _context.UserAccounts.Where(x=>x.Id==_user.Id).ProjectTo<UserInfoVM>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

            if(user == null)
            {
                return (Result.Failure(["User not found"]), null);
            }
            user.Succeeded = true;

            return (Result.Success(), user);
        }
        catch(Exception ex) 
        {
            return (Result.Failure([ex.Message]), null);
        }
    }

    public async Task<Result> updateProfile(string name, string phoeno)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
            {
                return Result.Failure(["empty username"]);
            }
            var existuser = await _context.UserAccounts.FirstOrDefaultAsync(u => u.Id == _user.Id);
            if (existuser == null)
            {
                return Result.Failure(["User Not Found"]);
            }
            existuser.FirstName = name;
            existuser.PhoneNumber = phoeno;
            await _context.SaveChangesAsync(CancellationToken.None);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure([ex.Message]);
        }
    }
}
