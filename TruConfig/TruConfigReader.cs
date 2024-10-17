namespace TruConfig;

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

using Microsoft.Extensions.Configuration;

using TruTxt;
using TruTxt.Common;

using static TruTxt.Common.TruTxtParser;

/// <summary>
/// Represents a type that can read values safely from an <see cref="IConfiguration"/> instance and provide a means to
/// map them to objects or records; or, to return all the missing values at one time.
/// </summary>
/// <param name="config">The <see cref="IConfiguration"/> instance to read from</param>
/// <param name="sectionPath">The full section path (if any) to prepend to the property path</param>
/// <typeparam name="TModel">The type of the model whose properties will be used as key names for the Read methods</typeparam>
// ReSharper disable once InconsistentNaming
public class TruConfigReader<TModel>(IConfiguration config, string? sectionPath = null)
{
   private readonly string _modelTypeName = typeof(TModel).Name;

   /// <summary>
   /// Reads 
   /// </summary>
   /// <param name="expr"></param>
   /// <typeparam name="T"></typeparam>
   /// <returns></returns>
   public ResultCollector<TModel> Read<T>(Expression<Func<TModel, T>> expr) where T : IParsable<T>
   {
      var value = ReadValue(expr);

      var result = value.IsPresent
         ? ParseObject<T>(value.Value).Match(
            onSome: v => ConfigResult<object>.Present(v, value.PropertyName),
            onNone: () =>
               ConfigResult<object>.Missing($"'{value.Value}' cannot be parsed as a '{typeof(T).Name}'", _modelTypeName,
                  value.PropertyName, value.KeyPath)
         )
         : ConfigResult<object>.Missing($"No value could be found for the property: {value.PropertyName}",
            _modelTypeName, value.PropertyName, value.KeyPath);

      return new ResultCollector<TModel>([result]);
   }

   public ResultCollector<TModel> ReadOptional<T>(Expression<Func<TModel, T>> expr, [NotNull] T defaultValue) where T : IParsable<T>
   {
      var value = ReadValue(expr);

      var result = value.IsPresent
         ? ParseObject<T>(value.Value).Match(
            onSome: v => ConfigResult<object>.Present(v, value.PropertyName),
            onNone: () =>
               ConfigResult<object>.Missing($"'{value.Value}' cannot be parsed as a '{typeof(T).Name}'", _modelTypeName,
                  value.PropertyName, value.KeyPath)
         )
         : ConfigResult<object>.Present(defaultValue ?? new object(), value.PropertyName);

      return new ResultCollector<TModel>([result]);
   }

   public ResultCollector<TModel> AddModel<T>(Expression<Func<TModel, T>> expr, ConfigResult<T> modelResult)
   {
      var propertyName = expr.GetPropertyName();
      
      var result = modelResult.Match(
         onPresent: m => ConfigResult<object>.Present(m.Value ?? new object(), propertyName),
         onMissing: e => new Missing<object>(e)
      );

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
   private static String[] SplitPath(string key) =>
      string.IsNullOrEmpty(key) ? [] : key.Split(':');

   /// <summary>
   /// An internal record that collects all the information gathered in retrieving a string value safely from the
   /// <see cref="IConfiguration"/> instance. 
   /// </summary>
   /// <param name="IsPresent">Whether a value was present in the <see cref="IConfiguration"/> instance</param>
   /// <param name="Value">The <see cref="string"/> found; or an empty string</param>
   /// <param name="PropertyName">The name of the property from the supplied expression</param>
   /// <param name="KeyPath">The full config path</param>
   private record ConfigMetadata(bool IsPresent, string Value, string PropertyName, string KeyPath);
}