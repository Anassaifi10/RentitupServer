using RentalApp.Application.Common.Models;
using RentalApp.Application.Items.Commands.CreateItem;

namespace RentalApp.Application.Common.Interfaces;
public interface IItemServices
{
    Task<(Result Result,string Id)> createItem(CreateItemVm iteminfo);
}
