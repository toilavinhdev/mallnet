using Asp.Versioning;
using BuildingBlock.Identity;
using BuildingBlock.Shared.CQRS;
using BuildingBlock.Shared.Extensions;
using BuildingBlock.Shared.Localization;
using BuildingBlock.Shared.OpenApi;
using Service.Catalog.AppSettings;
using Service.Catalog.Resources;

var builder = WebApplication.CreateBuilder(args)
    .WithCoreEnvironment<AppSettings>(out var appSettings);

var services = builder.Services;
services.AddHttpContextAccessor();
services.AddCoreCors();
services.AddCoreOpenApi<Program>();
services.AddCoreLocalization();
services.AddCoreIdentity(appSettings.Identity);
services.AddCoreMediator<Program>();
services.AddLocalizationService<CatalogLocalizationResource>();

var app = builder.Build();
app.UseCoreExceptionHandler();
app.UseCors(CorsExtensions.AllowAll);
app.UseCoreLocalization();
app.UseCoreIdentity();
app.UseCoreOpenApi("/identity/api/v{apiVersion:apiVersion}", apiVersionSetBuilder =>
{
    apiVersionSetBuilder.HasApiVersion(new ApiVersion(1));
    apiVersionSetBuilder.HasApiVersion(new ApiVersion(2));
});
app.MapGet("/", () => "Service.Catalog").ExcludeFromDescription();
app.Run();