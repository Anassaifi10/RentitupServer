using System.Globalization;

namespace RentalApp.Domain.Entities;
public class RentalRequest : BaseEntity<string>
{
    public string ?ItemId { get; set; } 
    public string RenterId { get; set; } = "";

    public decimal RentTimeLength { get; set; }

    public string AdditionalComment { get; set; } = "";
    public RentalRequestStatus Status { get; set; }=RentalRequestStatus.Pending;

    public Item? Item { get; set; }

    public UserAccount RentalRequestUser { get; set; } = null!;

    public Rental Rental { get; set; } =null!;
}
