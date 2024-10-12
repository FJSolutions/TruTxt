namespace TruTxt.Common;

using System.Diagnostics.Contracts;

/// <summary>
/// Contains a set of parser methods for the following types:
/// - String.
/// - All the BCL Integer numbers, signed and unsigned (byte, short, int, long).
/// - All the BCL floating point numbers (double, float, decimal).
/// - GUID
/// - DateTime, DateOnly, and TimeOnly
/// <para>All the functions return an <see cref="Option{TValue}"/></para>
/// </summary>
[Pure]
public static class TruTxtParser
{
   /// <summary>
   /// Tries to parse the supplied string to any of the included parsers based on the return type of
   /// <typeparam name="T"></typeparam>  
   /// </summary>
   /// <param name="value">The <see cref="String"/> to parse</param>
   /// <typeparam name="T">The type to parse the string value as</typeparam>
   /// <returns>An instance of <typeparam name="T"></typeparam></returns>
   /// <exception cref="NotImplementedException">If the type is not one of the included parsers</exception>
   // public static Option<T> Parse<T>(string value)
   public static Option<object> Parse<T>(string value)
   {
      var t = typeof(T);

      if (t == typeof(String))
         return ParseString(value).Map(x => (object)x);
      if (t == typeof(sbyte))
         return ParseInt8(value).Map(x => (object)x);
      if (t == typeof(Int16))
         return ParseInt16(value).Map(x => (object)x);
      if (t == typeof(Int32))
         return ParseInt32(value).Map(x => (object)x);
      if (t == typeof(Int64))
         return ParseInt64(value).Map(x => (object)x);
      if (t == typeof(byte))
         return ParseUInt8(value).Map(x => (object)x);
      if (t == typeof(UInt16))
         return ParseUInt16(value).Map(x => (object)x);
      if (t == typeof(UInt32))
         return ParseUInt32(value).Map(x => (object)x);
      if (t == typeof(UInt64))
         return ParseUInt64(value).Map(x => (object)x);
      if (t == typeof(double))
         return ParseDouble(value).Map(x => (object)x);
      if (t == typeof(float))
         return ParseSingle(value).Map(x => (object)x);
      if (t == typeof(decimal))
         return ParseDecimal(value).Map(x => (object)x);
      if (t == typeof(Guid))
         return ParseGuid(value).Map(x => (object)x);
      if (t == typeof(DateTime))
         return ParseDateTime(value).Map(x => (object)x);
      if (t == typeof(DateOnly))
         return ParseDate(value).Map(x => (object)x);
      if (t == typeof(TimeOnly))
         return ParseTime(value).Map(x => (object)x);

      throw new NotImplementedException($"No TruTxtParser implemented for type '{t}'");
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as a <see cref="string"/>
   /// </summary>
   /// <param name="value">The value to parse as a <see cref="String"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<string> ParseString(string value)
   {
      if (string.IsNullOrWhiteSpace(value))
         return Option<string>.Some(string.Empty);

      return Option<string>.Some(value);
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="sbyte"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="sbyte"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<sbyte> ParseInt8(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (sbyte.TryParse(value, out var val))
            return Option<sbyte>.Some(val);
      }

      return Option<sbyte>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="short"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="short"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<short> ParseInt16(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (short.TryParse(value, out var val))
            return Option<short>.Some(val);
      }

      return Option<short>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="int"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="int"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<int> ParseInt32(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (int.TryParse(value, out var val))
            return Option<int>.Some(val);
      }

      return Option<int>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="long"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="long"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<long> ParseInt64(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (long.TryParse(value, out var val))
            return Option<long>.Some(val);
      }

      return Option<long>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="byte"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="byte"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<byte> ParseUInt8(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (byte.TryParse(value, out var val))
            return Option<byte>.Some(val);
      }

      return Option<byte>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="ushort"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="ushort"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<ushort> ParseUInt16(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (ushort.TryParse(value, out var val))
            return Option<ushort>.Some(val);
      }

      return Option<ushort>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="uint"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="uint"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<uint> ParseUInt32(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (uint.TryParse(value, out var val))
            return Option<uint>.Some(val);
      }

      return Option<uint>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="ulong"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="ulong"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<ulong> ParseUInt64(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (ulong.TryParse(value, out var val))
            return Option<ulong>.Some(val);
      }

      return Option<ulong>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="float"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="float"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<float> ParseSingle(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (float.TryParse(value, out var val))
            return Option<float>.Some(val);
      }

      return Option<float>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="double"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="double"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<double> ParseDouble(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (double.TryParse(value, out var val))
            return Option<double>.Some(val);
      }

      return Option<double>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="decimal"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="decimal"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<decimal> ParseDecimal(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (decimal.TryParse(value, out var val))
            return Option<decimal>.Some(val);
      }

      return Option<decimal>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="Guid"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="Guid"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<Guid> ParseGuid(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (Guid.TryParse(value, out var val))
            return Option<Guid>.Some(val);
      }

      return Option<Guid>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="DateTime"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="DateTime"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<DateTime> ParseDateTime(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (DateTime.TryParse(value, out var val))
            return Option<DateTime>.Some(val);
      }

      return Option<DateTime>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="DateOnly"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="DateOnly"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<DateOnly> ParseDate(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (DateOnly.TryParse(value, out var val))
            return Option<DateOnly>.Some(val);
      }

      return Option<DateOnly>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="TimeOnly"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="TimeOnly"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<TimeOnly> ParseTime(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         if (TimeOnly.TryParse(value, out var val))
            return Option<TimeOnly>.Some(val);
      }

      return Option<TimeOnly>.None();
   }

   /// <summary>
   /// Parses a <see cref="String"/> value as an <see cref="Boolean"/>
   /// </summary>
   /// <param name="value">The value to parse as an <see cref="Boolean"/></param>
   /// <returns>A <see cref="Option{TValue}"/></returns>
   [Pure]
   public static Option<bool> ParseBool(string value)
   {
      if (!string.IsNullOrWhiteSpace(value))
      {
         switch (value.ToLowerInvariant())
         {
            case "true":
            case "on":
            case "yes":
            case "1":
               return Option<bool>.Some(true);
            case "false":
            case "off":
            case "no":
            case "0":
               return Option<bool>.Some(false);
         }
      }

      return Option<bool>.None();
   }
}