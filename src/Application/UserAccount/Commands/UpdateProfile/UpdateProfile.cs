using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;

namespace RentalApp.Application.UserAccount.Commands.UpdateProfile;

public record UpdateProfileCommand : IRequest<Result>
{
    public string username { get; set; } = "";
    public string Phoneno { get; set; } = "";
}

public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
    {
    }
}

public class UpdateProfileCommandHandler(IApplicationDbContext context,IUserAccountServices userAccountServices) : IRequestHandler<UpdateProfileCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUserAccountServices _userAccountServices=userAccountServices;

    public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        return await _userAccountServices.updateProfile(request.username, request.Phoneno);
    }
}
