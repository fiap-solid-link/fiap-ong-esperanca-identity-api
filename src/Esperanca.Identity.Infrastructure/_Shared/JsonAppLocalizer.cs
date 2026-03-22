using System.Globalization;
using System.Text.Json;
using Esperanca.Identity.Application._Shared.Localization;

namespace Esperanca.Identity.Infrastructure._Shared;

public class JsonAppLocalizer : IAppLocalizer
{
    private readonly Dictionary<string, Dictionary<string, string>> _locales = new();
    private const string DefaultCulture = "pt-BR";

    public JsonAppLocalizer()
    {
        var assembly = typeof(IdentityErrorCodes).Assembly;
        var resourceNames = assembly.GetManifestResourceNames()
            .Where(n => n.EndsWith(".json", StringComparison.OrdinalIgnoreCase));

        foreach (var resourceName in resourceNames)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is null) continue;

            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            var doc = JsonSerializer.Deserialize<LocalizationFile>(json);

            if (doc?.Culture is not null && doc.Texts is not null)
                _locales[doc.Culture] = doc.Texts;
        }
    }

    public string this[string code]
    {
        get
        {
            var culture = CultureInfo.CurrentUICulture.Name;

            if (_locales.TryGetValue(culture, out var texts) && texts.TryGetValue(code, out var value))
                return value;

            if (_locales.TryGetValue(DefaultCulture, out var fallback) && fallback.TryGetValue(code, out var fallbackValue))
                return fallbackValue;

            return code;
        }
    }

    private sealed class LocalizationFile
    {
        public string? Culture { get; set; }
        public Dictionary<string, string>? Texts { get; set; }
    }
}
