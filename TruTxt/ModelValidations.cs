namespace TruTxt;

using Common;

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Collections.Immutable;
using System.Linq.Expressions;

public record ModelValidations<T>(ImmutableDictionary<string, ValidationResult> Results, bool IsValid)
   : ResultsCollector(Results, IsValid)
{
   [Pure]
   public ValidationResult Get(Expression<Func<T, object>> expr)
   {
      var key = expr.GetPropertyName();
      return base.Get(key);
   }

   [Pure]
   private ModelValidations<T> Add(Expression<Func<T, object>> expr, ValidationResult result)
   {
      var key = expr.GetPropertyName();

      if (this.Results.TryGetValue(key, out var r2))
      {
         var builder = this.Results.ToBuilder();
         builder[key] = result + r2;
         var results = builder.ToImmutableDictionary();
         var isValid = results.Values.All(r => r.IsValid);
         return new ModelValidations<T>(results, isValid);
      }

      return new ModelValidations<T>(this.Results.Add(key, result), result.IsValid);
   }

   [Pure]
   private ModelValidations<T> AddRange(ModelValidations<T> other)
   {
      var builder = this.Results.ToBuilder();
      foreach (var pair in other.Results)
      {
         if (this.Results.TryGetValue(pair.Key, out var r2))
            builder[pair.Key] = pair.Value + r2;
         else
            builder[pair.Key] = pair.Value;
      }

      var dict = builder.ToImmutableDictionary();
      var isValid = dict.Values.All(r => r.IsValid);

      return new ModelValidations<T>(dict, isValid);
   }

   [Pure]
   public ModelValidations<T> CompareResults(Expression<Func<T, object>> keyExpr1,
      Expression<Func<T, object>> keyExpr2,
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
   public static new ModelValidations<T> Empty() =>
      new(ImmutableDictionary<string, ValidationResult>.Empty, true);

   /// <summary>
   /// Creates a new <see cref="ResultsCollector"/> and collects the supplied <paramref name="result"/> to it
   /// using the supplied <paramref name="keyExpr"/>
   /// </summary>
   /// <param name="keyExpr">The key property expression to collect the result under.</param>
   /// <param name="result">The <see cref="ValidationResult"/> to add</param>
   /// <returns>The new <see cref="ResultsCollector"/></returns>
   [Pure]
   private static ModelValidations<T> Create(Expression<Func<T, object>> keyExpr, ValidationResult result)
   {
      var key = keyExpr.GetPropertyName();
      var builder = ImmutableDictionary.Create<string, ValidationResult>();
      return new ModelValidations<T>(builder.Add(key, result), result.IsValid);
   }

   // /// <summary>
   // /// <inheritdoc cref="op_Addition"/>
   // /// </summary>
   // /// <param name="lhs">The <see cref="ResultsCollector"/> to add the item to</param>
   // /// <param name="rhs">A <see cref="Tuple{T1, ValidatioonResult}"/> containing the key to collect the result under.</param>
   // /// <returns>The <see cref="ResultsCollector"/></returns>
   public static ModelValidations<T> operator +(ModelValidations<T> lhs, ModelValidations<T> rhs) =>
      lhs.AddRange(rhs);

   /// <summary>
   /// Creates a Runner function that processes a <see cref="ValidationResult"/>s for a property name gleaned from
   /// the supplied <see cref="Expression{TDelegate}"/>, and returns a new <see cref="ModelValidations{T}"/> collection.
   /// </summary>
   /// <returns>A <see cref="Runner"/> function</returns>
   public static Runner<T> Runner() => Create;
}

/// <summary>
/// The <see cref="Delegate"/> definition for the <see cref="ModelValidations{T}"/> Runner method.
/// </summary>
/// <typeparam name="T">The type of model object whose property name will be used as the key for the
/// <see cref="ValidationResult"/></typeparam>
public delegate ModelValidations<T> Runner<T>(Expression<Func<T, object>> keyExpr, ValidationResult result);
