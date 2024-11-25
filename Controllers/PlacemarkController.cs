using Microsoft.AspNetCore.Mvc;
using SerhendKumbara.Core.HTTP;
using SerhendKumbara.Data.Entity;
using SerhendKumbara.Data.Seed;
using SerhendKumbara.Services;

namespace SerhendKumbara.Controllers;

[ApiController]
[Route("[controller]")]
public class PlacemarkController : ControllerBase
{
    private readonly PlacemarkService _placemarkService;
    private readonly KMLImporter _importer;

    public PlacemarkController(PlacemarkService placemarkService, KMLImporter kMLImporter)
    {
        _placemarkService = placemarkService;
        _importer = kMLImporter;
    }

    [HttpGet]
    public async Task<DataResult<List<Placemark>>> Get() => new SuccessDataResult<List<Placemark>>(await _placemarkService.GetPlacemarks());

    [HttpPost]
    public async Task<Placemark> Upsert(Placemark placemarks)
    {
        return await _placemarkService.UpsertPlacemark(placemarks);
    }

    [HttpDelete]
    public async Task<bool> Delete(List<int> ids)
    {
        return await _placemarkService.DeletePlacemarks(ids);
    }

    [HttpGet("Import")]
    public bool Import()
    {
        try
        {
            _importer.ImportSeedFromJSON();
            return true;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
