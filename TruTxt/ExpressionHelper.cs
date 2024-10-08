﻿using System.Linq.Expressions;

namespace TruTxt;

internal static class ExpressionHelper
{
   public static string GetPropertyName<T, U>(this Expression<Func<T,U>> expression)
   {
      if (expression.Body is MemberExpression b)
         return b.Member.Name;

      throw new TruTxtException("Cannot get a property name from the expression.");
   }
}