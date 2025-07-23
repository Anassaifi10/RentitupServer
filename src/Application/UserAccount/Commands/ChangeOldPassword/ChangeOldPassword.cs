using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;

namespace RentalApp.Application.UserAccount.Commands.ChangeOldPassword;

public record ChangeOldPasswordCommand : IRequest<Result>
{
    public string oldPassword { get; set; } = "";
    public string newPassword { get; set; } = "";
}

public class ChangeOldPasswordCommandValidator : AbstractValidator<ChangeOldPasswordCommand>
{
    public ChangeOldPasswordCommandValidator()
    {
    }
}

public class ChangeOldPasswordCommandHandler(IApplicationDbContext context,IIdentityService identityService,IUser user) : IRequestHandler<ChangeOldPasswordCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IIdentityService _identityService=identityService;
    private readonly IUser _user=user;

    public async Task<Result> Handle(ChangeOldPasswordCommand request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(_user.Id))
        {
            return Result.Failure(["User id must not be null or Empty"]);
        }
        return await _identityService.changePasswordAsync(_user.Id, request.oldPassword, request.newPassword);
    }
}
