using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;

namespace RentalApp.Application.ItemRequest.Commands.SendItemRequest;

public record SendItemRequestCommand : IRequest<Result>
{
    public string? ItemId { get; set; }
    public string? message { get; set; }

    public decimal RentalLength { get; set; }
}

public class SendItemRequestCommandValidator : AbstractValidator<SendItemRequestCommand>
{
    public SendItemRequestCommandValidator()
    {
    }
}

public class SendItemRequestCommandHandler(IApplicationDbContext context,IItemRequestService itemRequestService) : IRequestHandler<SendItemRequestCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IItemRequestService _itemRequestService=itemRequestService;

    public async Task<Result> Handle(SendItemRequestCommand request, CancellationToken cancellationToken)
    {
        return await _itemRequestService.SendItemRequest(request.ItemId??"", request.message??"", request.RentalLength);
    }
}
