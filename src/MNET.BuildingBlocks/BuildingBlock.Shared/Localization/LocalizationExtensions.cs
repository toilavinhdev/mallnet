using System.Globalization;
using BuildingBlock.Shared.Localization.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Shared.Localization;

public static class LocalizationExtensions
{
    private static readonly List<CultureInfo> SupportedCultures =
    [
        new("vi-VN"),
        new("en-US"),
    ];
    
    public static void AddCoreLocalization(this IServiceCollection services)
    {
        services.AddLocalization(o =>
        {
            o.ResourcesPath = "Resources";
        });
    }
    
    public static void AddLocalizationService<TResource>(this IServiceCollection services) where TResource : ILocalizationResource
    {
        services.AddSingleton<ILocalizationService<TResource>, LocalizationService<TResource>>();
    }
    
    public static void UseCoreLocalization(this WebApplication app)
    {
        app.UseRequestLocalization(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(SupportedCultures[0]);
            options.SupportedCultures = SupportedCultures;
            options.SupportedUICultures = SupportedCultures;
            options.RequestCultureProviders =
            [
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider(),
                new AcceptLanguageHeaderRequestCultureProvider()
            ];
            options.ApplyCurrentCultureToResponseHeaders = true;
        });
    }
}