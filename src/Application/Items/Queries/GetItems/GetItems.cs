using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Common.Models;

namespace RentalApp.Application.Items.Queries.GetItems;

public record GetItemsQuery : IRequest<(Result result, List<GetItemVm> ?items)>
{
}

public class GetItemsQueryValidator : AbstractValidator<GetItemsQuery>
{
    public GetItemsQueryValidator()
    {
    }
}

public class GetItemsQueryHandler(IApplicationDbContext context,IItemServices itemServices) : IRequestHandler<GetItemsQuery, (Result result, List<GetItemVm> ?items)>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IItemServices _itemServices=itemServices;

    public async Task<(Result result, List<GetItemVm> ?items)> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        return await _itemServices.getItems();
    }
}
