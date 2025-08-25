using BuildingBlock.Identity;
using BuildingBlock.Shared.Extensions;

namespace Service.Catalog.AppSettings;

public class AppSettings : IAppSettings
{
    public IdentityOptions Identity { get; set; } = null!;
}