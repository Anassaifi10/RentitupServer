
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RentalApp.Application.Common.Models;
using RentalApp.Application.Common.Security;
using RentalApp.Application.UserAccount.Commands.ChangeOldPassword;
using RentalApp.Application.UserAccount.Commands.ForgetPassword;
using RentalApp.Application.UserAccount.Commands.Resetpassword;
using RentalApp.Application.UserAccount.Commands.UpdateProfile;
using RentalApp.Application.UserAccount.Commands.UpdateUserImage;
using RentalApp.Application.UserAccount.Queries.UserInfo;

namespace RentalApp.Web.Endpoints;

public class UserAccount : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetMyInfo, nameof(GetMyInfo))
            .MapPut(updateProfile, nameof(updateProfile))
            .MapPut(UpdateUserImage, nameof(UpdateUserImage))
            .MapPut(ChangeOldPassword, nameof(changePassword));

        app.MapGroup(this)
            .MapPost(forgetPasswordLink, nameof(forgetPasswordLink))
            .MapPost(changePassword, "resetPassword");

    }
    public async Task<string> forgetPasswordLink(ISender sender, ForgetPasswordCommand command) => await sender.Send(command);

    public async Task<Result> changePassword(ISender sender, ResetpasswordCommand command) => await sender.Send(command);

    public async Task<Results<Ok<UserInfoVM>, BadRequest<Result>>> GetMyInfo(ISender sender)
    {
        var respo = await sender.Send(new UserInfoQuery());
        if (respo.Item1.Succeeded)
        {
            return TypedResults.Ok(respo.Item2);
        }
        return TypedResults.BadRequest(respo.Item1);
    }

    public async Task<Result> updateProfile(ISender sender, UpdateProfileCommand updateProfileCommand) => await sender.Send(updateProfileCommand);

    public async Task<Result> UpdateUserImage(ISender sender, [FromForm]IFormFile Image)
    {
        var command=new UpdateUserImageCommand();
        command.Image = Image;
        return await sender.Send(command);
     }

    public async Task<Result> ChangeOldPassword(ISender sender, ChangeOldPasswordCommand command) => await sender.Send(command);
}
