using Microsoft.AspNetCore.Http;
using RentalApp.Domain.Enums;

namespace RentalApp.Application.Items.Commands.CreateItem;
public class CreateItemVm
{
    public string ItemName { get; set; } = "";
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ItemPriceType PriceType { get; set; } 
    public IFormFile ?Image { get; set; }
}
