using System.Diagnostics.Contracts;

namespace TruConfig;

using TruTxt.Common;

using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.Configuration;

/// <summary>
/// A builder class for helping build strongly-typed configuration types from an <see cref="IConfiguration"/> instance 
/// </summary>
/// <param name="configuration">The <see cref="IConfiguration"/> instance to read values from</param>
public class ConfigBuilder(IConfiguration configuration)
{
   public Result<string> GetOptionalString(string key, [NotNull] string defaultValue)
   {
      var result = GetValue(configuration, key);
      return Result<string>.Ok(result.Item1 ? result.Item2 : defaultValue);
   }

   public Result<string> GetString(string key)
   {
      var result = GetValue(configuration, key);
      return result.Item1
         ? Result<string>.Ok(result.Item2)
         : Result<string>.Error($"The configuration key: '{key}', was not found in the configuration sources");
   }

   /// <summary>
   /// Smart constructor
   /// </summary>
   /// <param name="configuration"></param>
   /// <returns></returns>
   public static ConfigBuilder Create(IConfiguration configuration)
   {
      return new ConfigBuilder(configuration);
   }

   [Pure]
   internal static Tuple<bool, string> GetValue(IConfiguration config, string key)
   {
      var path = SplitPath(key);

      switch (path.Length)
      {
         case 0:
            return Tuple.Create(false, string.Empty);
         case 1:
            var s = config.GetSection(path[0]);
            if (s.Exists() && !string.IsNullOrEmpty(s.Value))
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

   [Pure]
   private static String[] SplitPath(string key)
   {
      if (string.IsNullOrEmpty(key))
         return [];

      return key.Split(':');
   }
}