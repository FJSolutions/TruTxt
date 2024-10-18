using System.Reflection;

namespace TruConfig;

using System.Diagnostics.Contracts;

public abstract record ConfigResult<T>
{
   internal ConfigResult()
   {
   }

   public static ConfigResult<T> Present(T value) => new Present<T>(value);

   public static ConfigResult<T> Missing(string errorMessage) => new Missing<T>([errorMessage]);
};

public sealed record Present<T>(T Value) : ConfigResult<T>;

public sealed record ConfigError(string ErrorMessage);

public sealed record Missing<T>(string[] Errors) : ConfigResult<T>;

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
   
   public static ConfigResult<T> AsResult<T>(this Missing<T> missing) where T : notnull
   {
      return missing;
   }
   
   public static ConfigResult<T> AsResult<T>(this Present<T> present) where T : notnull
   {
      return present;
   }

   public static TResult Match<T, TResult>(
      this ConfigResult<T> result,
      Func<Present<T>, TResult> onPresent,
      Func<string[], TResult> onMissing
   ) => result switch
   {
      Present<T> p => onPresent(p),
      Missing<T> e => onMissing(e.Errors),
      _ => throw new NotImplementedException("The Type of ConfigResult<T> is not known in the Match function!")
   };

   public static ConfigResult<TResult> Map<T, TResult>(
      this ConfigResult<T> result,
      Func<T, TResult> mapper
   ) => result switch
   {
      Present<T> p => new Present<TResult>(mapper(p.Value)),
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
            ? ConfigResult<T>.Present(v.Value)
            : ConfigResult<T>.Missing($"{v.Value} does not match the Where predicate"),
         onMissing: e => new Missing<T>(e)
      );
}