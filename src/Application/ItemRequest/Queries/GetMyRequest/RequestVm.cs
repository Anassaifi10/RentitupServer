namespace RentalApp.Application.ItemRequest.Queries.GetMyRequest;
public class RequestVm
{
    public string RequestId { get; set; } = "";
    public string RenterEmail { get; set; } = "";
    public decimal RentTimeLength { get; set; }
    public string ItemImageurl { get; set; } = "";
    public string AdditionalComment { get; set; } = "";
    public decimal TotalPayment { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.RentalRequest, RequestVm>();
        }
    }
}
