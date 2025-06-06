namespace RentalApp.Domain.Entities;
public class UserAccount : BaseEntity<string>
{
    public string? FirstName { get; set; }
    public string? PhoneNumber{ get; set; }

    public string? Email { get; set; }

    public string? Profileimage{ get; set; }
    public ICollection<Item> Items { get; set; } = [];
    public ICollection<RentalRequest> RentalRequests { get; set; } = [];
}
