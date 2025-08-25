using Microsoft.Extensions.Localization;

namespace BuildingBlock.Shared.Localization.Abstractions;

public interface ILocalizationService<TResource> where TResource : ILocalizationResource
{
    string ResourceName { get; }
    
    IEnumerable<LocalizedString> List();
    
    string this[string name] { get; }
}

public sealed class LocalizationService<TResource>(IStringLocalizer<TResource> localizer)
    : ILocalizationService<TResource> where TResource : ILocalizationResource
{
    public string ResourceName => typeof(TResource).Name;
    
    public IEnumerable<LocalizedString> List() => localizer.GetAllStrings();
    
    public string this[string name] => localizer[name];
}