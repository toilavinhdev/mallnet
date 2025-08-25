using BuildingBlock.Identity;
using BuildingBlock.MongoDB;
using BuildingBlock.Shared.Extensions;

namespace Service.Identity.AppSettings;

public class AppSettings : IAppSettings
{
    public IdentityOptions Identity { get; set; } = null!;
    
    public MongoOptions Mongo { get; set; } = null!;
}