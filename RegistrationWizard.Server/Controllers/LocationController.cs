using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.Server.Application.Common;
using RegistrationWizard.Server.Application;
using RegistrationWizard.Server.Application.Location.GetCountries;
using RegistrationWizard.Server.Application.Location.GetProvinces;

namespace RegistrationWizard.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly IHandler<GetCountriesRequest, HandlerResult<IEnumerable<Country>>> _getCountriesHandler;
    private readonly IHandler<GetProvinceRequest, HandlerResult<IEnumerable<Province>>> _getProvincesHandler;

    public LocationController(
        IHandler<GetCountriesRequest, HandlerResult<IEnumerable<Country>>> getCountriesHandler,
        IHandler<GetProvinceRequest, HandlerResult<IEnumerable<Province>>> getProvincesHandler)
    {
        _getCountriesHandler = getCountriesHandler;
        _getProvincesHandler = getProvincesHandler;
    }

    //TODO: replace with endpoints to get rid of redundant injections
    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries(CancellationToken ct)
    {
        var result = await _getCountriesHandler.Handle(new GetCountriesRequest(), ct);
        return result.ResultType switch
        {
            HandlerResultType.Success => Ok(result.Data),
            _ => throw new NotSupportedException("Unexpected ResultType.")
        };
    }

    [HttpGet("country/{countryId:int}/provinces")]
    public async Task<IActionResult> GetProvinces(int countryId, CancellationToken ct)
    {
        var result = await _getProvincesHandler
            .Handle(new GetProvinceRequest { CountryId = countryId }, ct);

        return result.ResultType switch
        {
            HandlerResultType.Success => Ok(result.Data),
            _ => throw new NotSupportedException("Unexpected ResultType.")
        };
    }
}
