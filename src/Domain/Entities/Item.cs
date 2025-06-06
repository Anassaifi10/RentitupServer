using System.ComponentModel.DataAnnotations.Schema;

namespace RentalApp.Domain.Entities;
public class Item : BaseEntity<string>
{
    public string ItemName { get; set; } = "";
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ItemPriceType PriceType { get; set; } 
    public bool IsAvailable { get; set; } = true;
    public string OwnerId { get; set; } = string.Empty;
    public UserAccount Owner { get; set; } = null!;

    public string Imageurl { get; set; } = "";
    public ICollection<RentalRequest> RentalRequests { get; set; } = [];
}
