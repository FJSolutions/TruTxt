using System.Diagnostics.Contracts;

namespace TruTxt.Common;

public abstract record Result<T>
{
   internal Result()
   {
   }

   public static Result<T> Ok(T value) => new Ok<T>(value);

   public static Result<T> Error(string message) => new Error<T>(message);
}

public sealed record Ok<T>(T Value) : Result<T>;

public sealed record Error<T>(string ErrorMessage) : Result<T>;

public static class ResultExtensions
{
   public static TReturn Match<TValue, TReturn>(
      this Result<TValue> result,
      Func<TValue, TReturn> ok,
      Func<string, TReturn> err
   )
   {
      return result switch
      {
         Ok<TValue> v => ok(v.Value),
         Error<TValue> e => err(e.ErrorMessage),
         _ => throw new NotImplementedException("Unknown Result type in Match!")
      };
   }

   /*******************************
    *
    * Functional Methods
    *
    *******************************/

   public static Result<TReturn> Map<TValue, TReturn>(
      this Result<TValue> result,
      Func<TValue, TReturn> mapper
   )
   {
      return result switch
      {
         Ok<TValue> v => Result<TReturn>.Ok(mapper(v.Value)),
         Error<TValue> e => Result<TReturn>.Error(e.ErrorMessage),
         _ => throw new NotImplementedException("Unknown Result type in Map!")
      };
   }

   public static Result<TReturn> Bind<TValue, TReturn>(
      this Result<TValue> result,
      Func<TValue, Result<TReturn>> binder
   )
   {
      return result switch
      {
         Ok<TValue> v => binder(v.Value),
         Error<TValue> e => Result<TReturn>.Error(e.ErrorMessage),
         _ => throw new NotImplementedException("Unknown Result type in Bind!")
      };
   }

   /*******************************
    *
    * Linq Methods
    *
    *******************************/

   [Pure]
   public static Result<TResult> Select<TValue, TResult>(this Result<TValue> result,
      Func<TValue, TResult> mapper) => Map(result, mapper);

   [Pure]
   public static Result<TResult> SelectMany<TValue, TResult>(this Result<TValue> result,
      Func<TValue, Result<TResult>> binder) => Bind(result, binder);

   [Pure]
   public static Result<TResult> SelectMany<TValue, TIntermediate, TResult>(
      this Result<TValue> result,
      Func<TValue, Result<TIntermediate>> binder,
      Func<TValue, TIntermediate, TResult> combiner) =>
      result.Bind(a => binder(a).Map(b => combiner(a, b)));

   [Pure]
   public static Result<TValue> Where<TValue>(
      this Result<TValue> result,
      Predicate<TValue> predicate
   ) => Match(result, ok: v => predicate(v), err: _ => false)
      ? result
      : Result<TValue>.Error("Unmatched predicate in where clause");
}