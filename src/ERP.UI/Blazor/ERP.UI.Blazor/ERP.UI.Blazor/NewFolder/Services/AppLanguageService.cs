using Newtonsoft.Json;

public class AppLanguageService
{
    private static readonly HashSet<string> RtlLanguages = new(StringComparer.OrdinalIgnoreCase)
    {
        "Fa"
    };

    public string Language { get; private set; } = "Fa";
    public string Direction { get; private set; } = "rtl";
    public event Action? LanguageChanged;

    private Dictionary<string, string> _translations = new();

    public string Translate(string key)
    {
        return _translations.TryGetValue(key, out var value) ? value : key;
    }

    public void SetLanguage(string language)
    {
        if (!string.Equals(Language, language, StringComparison.OrdinalIgnoreCase))
        {
            Language = language;
            Direction = RtlLanguages.Contains(Language) ? "rtl" : "ltr";
            LoadTranslations();
            LanguageChanged?.Invoke();
        }
    }

    // Call this from constructor and SetLanguage
    private void LoadTranslations()
    {
        var path = $"./wwwroot/Locales/{Language.ToLower()}.json";
        if (System.IO.File.Exists(path))
        {
            var json = System.IO.File.ReadAllText(path);
            _translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new();
        }
        else
        {
            _translations = new();
        }
    }

    public AppLanguageService()
    {
        LoadTranslations();
    }
}
