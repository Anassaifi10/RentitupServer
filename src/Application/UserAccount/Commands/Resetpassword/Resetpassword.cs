using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;

namespace RentalApp.Application.UserAccount.Commands.Resetpassword;

public record ResetpasswordCommand : IRequest<Result>
{
    public string token { get; set; } = "";
    public string newPassword { get; set; } = "";
    public string email { get; set; } = "";
}

public class ResetpasswordCommandValidator : AbstractValidator<ResetpasswordCommand>
{
    public ResetpasswordCommandValidator()
    {
    }
}

public class ResetpasswordCommandHandler(IApplicationDbContext context,IUserAccountServices userAccountServices) : IRequestHandler<ResetpasswordCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUserAccountServices _userAccountServices=userAccountServices;

    public async Task<Result> Handle(ResetpasswordCommand request, CancellationToken cancellationToken)
    {
        return await _userAccountServices.resetPassword(request.email, request.token,request.newPassword);
    }
}
