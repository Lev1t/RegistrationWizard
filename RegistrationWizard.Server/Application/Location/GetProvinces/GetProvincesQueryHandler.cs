
using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Server.Application.Common;

namespace RegistrationWizard.Server.Application.Location.GetProvinces;

public class GetProvincesQueryHandler : IHandler<GetProvinceRequest, HandlerResult<IEnumerable<Province>>>
{
    private readonly IIdentityDbContext _context;

    public GetProvincesQueryHandler(IIdentityDbContext identityDbContext)
    {
        _context = identityDbContext;
    }

    public async Task<HandlerResult<IEnumerable<Province>>> Handle(GetProvinceRequest request, CancellationToken ct = default)
    {
        var provinces = await _context.Provinces.AsNoTracking()
            .Where(x => x.CountryId == request.CountryId)
            .Select(x => new Province { Id = x.Id, Name = x.Name })
            .ToListAsync(ct);

        return HandlerResult<IEnumerable<Province>>.Success(provinces);
    }
}
