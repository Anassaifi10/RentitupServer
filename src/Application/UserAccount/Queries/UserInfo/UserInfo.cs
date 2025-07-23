using Microsoft.AspNetCore.Authorization;
using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;

namespace RentalApp.Application.UserAccount.Queries.UserInfo;

public record UserInfoQuery : IRequest<(Result, UserInfoVM?)>
{
}

public class UserInfoQueryValidator : AbstractValidator<UserInfoQuery>
{
    public UserInfoQueryValidator()
    {
    }
}
//[Authorize]
public class UserInfoQueryHandler(IApplicationDbContext context,IUserAccountServices userAccountServices) : IRequestHandler<UserInfoQuery, (Result, UserInfoVM?)>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUserAccountServices _userAccountServices=userAccountServices;

    public async Task<(Result,UserInfoVM?)> Handle(UserInfoQuery request, CancellationToken cancellationToken)
    {
        return await _userAccountServices.GetMyInfo();
    }
}
