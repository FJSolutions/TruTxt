namespace TruConfig;

using System.Diagnostics.Contracts;

public abstract record ConfigResult<T>
{
   internal ConfigResult()
   {
   }

   public static ConfigResult<T> Present(T value, string propertyName) => new Present<T>(value, propertyName);

   public static ConfigResult<T>
      Missing(string errorMessage, string typeName, string propertyName, string configPath) =>
      new Missing<T>([new ConfigError(errorMessage, typeName, propertyName, configPath)]);
};

public sealed record Present<T>(T Value, string PropertyName) : ConfigResult<T>;

public sealed record ConfigError(string ErrorMessage, string TypeName, string PropertyName, string ConfigPath);

public sealed record Missing<T>(ConfigError[] Errors) : ConfigResult<T>;

public static class ConfigResultExtensions
{
   public static TResult Match<T, TResult>(
      this ConfigResult<T> result,
      Func<Present<T>, TResult> onPresent,
      Func<ConfigError[], TResult> onMissing
   ) => result switch
   {
      Present<T> p => onPresent(p),
      Missing<T> a => onMissing(a.Errors),
      _ => throw new NotImplementedException("The Type of ConfigResult<T> is not known in the Match function!")
   };

   public static ConfigResult<TResult> Map<T, TResult>(
      this ConfigResult<T> result,
      Func<T, TResult> mapper
   ) => result switch
   {
      Present<T> p => new Present<TResult>(mapper(p.Value), p.PropertyName),
      Missing<T> m => new Missing<TResult>(m.Errors),
      _ => throw new NotImplementedException("The Type of ConfigResult<T> is not known in the Map function!")
   };

   public static ConfigResult<TResult> Bind<T, TResult>(
      this ConfigResult<T> result,
      Func<T, ConfigResult<TResult>> binder
   ) => result switch
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
      Func<T, TResult> mapper) => Map(result, mapper);

   [Pure]
   public static ConfigResult<TResult> SelectMany<T, TResult>(
      this ConfigResult<T> result,
      Func<T, ConfigResult<TResult>> binder) => Bind(result, binder);

   [Pure]
   public static ConfigResult<TResult> SelectMany<T, TIntermediate, TResult>(
      this ConfigResult<T> result,
      Func<T, ConfigResult<TIntermediate>> binder,
      Func<T, TIntermediate, TResult> combiner
   ) => result.Bind(a => binder(a).Map(b => combiner(a, b)));

   public static ConfigResult<T> Where<T>(this ConfigResult<T> result, Predicate<T> predicate)
      => result.Match(
         onPresent: v => predicate(v.Value) 
            ? ConfigResult<T>.Present(v.Value, v.PropertyName) 
            : ConfigResult<T>.Missing($"{v.Value} does not match the Where predicate", string.Empty, v.PropertyName, string.Empty),
         onMissing: e => new Missing<T>(e)
      );
}