using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Server.Application.Common;

namespace RegistrationWizard.Server.Application.Location.GetCountries;


public class GetCountriesQueryHandler : IHandler<GetCountriesRequest, HandlerResult<IEnumerable<Country>>>
{
    private readonly IIdentityDbContext _context;

    public GetCountriesQueryHandler(IIdentityDbContext identityDbContext)
    {
        _context = identityDbContext;
    }

    public async Task<HandlerResult<IEnumerable<Country>>> Handle(GetCountriesRequest request, CancellationToken ct = default)
    {
        var countries = await _context.Countries.AsNoTracking()
            .Select(x => new Country { Id = x.Id, Name = x.Name })
            .ToListAsync(ct);

        return HandlerResult<IEnumerable<Country>>.Success(countries);
    }
}
