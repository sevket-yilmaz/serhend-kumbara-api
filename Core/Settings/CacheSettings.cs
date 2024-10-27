﻿using SerhendKumbara.Core.Caching;

namespace SerhendKumbara.Core.Settings;

public class CacheSettings : ICacheSettings
{
    public string Prefix { get; set; }
    public string MainKey { get; set; }
    public int ExpiryTime { get; set; }
}