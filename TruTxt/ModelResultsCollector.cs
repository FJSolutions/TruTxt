namespace TruTxt;

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Collections.Immutable;
using System.Linq.Expressions;

public record ModelResultsCollector<T>(ImmutableDictionary<string, ValidationResult> Results, bool IsValid)
   : ResultsCollector(Results, IsValid)
{
   [Pure]
   public ValidationResult Get(Expression<Func<T, object>> expr)
   {
      var key = expr.GetPropertyName();
      return base.Get(key);
   }

   [Pure]
   public ModelResultsCollector<T> Add(Expression<Func<T, object>> expr, ValidationResult result)
   {
      var key = expr.GetPropertyName();

      if (this.Results.TryGetValue(key, out var r2))
      {
         var builder = this.Results.ToBuilder();
         builder[key] = result + r2;
         var results = builder.ToImmutableDictionary();
         var isValid = results.Values.All(r => r.IsValid);
         return new ModelResultsCollector<T>(results, isValid);
      }

      return new ModelResultsCollector<T>(this.Results.Add(key, result), result.IsValid);
   }

   [Pure]
   public ModelResultsCollector<T> CompareResults(Expression<Func<T, object>> keyExpr1, Expression<Func<T, object>> keyExpr2,
      [NotNull] string message)
   {
      var result1 = Get(keyExpr1.GetPropertyName());
      var result2 = Get(keyExpr2.GetPropertyName());

      if (!result1.IsValid || !result2.IsValid) return this;

      return result1.AsValid().Value != result2.AsValid().Value
         ? this.Add(keyExpr2, new Invalid(result2.Text, new[] { message }))
         : this;
   }

   [Pure]
   public static new ModelResultsCollector<T> Empty() =>
      new(ImmutableDictionary<string, ValidationResult>.Empty, true);

   /// <summary>
   /// Creates a new <see cref="ResultsCollector"/> and collects the supplied <paramref name="result"/> to it
   /// using the supplied <paramref name="keyExpr"/>
   /// </summary>
   /// <param name="keyExpr">The key property expression to collect the result under.</param>
   /// <param name="result">The <see cref="ValidationResult"/> to add</param>
   /// <returns>The new <see cref="ResultsCollector"/></returns>
   [Pure]
   public static ModelResultsCollector<T> Create(Expression<Func<T, object>> keyExpr, ValidationResult result)
   {
      var key = keyExpr.GetPropertyName();
      var builder = ImmutableDictionary.Create<string, ValidationResult>();
      return new ModelResultsCollector<T>(builder.Add(key, result), result.IsValid);
   }

   /// <summary>
   /// <inheritdoc cref="op_Addition"/>
   /// </summary>
   /// <param name="lhs">The <see cref="ResultsCollector"/> to add the item to</param>
   /// <param name="rhs">A <see cref="Tuple{T1, ValidatioonResult}"/> containing the key to collect the result under.</param>
   /// <returns>The <see cref="ResultsCollector"/></returns>
   public static ModelResultsCollector<T> operator +(ModelResultsCollector<T> lhs, Tuple<Expression<Func<T, object>>, ValidationResult> rhs)
   {
      return lhs.Add(rhs.Item1, rhs.Item2);
   }
}


public static class ModelResultCollectorExtensions
{
   /// <summary>
   /// Creates a <see cref="Tuple"/> that can be Added to a <see cref="ResultsCollector"/>
   /// </summary>
   /// <param name="result">The <see cref="ValidationResult"/> to add to the collection</param>
   /// <param name="expr">The key expression name to add the <see cref="ValidationResult"/> under</param>
   /// <returns>A <see cref="Tuple"/> contsining the key name and <see cref="ValidationResult"/> to add to a
   /// <see cref="ResultsCollector"/>.</returns>
   public static Tuple<Expression<Func<T, object>>, ValidationResult> WithKey<T>(this ValidationResult result, Expression<Func<T, object>> expr) =>
      Tuple.Create(expr, result);
}