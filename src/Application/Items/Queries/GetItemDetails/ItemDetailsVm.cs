using RentalApp.Domain.Enums;

namespace RentalApp.Application.Items.Queries.GetItemDetails;
public class ItemDetailsVm
{
    public string ItemName { get; set; } = "";
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ItemPriceType PriceType { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string Imageurl { get; set; } = "";

    public int noOfRequestsend { get; set; }

    public string? OwnerName { get; set; } = "";

    public string? OwnerImageUrl { get; set; } = "";

}
