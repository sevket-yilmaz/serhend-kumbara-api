namespace SerhendKumbara.Data.Seed;

public class KMLJsonModel
{
    public string type { get; set; }
    public string name { get; set; }
    public Crs crs { get; set; }
    public Feature[] features { get; set; }
}

public class Crs
{
    public string type { get; set; }
    public Properties properties { get; set; }
}

public class Properties
{
    public string name { get; set; }
}

public class Feature
{
    public string type { get; set; }
    public Properties1 properties { get; set; }
    public Geometry geometry { get; set; }
}

public class Properties1
{
    public string Name { get; set; }
}

public class Geometry
{
    public string type { get; set; }
    public decimal[] coordinates { get; set; }
}
