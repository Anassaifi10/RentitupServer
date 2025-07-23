using RentalApp.Application.Common.Models;
using RentalApp.Application.UserAccount.Queries.UserInfo;

namespace RentalApp.Application.Common.Interfaces;
public interface IUserAccountServices
{
    Task<(string, Result)> ForgetPasswordVarificationLink(string email,string ClientURI);
    Task<Result> resetPassword(string email, string token, string newPassword);

    Task<Result> updateProfile(string name, string phoeno);
    Task<(Result, UserInfoVM?)> GetMyInfo();
}
