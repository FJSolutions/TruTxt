namespace TruAspNetCore.Config;

using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;

using Microsoft.Extensions.Configuration;

using TruTxt;
using TruTxt.Common;

/// <summary>
/// Represents a type that can read values safely from an <see cref="IConfiguration"/> instance and provide a means to
/// map them to objects or records; or, to return all the missing values at one time.
/// </summary>
/// <param name="config">The <see cref="IConfiguration"/> instance to read from</param>
public class TruConfigReader(IConfiguration config)
{
   private readonly List<ConfigMetadata> _metadata = [];

   public Reader<T> CreateReader<T>(string? sectionName = null)
   {
      // Reflect all the public properties of T
      var t = typeof(T);
      var typeName = t.Name;
      foreach (var property in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
      {
         // This is to skip complex classes and records that need to have their own readers.
         if (!property.PropertyType.IsValueType && property.PropertyType != typeof(string))
            continue;

         var path = sectionName == null ? property.Name : $"{sectionName}:{property.Name}";
         var (isPresent, stringValue) = ReadValue(path, config);
         if (isPresent)
         {
            var pt = property.PropertyType;
            var objValue =
               TruTxtParser.ParseObject(stringValue, pt)
                  .Match(
                     onSome: ConfigResult<object>.Present,
                     onNone: () => ConfigResult<object>.Missing($"{stringValue} could not be parsed as a {pt}",
                        typeName, property.Name, path)
                  );
            _metadata.Add(new ConfigMetadata(true, objValue, typeName, property.Name, path));
         }
         else
            _metadata.Add(new ConfigMetadata(false,
               ConfigResult<object>.Missing(
                  $"No value could be found for '{property.Name}' of '{typeName}' at path: {path}", typeName,
                  property.Name, path),
               typeName, property.Name, path)
            );
      }

      // Return a reader function that gets values for this T and returns them as a ConfigResult
      return new Reader<T>(_metadata, typeName);
   }

   public ConnectionStringsCollector ReadConnectionStrings()
   {
      var list = ImmutableList.Create<ConfigConnectionInfo>().ToBuilder();

      var section = config.GetSection("ConnectionStrings");
      if (section.Exists())
      {
         foreach (var conStr in section.GetChildren())
            list.Add(new ConfigConnectionInfo(conStr.Key, conStr.Value ?? string.Empty));
      }

      return new ConnectionStringsCollector(list.ToImmutable());
   }

   [Pure]
   private static Tuple<bool, string> ReadValue(string fullKeyPath, IConfiguration config)
   {
      var path = SplitPath(fullKeyPath);

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
   private static String[] SplitPath(string key) =>
      string.IsNullOrEmpty(key) ? [] : key.Split(':');

   /// <summary>
   /// An internal record that collects all the information gathered in retrieving a string value safely from the
   /// <see cref="IConfiguration"/> instance. 
   /// </summary>
   /// <param name="TypeName">The name of the type that this _metadata is for</param>
   /// <param name="PropertyName">The name of the property from the supplied expression</param>
   internal record ConfigMetadata(
      bool IsPresent,
      ConfigResult<object> ParsedValue,
      string TypeName,
      string PropertyName,
      string ConfigPath);


   public class Reader<T>
   {
      private readonly List<ConfigMetadata> _metadata;
      private readonly string _typeName;

      internal Reader(List<ConfigMetadata> metadata, string typeName)
      {
         this._metadata = metadata;
         this._typeName = typeName;
      }

      public ConfigResult<A> GetValue<A>(Expression<Func<T, A>> expr)
      {
         var propertyName = expr.GetPropertyName();
         var meta = this._metadata.First(m => m.TypeName == _typeName && m.PropertyName == propertyName);
         var result = meta.ParsedValue.Bind(v => ConfigResult<A>.Present((A)Convert.ChangeType(v, typeof(A))));

         return result.Match(
            onPresent: p => p,
            onMissing: _ => AllErrors<A>()
         );
      }

      public ConfigResult<A> GetOptionalValue<A>(Expression<Func<T, A>> expr, A defaultValue)
      {
         var propertyName = expr.GetPropertyName();
         var meta = this._metadata.First(m => m.TypeName == _typeName && m.PropertyName == propertyName);
         var result = meta.ParsedValue.Bind(v => ConfigResult<A>.Present((A)Convert.ChangeType(v, typeof(A))));

         return result.Match(
            onPresent: p => p,
            onMissing: _ => new Present<A>(defaultValue)
         );
      }

      public ConfigResult<A> GetModel<A>(ConfigResult<A> model)
      {
         return model.Match(
            onPresent: p => p,
            onMissing: _ => AllErrors<A>()
         );
      }

      private ConfigResult<A> AllErrors<A>() =>
         this._metadata.Where(m => !m.IsPresent)
            .Select(m => m.ParsedValue.AsMissing())
            .Aggregate((m1, m2) => new Missing<object>(m1.Errors.Concat(m2.Errors).ToArray()))
            .SelectMany(r =>
            {
               var m = r as Missing<object>;
               return new Missing<A>(m!.Errors);
            });
   }

   public record ConfigConnectionInfo(string Name, string ConnectionString);
}

public record ConnectionStringsCollector(
   ImmutableList<TruConfigReader.ConfigConnectionInfo> ConfigConnectionInformation);

public static class ConnectionConfigExtensions
{
   public static bool IsEmpty(this ConnectionStringsCollector info) => info.ConfigConnectionInformation.IsEmpty;

   public static string Get(this ConnectionStringsCollector connectionStrings, string name) =>
      connectionStrings.ConfigConnectionInformation.FirstOrDefault(cs => cs.Name == name)?.ConnectionString ??
      string.Empty;
}