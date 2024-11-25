using Microsoft.EntityFrameworkCore;
using SerhendKumbara.Core.Caching;
using SerhendKumbara.Data.Entity;

namespace SerhendKumbara.Services;

public class RegionService
{
    private readonly ICacheService _cacheService;
    private readonly SerhendKumbaraDBContext _dBContext;

    public RegionService(ICacheService cacheService, SerhendKumbaraDBContext dBContext)
    {
        _cacheService = cacheService;
        _dBContext = dBContext;
    }

    public async Task<List<Region>> GetRegions()
    {
        var cacheKey = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        var result = await _cacheService.GetOrCreateAsync(cacheKey, async () =>
        {
            return await _dBContext.Regions.ToListAsync();
        }, DateTime.Now.AddDays(10));

        return result;
    }

    public async Task<Region> UpsertRegion(Region region)
    {
        _cacheService.Clear();
        if (region.RegionID > 0)
        {
            _dBContext.Regions.Update(region);
        }
        else
        {
            _dBContext.Regions.Add(region);
        }

        var result = await _dBContext.SaveChangesAsync() > 0;
        if (!result)
        {
            throw new Exception("Upsert failed.");
        }
        return region;
    }

    public async Task<bool> DeleteRegions(List<int> ids)
    {
        _cacheService.Clear();
        var regions = await _dBContext.Regions.Where(w => ids.Contains(w.RegionID)).ToListAsync();
        bool placemarkIsExist = false;
        foreach (var region in regions)
        {
            placemarkIsExist = _dBContext.Placemarks.Where(w => ids.Contains(w.RegionId ?? 0)).Any();
            if (placemarkIsExist)
            {
                throw new Exception($"Region {region.RegionID} is used by some placemarks.");
            }
            _dBContext.Regions.Remove(region);
        }

        return await _dBContext.SaveChangesAsync() > 0;
    }
}
