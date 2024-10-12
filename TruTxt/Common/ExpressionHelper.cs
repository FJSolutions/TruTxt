using System.Linq.Expressions;

namespace TruTxt.Common;

public static class ExpressionHelper
{
   public static string GetPropertyName<T, U>(this Expression<Func<T,U>> expression)
   {
      if (expression.Body is MemberExpression b)
         return b.Member.Name;

      throw new TruTxtException("Cannot get a property name from the expression.");
   }
   
   public static Tuple<string, Type> GetPropertyAndType<T, U>(this Expression<Func<T,U>> expression)
   {
      if (expression.Body is MemberExpression b)
         return Tuple.Create(b.Member.Name, b.Member.DeclaringType);

      throw new TruTxtException("Cannot get a property name from the expression.");
   }
}