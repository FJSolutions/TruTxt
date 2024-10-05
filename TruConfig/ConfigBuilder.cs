using TruTxt;

namespace TruConfig;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

public class ConfigBuilder(IConfiguration configuration)
{
    private readonly IConfiguration _config = configuration;
    private static readonly Dictionary<string, string[]> KeyPaths = new();

    public ΤruΤxtResult<string> GetOptionalString(string key, [NotNull] string defaultValue)
    {
        var result = GetSections(_config, key);
        return ΤruΤxtResult<string>.Ok(result.Item1 ? result.Item2 : defaultValue);
    }

    public ΤruΤxtResult<string> GetString(string key)
    {
        var result = GetSections(_config, key);
        return result.Item1
            ? ΤruΤxtResult<string>.Ok(result.Item2)
            : ΤruΤxtResult<string>.Error($"The configuration key: '{key}', was not found in the configuration sources", "","");
    }

    public static ConfigBuilder Create(IConfiguration configuration)
    {
        return new ConfigBuilder(configuration);
    }

    private static Tuple<bool, string> GetSections(IConfiguration config, string key)
    {
        var path = SplitPath(key);

        switch (path.Length)
        {
            case 0:
                return Tuple.Create(false, string.Empty);
            case 1:
                var s = config.GetSection(path[0]);
                if(s.Exists() && !string.IsNullOrEmpty(s.Value))
                    return Tuple.Create(true, s.Value ?? string.Empty);
                return Tuple.Create(false, string.Empty);
        }

        var section = config.GetSection(path[0]);
        if (!section.Exists())
            return Tuple.Create(false, string.Empty);

        for (int i = 1; i < path.Length; i++)
        {
            section = section.GetSection(path[i]);
            if (!section.Exists())
                return Tuple.Create(false, string.Empty);
        }

        return Tuple.Create(true, section.Value ?? string.Empty);
    }

    private static String[] SplitPath(string key)
    {
        if (string.IsNullOrEmpty(key))
            return [];

        if (KeyPaths.TryGetValue(key, out var path))
            return path;

        var split = key.Split(':');
        KeyPaths.Add(key, split);
        return split;
    }
}