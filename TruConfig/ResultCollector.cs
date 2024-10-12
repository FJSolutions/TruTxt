namespace TruConfig;

using System.Diagnostics.Contracts;
using System.Linq.Expressions;

using TruTxt.Common;

// ReSharper disable once InconsistentNaming
public record ResultCollector<TModel>(ConfigResult<object>[] Results)
{
   public ConfigResult<T> GetValue<T>(Expression<Func<TModel, T>> expr)
   {
      var key = expr.GetPropertyName();
      var result = this.Results.FirstOrDefault(r => r.Match(
         onPresent: m => key == m.PropertyName,
         onMissing: _ => false
      ));

      if (result == null)
      {
         var errors = 
            this.Results.Where(r => r is Missing<object>)
               .SelectMany(r => ((Missing<object>)r).Errors).ToArray();
         //? Collate all the errors and send them through
         return new Missing<T>(errors);
      }

      return result.Match(
         onPresent: p => ConfigResult<T>.Present((T)p.Value, p.PropertyName),
         onMissing: m => new Missing<T>(m)
      );
   }

   [Pure]
   public static ResultCollector<TModel> operator +(ResultCollector<TModel> lhs, ResultCollector<TModel> rhs)
      => new(lhs.Results.Concat(rhs.Results).ToArray());
}