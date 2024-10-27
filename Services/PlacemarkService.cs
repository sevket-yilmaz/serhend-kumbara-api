using Microsoft.EntityFrameworkCore;
using SerhendKumbara.Core.Caching;
using SerhendKumbara.Data.Entity;

namespace SerhendKumbara.Services;

public class PlacemarkService
{
    private readonly ICacheService _cacheService;
    private readonly SerhendKumbaraDBContext _dBContext;

    public PlacemarkService(ICacheService cacheService, SerhendKumbaraDBContext dBContext)
    {
        _cacheService = cacheService;
        _dBContext = dBContext;
    }

    public async Task<List<Placemark>> GetPlacemarks()
    {
        var cacheKey = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        var result = await _cacheService.GetOrCreateAsync(cacheKey, async () =>
        {
            return await _dBContext.Placemarks.Where(w => w.Status == PlacemarkStatus.Active).ToListAsync();
        }, DateTime.Now.AddDays(10));

        return result;
    }

    public async Task<List<Placemark>> GetAllPlacemarks()
    {
        return await _dBContext.Placemarks.ToListAsync();
    }

    public async Task<Placemark> UpsertPlacemark(Placemark placemark)
    {
        _cacheService.Clear();
        if (placemark.PlacemarkID > 0)
        {
            _dBContext.Placemarks.Update(placemark);
        }
        else
        {
            _dBContext.Placemarks.Add(placemark);
        }

        var result = await _dBContext.SaveChangesAsync() > 0;
        if (!result)
        {
            throw new Exception("Upsert failed.");
        }
        return placemark;
    }

    public async Task<bool> UpsertPlacemarks(List<Placemark> placemarks)
    {
        _cacheService.Clear();
        foreach (var placemark in placemarks)
        {
            if (placemark.PlacemarkID > 0)
            {
                _dBContext.Placemarks.Update(placemark);
            }
            else
            {
                _dBContext.Placemarks.Add(placemark);
            }
        }
        return await _dBContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeletePlacemarks(List<int> ids)
    {
        _cacheService.Clear();
        var placemarks = await _dBContext.Placemarks.Where(w => ids.Contains(w.PlacemarkID)).ToListAsync();
        foreach (var placemark in placemarks)
        {
            placemark.Status = PlacemarkStatus.Passive;
            _dBContext.Placemarks.Update(placemark);
        }

        return await _dBContext.SaveChangesAsync() > 0;
    }
}
