using AutoMapper;
using CloudinaryDotNet.Core;
using Microsoft.EntityFrameworkCore;
using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Mappings;
using RentalApp.Application.Common.Models;
using RentalApp.Application.Items.Commands.CreateItem;
using RentalApp.Application.Items.Queries.GetItemDetails;
using RentalApp.Application.Items.Queries.GetItems;
using RentalApp.Domain.Entities;

namespace RentalApp.Infrastructure.Services;
public class ItemServices(IApplicationDbContext context, IImageServices imageServices, IUser user, IMapper mapper) : IItemServices
{
    private readonly IApplicationDbContext _context = context;
    private readonly IImageServices _imageServices = imageServices;
    private readonly IUser _user = user;
    private readonly IMapper _mapper=mapper;
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

    public async Task<(Result result, ItemDetailsVm? detailsVm)> getItemDetails(string ItemId)
    {
        try
        {
            var item= await _context.Items.FirstOrDefaultAsync(i => i.Id == ItemId);
            if(item==null)
            {
                return (Result.Failure(["invalid ItemId"]), null);
            }
            var _ownerinfo = await _context.UserAccounts.FirstOrDefaultAsync(u=>u.Id==item.OwnerId);
            if(_ownerinfo==null)
            {
               return (Result.Failure(["Item Owner is not found"]),null);
            }
            ItemDetailsVm ItemDetailsVm = new()
            {
                Description = item.Description,
                Price = item.Price,
                PriceType = item.PriceType,
                ItemName=item.ItemName,
                Imageurl= item.Imageurl,
                IsAvailable=item.IsAvailable,
                noOfRequestsend=item.RentalRequests.Count(),
                OwnerName=_ownerinfo.FirstName,
                OwnerImageUrl=_ownerinfo.Profileimage,
            };
            return (Result.Success(), ItemDetailsVm);

        }catch(Exception  ex)
        {
            return (Result.Failure([ex.Message]), null);
        }
    }

    public async Task<(Result result, List<GetItemVm> ?items)> getItems()
    {
        try
        {
            var ItemServices= await _context.Items.Where((i)=>i.CreatedBy!=_user.Id).ProjectToListAsync<GetItemVm>(_mapper.ConfigurationProvider);
            return (Result.Success(), ItemServices);
        }
        catch (Exception ex)
        {
            return (Result.Failure([ex.Message]), null);
        }
    }

    public async Task<(Result result, List<ItemDetailsVm> MyItems)> GetMyItem()
    {
        try
        {
            var myItems = await _context.Items.Where(x => x.Id == _user.Id).ProjectToListAsync<ItemDetailsVm>(_mapper.ConfigurationProvider);

            return (Result.Success(), myItems);
        }
        catch (Exception ex)
        {
            return (Result.Failure([ex.Message]), []);
        }
    }
}
