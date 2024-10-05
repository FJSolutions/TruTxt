using System.Diagnostics.Contracts;

namespace TruTxt;

/// <summary>
/// Represents the result of a <see cref="TruReader"/> read method. 
/// </summary>
/// <typeparam name="TValue"></typeparam>
[Pure]
public abstract record ΤruΤxtResult<TValue>
{
   private protected ΤruΤxtResult()
   {
   }

   public static ΤruΤxtResult<TValue> Ok(TValue value) => new Ok<TValue>(value);

   public static ΤruΤxtResult<TValue> Error(string error, string key, string text) => new Error<TValue>(error, key, text);

   /// <summary>
   /// Implicitly converts a value to a <see cref="Ok{A}"/>
   /// </summary>
   /// <param name="value">The value to put in a <see cref="Ok{A}"/></param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/></returns>
   public static implicit operator ΤruΤxtResult<TValue>(TValue value) => new Ok<TValue>(value);
}

/// <summary>
/// Represents the successful <see cref="ΤruΤxtResult{TValue}"/> type containing a value
/// </summary>
/// <param name="Value">The value the <see cref="Ok{A}"/> contains</param>
/// <typeparam name="TValue">The type of the <see cref="ΤruΤxtResult{TValue}"/>'s value</typeparam>
public sealed record Ok<TValue>(TValue Value) : ΤruΤxtResult<TValue>;

/// <summary>
/// Represents the failure <see cref="ΤruΤxtResult{TValue}"/>, containing an error message
/// </summary>
/// <param name="ErrorMessage">The error message</param>
public sealed record Error<TValue>(string ErrorMessage, string Key, string Text) : ΤruΤxtResult<TValue>;

/// <summary>
/// LINQ extensions for the <see cref="ΤruΤxtResult{TValue}"/> type 
/// </summary>
public static class ResultExtensions
{
   /// <summary>
   /// Matches this <see cref="ΤruΤxtResult{TValue}"/> against the two supplied functions. 
   /// </summary>
   /// <param name="τruΤxtResult"></param>
   /// <param name="ok">The function to execute if the <see cref="ΤruΤxtResult{TValue}"/> is an <see cref="Ok{T}"/></param>
   /// <param name="fail">The function to execute if the <see cref="ΤruΤxtResult{TValue}"/> is a <see cref="Error{TValue}"/></param>
   /// <typeparam name="TResult">The return type of the method</typeparam>
   /// <typeparam name="TValue"></typeparam>
   /// <returns>An instance of <typeparam name="TResult"></typeparam></returns>
   [Pure]
   public static TResult Match<TValue, TResult>(this ΤruΤxtResult<TValue> τruΤxtResult, Func<TValue, TResult> ok,
      Func<string, string, string, TResult> fail)
   {
      return τruΤxtResult switch
      {
         Ok<TValue>(var v) => ok(v),
         Error<TValue>(var message, var key, var text) => fail(message, key, text),
         _ => throw new TruTxtException("Unknown ΤruΤxtResult type")
      };
   }

   /// <summary>
   /// Maps the value of this <see cref="ΤruΤxtResult{TValue}"/> if it is an <see cref="Ok{T}"/> 
   /// </summary>
   /// <param name="τruΤxtResult"></param>
   /// <param name="mapper">The mapper function</param>
   /// <typeparam name="TResult">The return type of the mapping</typeparam>
   /// <typeparam name="TValue"></typeparam>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/></returns>
   [Pure]
   public static ΤruΤxtResult<TResult> Map<TValue, TResult>(this ΤruΤxtResult<TValue> τruΤxtResult, Func<TValue, TResult> mapper)
   {
      return τruΤxtResult switch
      {
         Ok<TValue> valid => mapper(valid.Value),
         Error<TValue> failure => ΤruΤxtResult<TResult>.Error(failure.ErrorMessage, failure.Key, failure.Text),
         _ => throw new TruTxtException("Unknown τruΤxtResult type!")
      };
   }

   /// <summary>
   /// Performs a function on the <see cref="ΤruΤxtResult{TValue}"/> if it is a <see cref="Ok{T}"/> which can return a <see cref="Error"/> 
   /// </summary>
   /// <param name="τruΤxtResult"></param>
   /// <param name="binder">The binding function</param>
   /// <typeparam name="TResult">The return type for the <see cref="ΤruΤxtResult{TValue}"/></typeparam>
   /// <typeparam name="TValue"></typeparam>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/></returns>
   [Pure]
   public static ΤruΤxtResult<TResult> Bind<TValue, TResult>(this ΤruΤxtResult<TValue> τruΤxtResult,
      Func<TValue, ΤruΤxtResult<TResult>> binder)
   {
      return τruΤxtResult switch
      {
         Ok<TValue> valid => binder(valid.Value),
         Error<TValue> failure => ΤruΤxtResult<TResult>.Error(failure.ErrorMessage, failure.Key, failure.Text),
         _ => throw new TruTxtException("Unknown τruΤxtResult type!")
      };
   }

   /******************************************
    *
    * LINQ Methods
    *
    *****************************************/

   [Pure]
   public static ΤruΤxtResult<TResult> Select<TValue, TResult>(this ΤruΤxtResult<TValue> τruΤxtResult, Func<TValue, TResult> mapper) =>
      τruΤxtResult.Map(mapper);

   [Pure]
   public static ΤruΤxtResult<TResult> SelectMany<TValue, TResult>(this ΤruΤxtResult<TValue> τruΤxtResult,
      Func<TValue, ΤruΤxtResult<TResult>> binder) => τruΤxtResult.Bind(binder);

   [Pure]
   public static ΤruΤxtResult<TResult> SelectMany<TValue, TIntermediate, TResult>
   (this ΤruΤxtResult<TValue> τruΤxtResult, Func<TValue, ΤruΤxtResult<TIntermediate>> binder,
      Func<TValue, TIntermediate, TResult> combiner) =>
      τruΤxtResult.Bind(a => binder(a).Map(b => combiner(a, b)));

   [Pure]
   public static ΤruΤxtResult<TValue> Where<TValue>(this ΤruΤxtResult<TValue> τruΤxtResult, Predicate<TValue> predicate) =>
      τruΤxtResult.Match(ok: v => predicate(v), fail: (_, _, _) => false)
         ? τruΤxtResult
         : ΤruΤxtResult<TValue>.Error("Unmatched predicate in where clause", string.Empty, string.Empty);
}