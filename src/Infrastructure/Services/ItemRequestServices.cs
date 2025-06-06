using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RentalApp.Application.Common;
using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;
using RentalApp.Domain.Entities;
using RentalApp.Domain.Enums;

namespace RentalApp.Infrastructure.Services;
public class ItemRequestServices(IApplicationDbContext context, IUser user, IHubContext<RentalHub, IRentalHub> hub) : IItemRequestService
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUser _user = user;
    private readonly IHubContext<RentalHub, IRentalHub> _rentalHub = hub;
    public async Task<Result> AcceptItemRequest(string requestId)
    {
        try
        {
            var request = await _context.RentalRequests.Include(x => x.Item).FirstOrDefaultAsync(x => x.Id == requestId);
            if (request == null)
            {
                return Result.Failure(["Item id is not Valid"]);
            }

            var exixtRental = await _context.Rentals.Where(x => x.requestId == requestId && x.CreatedBy == _user.Id).ToListAsync();
            if (exixtRental != null)
            {
                return Result.Failure(["I already Accet this Request "]);
            }

            var newRental = new Rental()
            {
                requestId = requestId,

            };

            if (request.Item?.PriceType == ItemPriceType.Days)
            {

                newRental.RentEndDate = DateTime.Now.AddDays(Convert.ToDouble(request?.RentTimeLength));
            }
            else
            {
                newRental.RentEndDate = DateTime.Now.AddHours(Convert.ToDouble(request?.RentTimeLength));
            }

            await _context.Rentals.AddAsync(newRental);
            await _context.SaveChangesAsync(CancellationToken.None);

            await _rentalHub.Clients.User(request?.RenterId ?? "").AccepRequest();
            await _rentalHub.Clients.User(_user.Id ?? "").AccepRequestSuccessfully();
            return Result.Success();

        }
        catch (Exception ex)
        {
            return Result.Failure([ex.Message]);
        }
    }
    public async Task<Result> SendItemRequest(string itemId, string message, decimal rentLength)
    {
        try
        {
            if (string.IsNullOrEmpty(itemId))
            {
                return Result.Failure(["item Id is not presenty"]);
            }
            if (rentLength <= 0)
            {
                return Result.Failure(["rentRequestLength is not valid plese provide more than 0 "]);
            }
            var useraccount = await _context.UserAccounts.FirstOrDefaultAsync(x => x.Id == _user.Id);
            if (useraccount == null || string.IsNullOrEmpty(useraccount.PhoneNumber))
            {
                return Result.Failure(["need contact no before Creating the Item"]);
            }
            var Item = await _context.Items.FirstOrDefaultAsync(x => x.Id == itemId);
            if (Item == null)
            {
                return Result.Failure(["Invalid ItemId Please Provide a valid ItemId"]);
            }
            var existrentalRequest = await _context.RentalRequests.Where(x => x.RenterId == _user.Id && x.ItemId == itemId).ToListAsync();
            if (existrentalRequest.Any()) { return Result.Failure(["Already Apply for this Request"]); }

            RentalRequest newRequest = new RentalRequest()
            {
                ItemId = itemId,
                RenterId = _user.Id ?? "",
                AdditionalComment = message,
                RentTimeLength = rentLength
            };
            await _context.RentalRequests.AddAsync(newRequest);
            await _context.SaveChangesAsync(CancellationToken.None);
            await _rentalHub.Clients.User(Item.OwnerId).ReciveRequest(message);
            await _rentalHub.Clients.User(_user.Id ?? "").SendRequestSuccessfully();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure([ex.Message]);
        }
    }
} 

