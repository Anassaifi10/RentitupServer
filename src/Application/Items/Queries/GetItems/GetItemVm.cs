using RentalApp.Domain.Entities;
using RentalApp.Domain.Enums;

namespace RentalApp.Application.Items.Queries.GetItems;
public class GetItemVm
{
    public string Id { get; set; } = "";
    public string ItemName { get; set; } = "";
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ItemPriceType PriceType { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string OwnerId { get; set; } = string.Empty;
    public string Imageurl { get; set; } = "";

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Item, GetItemVm>();
        }
    }
}
