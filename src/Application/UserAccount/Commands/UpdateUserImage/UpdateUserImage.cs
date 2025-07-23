using Microsoft.AspNetCore.Http;
using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;

namespace RentalApp.Application.UserAccount.Commands.UpdateUserImage;

public record UpdateUserImageCommand : IRequest<Result>
{
    public IFormFile? Image { get; set; }
}

public class UpdateUserImageCommandValidator : AbstractValidator<UpdateUserImageCommand>
{
    public UpdateUserImageCommandValidator()
    {
    }
}

public class UpdateUserImageCommandHandler(IApplicationDbContext context,IUser user,IImageServices imageServices) : IRequestHandler<UpdateUserImageCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user=user;
    private readonly IImageServices _imageServices=imageServices;

    public async Task<Result> Handle(UpdateUserImageCommand request, CancellationToken cancellationToken)
    {
        if (request.Image == null)
        {
            return Result.Failure(["Please Select the Image first"]);
        }
        if(string.IsNullOrEmpty(_user.Id))
        {
            return Result.Failure(["User Id is Invalid"]);
        }
        return await _imageServices.UplodeImage(request.Image, "user", _user.Id);
    }
}
