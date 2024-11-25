using Microsoft.EntityFrameworkCore;

namespace SerhendKumbara.Data.Entity;

public class SerhendKumbaraDBContext : DbContext
{
    public SerhendKumbaraDBContext(DbContextOptions<SerhendKumbaraDBContext> options) : base(options) { }

    public DbSet<Placemark> Placemarks { get; set; }
    public DbSet<Region> Regions { get; set; }
}
