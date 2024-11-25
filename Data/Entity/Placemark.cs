using System.ComponentModel.DataAnnotations.Schema;

namespace SerhendKumbara.Data.Entity;

public class Placemark
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PlacemarkID { get; set; }
    [Column(TypeName = "varchar")]
    public string Name { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public PlacemarkStatus Status { get; set; }
    public DateTime LastVisit { get; set; } = DateTime.MinValue;
    public int VisitPeriod { get; set; }

    public int? RegionID { get; set; }
    public Region? Region { get; set; }
}

public enum PlacemarkStatus
{
    Active = 1, Passive = 2
}
