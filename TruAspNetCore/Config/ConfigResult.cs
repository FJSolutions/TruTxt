namespace TruAspNetCore.Config;

using System.Diagnostics.Contracts;
using System.Text;

public abstract record ConfigResult<T> where T : notnull
{
   internal ConfigResult()
   {
   }

   public static ConfigResult<T> Present(T value) => new Present<T>(value);

   public static ConfigResult<T>
      Missing(string errorMessage, string typeName, string propertyName, string configPath) =>
      new Missing<T>([new MissingInfo(errorMessage, typeName, propertyName, configPath)]);
};

public sealed record Present<T>(T Value) : ConfigResult<T> where T : notnull;

public sealed record Missing<T>(MissingInfo[] Errors) : ConfigResult<T> where T : notnull;

public sealed record MissingInfo(string ErrorMessage, string TypeName, string PropertyName, string ConfigPath);

public static class ConfigResultExtensions
{
   public static Present<T> AsPresent<T>(this ConfigResult<T> result) where T : notnull
   {
      if (result is Present<T> present)
         return present;

      throw new InvalidCastException($"{typeof(T).Name} is not a Present result.");
   }

   public static Missing<T> AsMissing<T>(this ConfigResult<T> result) where T : notnull
   {
      if (result is Missing<T> missing)
         return missing;

      throw new InvalidCastException($"{typeof(T).Name} is not a Missing result.");
   }

   public static string ToHierarchicalMessage<T>(this Missing<T> missing, int indent = 2) where T : notnull
   {
      var sb = new StringBuilder("Missing Configuration:\n");
      StringBuilder WriteIndented(string msg, int depth) => sb.Append(new string(' ', indent * depth)).AppendLine(msg);

      foreach (var groupByType in missing.Errors.GroupBy(m => m.TypeName))
      {
         WriteIndented($"Type: {groupByType.Key}", 1);

         foreach (var groupByProperty in groupByType.GroupBy(t => t.PropertyName))
         {
            WriteIndented($"Property: {groupByProperty.Key}", 2);

            foreach (var info in groupByProperty)
            {
               WriteIndented(info.ErrorMessage, 3);
               WriteIndented($"ConfigPath: {info.ConfigPath}", 4);
            }
         }
      }


      return sb.ToString();
   }

   public static TResult Match<T, TResult>(
      this ConfigResult<T> result,
      Func<Present<T>, TResult> onPresent,
      Func<Missing<T>, TResult> onMissing
   ) where T : notnull => result switch
   {
      Present<T> p => onPresent(p),
      Missing<T> missing => onMissing(missing),
      _ => throw new NotImplementedException("The Type of ConfigResult<T> is not known in the Match function!")
   };

   public static ConfigResult<TResult> Map<T, TResult>(
      this ConfigResult<T> result,
      Func<T, TResult> mapper
   ) where T : notnull => result switch
   {
      Present<T> p => new Present<TResult>(mapper(p.Value)),
      Missing<T> m => new Missing<TResult>(m.Errors),
      _ => throw new NotImplementedException("The Type of ConfigResult<T> is not known in the Map function!")
   };

   public static ConfigResult<TResult> Bind<T, TResult>(
      this ConfigResult<T> result,
      Func<T, ConfigResult<TResult>> binder
   ) where T : notnull => result switch
   {
      Present<T> p => binder(p.Value),
      Missing<T> m => new Missing<TResult>(m.Errors),
      _ => throw new NotImplementedException("The Type of ConfigResult<T> is not known in the Bind function!")
   };

   /*******************************
    *
    * Linq Methods
    *
    *******************************/

   [Pure]
   public static ConfigResult<TResult> Select<T, TResult>(
      this ConfigResult<T> result,
      Func<T, TResult> mapper) where T : notnull => Map(result, mapper);

   [Pure]
   public static ConfigResult<TResult> SelectMany<T, TResult>(
      this ConfigResult<T> result,
      Func<T, ConfigResult<TResult>> binder) where T : notnull => Bind(result, binder);

   [Pure]
   public static ConfigResult<TResult> SelectMany<T, TIntermediate, TResult>(
      this ConfigResult<T> result,
      Func<T, ConfigResult<TIntermediate>> binder,
      Func<T, TIntermediate, TResult> combiner
   ) where T : notnull => result.Bind(a => binder(a).Map(b => combiner(a, b)));

   public static ConfigResult<T> Where<T>(this ConfigResult<T> result, Predicate<T> predicate) where T : notnull
      => result.Match(
         onPresent: v => predicate(v.Value)
            ? ConfigResult<T>.Present(v.Value)
            : ConfigResult<T>.Missing($"{v.Value} does not match the Where predicate", typeof(T).Name, string.Empty,
               string.Empty),
         onMissing: e => new Missing<T>(e.Errors)
      );
}