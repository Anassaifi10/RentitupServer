using CleanArchitecture.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Image.Commands.UplodeImage;

public record UplodeImageCommand : IRequest<string>
{
}

public class UplodeImageCommandValidator : AbstractValidator<UplodeImageCommand>
{
    public UplodeImageCommandValidator()
    {
    }
}

public class UplodeImageCommandHandler : IRequestHandler<UplodeImageCommand, string>
{
    private readonly IApplicationDbContext _context;

    public UplodeImageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(UplodeImageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
