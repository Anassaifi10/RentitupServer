using RentalApp.Application.Common.Interfaces;

namespace RentalApp.Application.UserAccount.Commands.ForgetPassword;

public record ForgetPasswordCommand : IRequest<string>
{
    public string Email { get; set; } = "";
    public string ClientUrl { get; set; } = "";
}

public class ForgetPasswordCommandValidator : AbstractValidator<ForgetPasswordCommand>
{
    public ForgetPasswordCommandValidator()
    {
    }
}

public class ForgetPasswordCommandHandler(IApplicationDbContext context,IUserAccountServices userAccountServices) : IRequestHandler<ForgetPasswordCommand, string>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUserAccountServices _userAccountServices=userAccountServices;

    public async Task<string> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _userAccountServices.ForgetPasswordVarificationLink(request.Email,request.ClientUrl);
        if(result.Item2.Succeeded==false)
        {
            return result.Item2.Errors[0];
        }
        return result.Item1;

    }
}
