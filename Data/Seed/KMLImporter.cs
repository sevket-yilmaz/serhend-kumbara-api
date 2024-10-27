using SerhendKumbara.Data.Entity;

namespace SerhendKumbara.Data.Seed;

public class KMLImporter
{
    private readonly SerhendKumbaraDBContext _dBContext;

    public KMLImporter(SerhendKumbaraDBContext dBContext)
    {
        _dBContext = dBContext;
    }

    public void ImportSeedFromJSON()
    {
        List<Placemark> placemarks = new List<Placemark>();

        using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\Data\Seed\locations.geojson"))
        {
            string json = r.ReadToEnd();
            var kmlModel = JsonSerializer.Deserialize<KMLJsonModel>(json);
            foreach (var item in kmlModel.features)
            {
                placemarks.Add(new Placemark
                {
                    Name = item.properties.Name,
                    Latitude = item.geometry.coordinates[1],
                    Longitude = item.geometry.coordinates[0],
                    LastVisit = DateTime.Now,
                    Color = 0,
                    Status = PlacemarkStatus.Active,
                    VisitPeriod = 30
                });
            }

            _dBContext.Placemarks.AddRange(placemarks);
            _dBContext.SaveChanges();
        }
    }
}
