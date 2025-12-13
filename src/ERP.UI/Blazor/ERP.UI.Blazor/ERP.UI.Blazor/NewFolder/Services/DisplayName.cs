using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PersonellInfo.Blazor.Components.Services;

public interface IDisplayName
{
    string GetDisplayName(object model, string propertyName);
}

public class DisplayName : IDisplayName
{
    public string GetDisplayName(object model, string propertyName)
    {
        var prop = model.GetType().GetProperty(propertyName);
        var displayAttr = prop?.GetCustomAttribute<DisplayAttribute>();
        return displayAttr?.Name ?? propertyName;
    }
}
