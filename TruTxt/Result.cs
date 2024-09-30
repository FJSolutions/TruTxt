namespace TruTxt;

using System.Diagnostics.Contracts;

/// <summary>
/// Represents the result of a <see cref="TruReader"/> read method. 
/// </summary>
/// <typeparam name="TValue"></typeparam>
[Pure]
public abstract record Result<TValue>
{
   private protected Result()
   {
   }

   public static Result<TValue> Ok(TValue value) => new Ok<TValue>(value);

   public static Result<TValue> Fail(string error, string key, string text) => new Fail<TValue>(error, key, text);

   /// <summary>
   /// Implicitly converts a value to a <see cref="Ok{A}"/>
   /// </summary>
   /// <param name="value">The value to put in a <see cref="Ok{A}"/></param>
   /// <returns>A <see cref="Result{A}"/></returns>
   public static implicit operator Result<TValue>(TValue value) => new Ok<TValue>(value);
}

/// <summary>
/// Represents the successful <see cref="Result{A}"/> type containing a value
/// </summary>
/// <param name="Value">The value the <see cref="Ok{A}"/> contains</param>
/// <typeparam name="TValue">The type of the <see cref="Result{A}"/>'s value</typeparam>
public sealed record Ok<TValue>(TValue Value) : Result<TValue>;

/// <summary>
/// Represents the failure <see cref="Result{A}"/>, containing an error message
/// </summary>
/// <param name="Error">The error message</param>
public sealed record Fail<TValue>(string Error, string Key, string Text) : Result<TValue>;

/// <summary>
/// LINQ extensions for the <see cref="Result{A}"/> type 
/// </summary>
public static class ResultExtensions
{
   /// <summary>
   /// Matches this <see cref="Result{A}"/> against the two supplied functions. 
   /// </summary>
   /// <param name="result"></param>
   /// <param name="ok">The function to execute if the <see cref="Result{A}"/> is an <see cref="Ok{T}"/></param>
   /// <param name="fail">The function to execute if the <see cref="Result{A}"/> is a <see cref="Fail{TValue}"/></param>
   /// <typeparam name="TResult">The return type of the method</typeparam>
   /// <typeparam name="TValue"></typeparam>
   /// <returns>An instance of <typeparam name="TResult"></typeparam></returns>
   [Pure]
   public static TResult Match<TValue, TResult>(this Result<TValue> result, Func<TValue, TResult> ok,
      Func<string, string, string, TResult> fail)
   {
      return result switch
      {
         Ok<TValue>(var v) => ok(v),
         Fail<TValue>(var message, var key, var text) => fail(message, key, text),
         _ => throw new TruTxtException("Unknown Result type")
      };
   }

   /// <summary>
   /// Maps the value of this <see cref="Result{A}"/> if it is an <see cref="Ok{T}"/> 
   /// </summary>
   /// <param name="result"></param>
   /// <param name="mapper">The mapper function</param>
   /// <typeparam name="TResult">The return type of the mapping</typeparam>
   /// <typeparam name="TValue"></typeparam>
   /// <returns>A <see cref="Result{B}"/></returns>
   [Pure]
   public static Result<TResult> Map<TValue, TResult>(this Result<TValue> result, Func<TValue, TResult> mapper)
   {
      return result switch
      {
         Ok<TValue> valid => mapper(valid.Value),
         Fail<TValue> failure => Result<TResult>.Fail(failure.Error, failure.Key, failure.Text),
         _ => throw new TruTxtException("Unknown result type!")
      };
   }

   /// <summary>
   /// Performs a function on the <see cref="Result{A}"/> if it is a <see cref="Ok{T}"/> which can return a <see cref="Fail{TValue}"/> 
   /// </summary>
   /// <param name="result"></param>
   /// <param name="binder">The binding function</param>
   /// <typeparam name="TResult">The return type for the <see cref="Result{B}"/></typeparam>
   /// <typeparam name="TValue"></typeparam>
   /// <returns>A <see cref="Result{B}"/></returns>
   [Pure]
   public static Result<TResult> Bind<TValue, TResult>(this Result<TValue> result,
      Func<TValue, Result<TResult>> binder)
   {
      return result switch
      {
         Ok<TValue> valid => binder(valid.Value),
         Fail<TValue> failure => Result<TResult>.Fail(failure.Error, failure.Key, failure.Text),
         _ => throw new TruTxtException("Unknown result type!")
      };
   }

   /******************************************
    *
    * LINQ Methods
    *
    *****************************************/

   [Pure]
   public static Result<TResult> Select<TValue, TResult>(this Result<TValue> result, Func<TValue, TResult> mapper) =>
      result.Map(mapper);

   [Pure]
   public static Result<TResult> SelectMany<TValue, TResult>(this Result<TValue> result,
      Func<TValue, Result<TResult>> binder) => result.Bind(binder);

   [Pure]
   public static Result<TResult> SelectMany<TValue, TIntermediate, TResult>
   (this Result<TValue> result, Func<TValue, Result<TIntermediate>> binder,
      Func<TValue, TIntermediate, TResult> combiner) =>
      result.Bind(a => binder(a).Map(b => combiner(a, b)));

   [Pure]
   public static Result<TValue> Where<TValue>(this Result<TValue> result, Predicate<TValue> predicate) =>
      result.Match(ok: v => predicate(v), fail: (_, _, _) => false)
         ? result
         : Result<TValue>.Fail("Unmatched predicate in where clause", string.Empty, string.Empty);
}