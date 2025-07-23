using RentalApp.Application.Common.Interfaces;

namespace RentalApp.Application.ItemRequest.Queries.GetMyRequest;

public record GetMyRequestQuery : IRequest<List<RequestVm>>
{
}

public class GetMyRequestQueryValidator : AbstractValidator<GetMyRequestQuery>
{
    public GetMyRequestQueryValidator()
    {
    }
}

public class GetMyRequestQueryHandler : IRequestHandler<GetMyRequestQuery, List<RequestVm>>
{
    private readonly IApplicationDbContext _context;

    public GetMyRequestQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<RequestVm>> Handle(GetMyRequestQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
