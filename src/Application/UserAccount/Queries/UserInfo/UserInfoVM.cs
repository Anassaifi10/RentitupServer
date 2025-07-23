namespace RentalApp.Application.UserAccount.Queries.UserInfo;


public class UserInfoVM
{
    public string? FirstName { get; set; }
    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Profileimage { get; set; }

    public bool? Succeeded { get; set; }
    private class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.UserAccount, UserInfoVM>();
        }
    }
}
