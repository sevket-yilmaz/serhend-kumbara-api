using Microsoft.AspNetCore.Mvc;
using SerhendKumbara.Core.HTTP;
using SerhendKumbara.Data.Entity;
using SerhendKumbara.Services;

namespace SerhendKumbara.Controllers;

[ApiController]
[Route("[controller]")]
public class RegionController : ControllerBase
{
    private readonly RegionService _regionService;

    public RegionController(RegionService regionService)
    {
        _regionService = regionService;
    }

    [HttpGet]
    public async Task<DataResult<List<Region>>> Get() => new SuccessDataResult<List<Region>>(await _regionService.GetRegions());

    [HttpPost]
    public async Task<Region> Upsert(Region regions)
    {
        return await _regionService.UpsertRegion(regions);
    }

    [HttpDelete]
    public async Task<bool> Delete(List<int> ids)
    {
        return await _regionService.DeleteRegions(ids);
    }
}
