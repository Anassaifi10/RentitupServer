using Microsoft.EntityFrameworkCore;
using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;
using RentalApp.Application.Items.Commands.CreateItem;
using RentalApp.Domain.Entities;

namespace RentalApp.Infrastructure.Services;
public class ItemServices(IApplicationDbContext context, IImageServices imageServices, IUser user) : IItemServices
{
    private readonly IApplicationDbContext _context = context;
    private readonly IImageServices _imageServices = imageServices;
    private readonly IUser _user = user;
    public async Task<(Result Result, string Id)> createItem(CreateItemVm iteminfo)
    {
        try
        {
            if (_user.Id == null)
            {
                return (Result.Failure(["Owner Id Is not Present"]), string.Empty);
            }

            var useraccount = await _context.UserAccounts.FirstOrDefaultAsync(x => x.Id == _user.Id);
            if (useraccount==null||string.IsNullOrEmpty(useraccount.PhoneNumber))
            {
                return (Result.Failure(["need contact no before Creating the Item"]),"");
            }

            Item newItem = new Item()
            {
                ItemName = iteminfo.ItemName,
                Description = iteminfo.Description,
                Price = iteminfo.Price,
                PriceType = iteminfo.PriceType,
                OwnerId = _user.Id,
            };
            await _context.Items.AddAsync(newItem);
            await _context.SaveChangesAsync(CancellationToken.None);
            //if (iteminfo.Image != null && !string.IsNullOrEmpty(newItem.Id))
            //{
            //    var imageurl = await _imageServices.UplodeImage(iteminfo.Image, "ItemImages", newItem.Id);
            //    newItem.Imageurl = imageurl;
            //    _context.Items.Update(newItem);
            //    await _context.SaveChangesAsync(CancellationToken.None);
            //}

            return (Result.Success(), newItem.Id ?? string.Empty);
        }
        catch (Exception ex)
        {
            return (Result.Failure([ex.Message]), string.Empty);
        }
    }
}
