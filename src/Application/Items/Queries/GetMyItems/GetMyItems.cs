using RentalApp.Application.Common.Interfaces;
using RentalApp.Application.Items.Queries.GetItems;

namespace RentalApp.Application.Items.Queries.GetMyItems;

public record GetMyItemsQuery : IRequest<List<GetItemVm>>
{
}

public class GetMyItemsQueryValidator : AbstractValidator<GetMyItemsQuery>
{
    public GetMyItemsQueryValidator()
    {
    }
}

public class GetMyItemsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetMyItemsQuery, List<GetItemVm>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<List<GetItemVm>> Handle(GetMyItemsQuery request, CancellationToken cancellationToken)
    {

    }
}
