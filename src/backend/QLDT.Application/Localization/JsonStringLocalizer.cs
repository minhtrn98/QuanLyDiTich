using Microsoft.Extensions.Localization;
using QLDT.Application.Common.Services;
using System.Text;
using System.Text.Json;

namespace QLDT.Application.Localization;


public sealed record JsonStringLocalizer(IMemoryCacheService Cache) : IStringLocalizer
{
    private static ReadOnlySpan<byte> Utf8Bom => [0xEF, 0xBB, 0xBF];

    public LocalizedString this[string name]
    {
        get
        {
            string? value = GetString(name);
            return new LocalizedString(name, value ?? name, value == null);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            LocalizedString actualValue = this[name];
            return !actualValue.ResourceNotFound
                ? new LocalizedString(name, string.Format(actualValue.Value, arguments), false)
                : actualValue;
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        throw new NotSupportedException();
    }

    private string? GetString(string key)
    {
        string currentCultureName = Thread.CurrentThread.CurrentCulture.Name;
        string relativeFilePath = $"Resources\\{currentCultureName}.json";
#if DEBUG
        relativeFilePath = Directory.GetCurrentDirectory()[..^3] + $"Application\\Resources\\{currentCultureName}.json";
#endif
        string fullFilePath = Path.GetFullPath(relativeFilePath);
        if (File.Exists(fullFilePath))
        {
            string cacheKey = $"locale_{currentCultureName}_{key}";
            string? cacheValue = Cache.Get<string>(cacheKey);
            if (!string.IsNullOrEmpty(cacheValue))
            {
                return cacheValue;
            }

            string? result = GetValueFromJSON(key, Path.GetFullPath(relativeFilePath));
            if (!string.IsNullOrEmpty(result))
            {
                Cache.Set(cacheKey, result);
            }

            return result;
        }
        return null;
    }

    private static string? GetValueFromJSON(string propertyName, string filePath)
    {
        if (propertyName == null)
        {
            return null;
        }

        if (filePath == null)
        {
            return null;
        }

        // ReadAllBytes if the file encoding is UTF-8:
        ReadOnlySpan<byte> jsonReadOnlySpan = File.ReadAllBytes(filePath);

        // Read past the UTF-8 BOM bytes if a BOM exists.
        if (jsonReadOnlySpan.StartsWith(Utf8Bom))
        {
            jsonReadOnlySpan = jsonReadOnlySpan[Utf8Bom.Length..];
        }

        Utf8JsonReader reader = new(jsonReadOnlySpan);
        byte[][] propsName = propertyName
            .Split('.')
            .Select(Encoding.UTF8.GetBytes)
            .ToArray();
        int currentLevel = 0;
        int targetLevel = propsName.Length - 1;

        while (reader.Read())
        {
            // early return when token = close scope && current is target level
            if (currentLevel == targetLevel && reader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                continue;
            }
            if (!reader.ValueTextEquals(propsName[currentLevel]))
            {
                reader.Skip();
                continue;
            }
            if (currentLevel < targetLevel)
            {
                currentLevel++;
            }
            else if (currentLevel == targetLevel)
            {
                reader.Read();
                return reader.GetString();
            }
        }

        return null;
    }
}

