namespace TruConfig;

using System.Diagnostics.Contracts;

public abstract record ConfigResult
{
   internal ConfigResult()
   {
   }

   public static ConfigResult Present(string value) => new Present([value]);

   public static ConfigResult Missing(string errorMessage) => new Absent([errorMessage]);
};

public sealed record Present(string[] Values) : ConfigResult;

public sealed record Absent(string[] ErrorMessages) : ConfigResult;

public static class ConfigResultExtensions
{
   public static TResult Match<TResult>(
      this ConfigResult result,
      Func<string[], TResult> present,
      Func<string[], TResult> absent
   ) => result switch
   {
      Present p => present(p.Values),
      Absent a => absent(a.ErrorMessages),
      _ => throw new NotImplementedException("The Type of ConfigResult is not known in the Match function!")
   };

   public static ConfigResult Combine(
      this ConfigResult result,
      ConfigResult otherResult
   ) => (result, otherResult) switch
   {
      (Present p1, Present p2) => new Present(p1.Values.Concat(p2.Values).ToArray()),
      (Present _, Absent a) => a,
      (Absent a, Present _) => a,
      (Absent a1, Absent a2) => new Absent(a1.ErrorMessages.Concat(a2.ErrorMessages).ToArray()),
      _ => throw new NotImplementedException("The Type of ConfigResult is not known in the Match function!")
   };

   public static ConfigResult Map(
      this ConfigResult result,
      Func<string[], string[]> mapper
   ) => result switch
   {
      Present p => new Present(mapper(p.Values)),
      Absent a => a,
      _ => throw new NotImplementedException("The Type of ConfigResult is not known in the Map function!")
   };

   public static ConfigResult Bind(
      this ConfigResult result,
      Func<string[], ConfigResult> binder
   ) => result switch
   {
      Present p => binder(p.Values),
      Absent a => a,
      _ => throw new NotImplementedException("The Type of ConfigResult is not known in the Bind function!")
   };

   /*******************************
    *
    * Linq Methods
    *
    *******************************/

   [Pure]
   public static ConfigResult Select(this ConfigResult result,
      Func<string[], string[]> mapper) => Map(result, mapper);

   [Pure]
   public static ConfigResult SelectMany<TValue, TResult>(this ConfigResult result,
      Func<string[], ConfigResult> binder) => Bind(result, binder);

   [Pure]
   public static ConfigResult SelectMany(
      this ConfigResult result,
      Func<string[], ConfigResult> binder) =>
      result.Bind(a => binder(a).Map(b => a.Concat(b).ToArray()));
   
   // [Pure]
   // public static ConfigResult SelectMany(
   //    this ConfigResult result,
   //    Func<string, ConfigResult> binder,
   //    Func<string, string, string[]> combiner) =>
   //    result.Bind(a => binder(a).Map(b => combiner(a, b)));
}