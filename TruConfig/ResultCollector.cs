namespace TruConfig;

using System.Diagnostics.Contracts;
using System.Linq.Expressions;

using TruTxt.Common;

// ReSharper disable once InconsistentNaming
public record ResultCollector<TModel>(ConfigResult<object>[] Results)
{
   public ConfigResult<T> GetValue<T>(Expression<Func<TModel, T>> expr)
   {
      // Get the key from the property and retrieve the result from the list, or null
      var key = expr.GetPropertyName();
      var result = this.Results.FirstOrDefault(r => r.Match(
         onPresent: m => key == m.PropertyName,
         onMissing: _ => false
      ));

      // If the result is null then collate  all the errors and return them 
      if (result is null)
      {
         var errors = 
            this.Results.Where(r => r is Missing<object>)
               .SelectMany(r => ((Missing<object>)r).Errors).ToArray();
         return new Missing<T>(errors);
      }

      // Cast the value to a strongly typed value and return a config result of that
      return result.Match(
         onPresent: p => ConfigResult<T>.Present((T)Convert.ChangeType(p.Value, typeof(T)), p.PropertyName),
         onMissing: m => new Missing<T>(m)
      );
   }

   /// <summary>
   /// Enables a more functional way to compose <see cref="ResultCollector{TModel}"/>s  
   /// </summary>
   /// <param name="lhs"></param>
   /// <param name="rhs"></param>
   /// <returns></returns>
   [Pure]
   public static ResultCollector<TModel> operator +(ResultCollector<TModel> lhs, ResultCollector<TModel> rhs)
      => new(lhs.Results.Concat(rhs.Results).ToArray());
}