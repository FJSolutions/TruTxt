namespace TruConfig;

using System.Linq.Expressions;

using Microsoft.Extensions.Configuration;

public delegate ConfigResult ConfigDelegate<TModel>(Expression<Func<TModel, object>> expr);

public static class TruConfig
{
   public static ConfigDelegate<TModel> ConfigReader<TModel>(IConfiguration config)
   {
      return expr =>
      {
         var key = TruTxt.Common.ExpressionHelper.GetPropertyName(expr);
         var value = ConfigBuilder.GetValue(config, key);

         return value.Item1
            ? ConfigResult.Present(value.Item2)
            : ConfigResult.Missing($"No value could be round for the property: {key}");
      };
   }
}