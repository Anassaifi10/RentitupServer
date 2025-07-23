using RentalApp.Application.Common.Models;
using RentalApp.Application.Items.Commands.CreateItem;
using RentalApp.Application.Items.Queries.GetItemDetails;
using RentalApp.Application.Items.Queries.GetItems;

namespace RentalApp.Application.Common.Interfaces;
public interface IItemServices
{
    Task<(Result Result,string Id)> createItem(CreateItemVm iteminfo);

    Task<(Result result,List<GetItemVm> ?items)> getItems();

    Task<(Result result, ItemDetailsVm? detailsVm)> getItemDetails(string ItemId);
    Task<(Result result, List<ItemDetailsVm> MyItems)> GetMyItem();


}
