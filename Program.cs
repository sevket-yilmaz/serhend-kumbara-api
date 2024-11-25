using Microsoft.EntityFrameworkCore;
using SerhendKumbara.Core.Caching;
using SerhendKumbara.Core.Settings;
using SerhendKumbara.Data.Entity;
using SerhendKumbara.Data.Seed;
using SerhendKumbara.Services;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    builder.WebHost.UseUrls("https://localhost:7061", "https://odin:7061", "https://192.168.1.6:7061");
}
builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

//Cache Settings
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));
builder.Services.AddSingleton<ICacheSettings>(sp => sp.GetRequiredService<IOptions<CacheSettings>>().Value);
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<ICacheService, MemoryCacheService>();

//Json Settings
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
    options.SerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
});

//Database
builder.Services.AddDbContext<SerhendKumbaraDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("KumbaraDB")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // Timespan sıkıntı yapıyor

//Service Dependencies
builder.Services.AddTransient<KMLImporter, KMLImporter>();
builder.Services.AddTransient<PlacemarkService, PlacemarkService>();
builder.Services.AddTransient<RegionService, RegionService>();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseSentry();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
