namespace SerhendKumbara.Core.Caching;

public interface ICacheSettings
{
    string Prefix { get; set; }
    string MainKey { get; set; }
    int ExpiryTime { get; set; }
}