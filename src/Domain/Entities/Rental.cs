namespace RentalApp.Domain.Entities;
public class Rental : BaseEntity<string>
{
    public string requestId { get; set; } = "";

    public DateTime RentEndDate { get; set; }

    public RentalStatus RentalStatus { get; set; }=RentalStatus.Active;

    public RentalRequest RentalRequest { get; set; } = null!;
}
