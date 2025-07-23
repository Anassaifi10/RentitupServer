using RentalApp.Application.Common.Models;
using RentalApp.Application.ItemRequest.Queries.GetMyRequest;

namespace RentalApp.Application.Common.Interfaces;
public interface IItemRequestService
{
    Task<Result> SendItemRequest(String itemId,string message,decimal rentLength);
    Task<Result> AcceptItemRequest(string requestId);

    Task<(Result,List<RequestVm>)> getMyAllRequest();
}
