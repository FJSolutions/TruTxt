using System.Diagnostics.CodeAnalysis;

using TruTxt.Common;

namespace TruConfig;

using System.Diagnostics.Contracts;

using TruTxt;

using System.Linq.Expressions;

using Microsoft.Extensions.Configuration;

using static TruTxtParser;

// ReSharper disable once InconsistentNaming
public class TruConfigReader<TModel>(IConfiguration config, string? sectionPath = null)
   where TModel : new()
{
   private readonly string _modelTypeName = typeof(TModel).Name;

   public ResultCollector<TModel> Read<T>(Expression<Func<TModel, T>> expr)
   {
      var value = ReadValue(expr);

      var result = value.IsPresent
         ? Parse<T>(value.Value).Match(
            onSome: v => ConfigResult<object>.Present(v, value.PropertyName),
            onNone: () =>
               ConfigResult<object>.Missing($"'{value.Value}' cannot be parsed as a '{typeof(T).Name}'", _modelTypeName,
                  value.PropertyName, value.KeyPath)
         )
         : ConfigResult<object>.Missing($"No value could be found for the property: {value.PropertyName}",
            _modelTypeName, value.PropertyName, value.KeyPath);

      return new ResultCollector<TModel>([result]);
   }

   public ResultCollector<TModel> ReadOptional<T>(Expression<Func<TModel, T>> expr, [NotNull] T defaultValue)
   {
      var value = ReadValue(expr);

      var result = value.IsPresent
         ? Parse<T>(value.Value).Match(
            onSome: v => ConfigResult<object>.Present(v, value.PropertyName),
            onNone: () =>
               ConfigResult<object>.Missing($"'{value.Value}' cannot be parsed as a '{typeof(T).Name}'", _modelTypeName,
                  value.PropertyName, value.KeyPath)
         )
         : ConfigResult<object>.Present(defaultValue ?? new object(), value.PropertyName);

      return new ResultCollector<TModel>([result]);
   }

   private ConfigMetadata ReadValue<T>(Expression<Func<TModel, T>> expr)
   {
      var fullKeyPath = expr.GetPropertyName();
      var propertyName = fullKeyPath;
      if (sectionPath != null)
         fullKeyPath = $"{sectionPath}:{fullKeyPath}";

      var path = SplitPath(fullKeyPath);

      switch (path.Length)
      {
         case 0:
            return new ConfigMetadata(false, string.Empty, propertyName, fullKeyPath);
         case 1:
            var s = config.GetSection(path[0]);
            if (s.Exists() && !string.IsNullOrEmpty(s.Value))
               return new ConfigMetadata(true, s.Value ?? string.Empty, propertyName, fullKeyPath);
            return new ConfigMetadata(false, string.Empty, propertyName, fullKeyPath);
      }

      var section = config.GetSection(path[0]);
      if (!section.Exists())
         return new ConfigMetadata(false, string.Empty, propertyName, fullKeyPath);

      for (int i = 1; i < path.Length; i++)
      {
         section = section.GetSection(path[i]);
         if (!section.Exists())
            return new ConfigMetadata(false, string.Empty, propertyName, fullKeyPath);
      }

      return new ConfigMetadata(true, section.Value ?? string.Empty, propertyName, fullKeyPath);
   }

   [Pure]
   private static String[] SplitPath(string key)
   {
      if (string.IsNullOrEmpty(key))
         return [];

      return key.Split(':');
   }
}

internal record ConfigMetadata(bool IsPresent, string Value, string PropertyName, string KeyPath);