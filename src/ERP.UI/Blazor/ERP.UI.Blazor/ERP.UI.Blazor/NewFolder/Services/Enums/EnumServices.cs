namespace PersonellInfo.Blazor.Components.Services.Enums;

public class EnumServices
{
    private readonly AppLanguageService languageService;
    public EnumServices(AppLanguageService languageService)
    {
        this.languageService = languageService;
    }
    public string GetLocalizedDisplayName(Type enumType, object value)
    {
        var key = $"Enum.{enumType.Name}.{value}";
        return languageService.Translate(key);
    }
}
