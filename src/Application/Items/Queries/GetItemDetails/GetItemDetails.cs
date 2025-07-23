using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;

namespace RentalApp.Application.Items.Queries.GetItemDetails;

public record GetItemDetailsQuery : IRequest<(Result result, ItemDetailsVm? detailsVm)>
{
    public string ItemId { get; set; } = "";
}

public class GetItemDetailsQueryValidator : AbstractValidator<GetItemDetailsQuery>
{
    public GetItemDetailsQueryValidator()
    {
    }
}

public class GetItemDetailsQueryHandler(IApplicationDbContext context,IItemServices itemServices) : IRequestHandler<GetItemDetailsQuery, (Result result, ItemDetailsVm? detailsVm)>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IItemServices _itemServices=itemServices;

    public async Task<(Result result, ItemDetailsVm? detailsVm)> Handle(GetItemDetailsQuery request, CancellationToken cancellationToken)
    {
        return await _itemServices.getItemDetails(request.ItemId);
    }
}
