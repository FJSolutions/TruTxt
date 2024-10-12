using TruTxt.Common;

namespace TruTxt;

/// <summary>
/// A class that can read validated text and trues to convert it to strongly typed values
/// </summary>
public class TruReader
{
   private readonly Dictionary<string, string> _data;

   /// <summary>
   /// The constructor is internal so it can only be created from 
   /// </summary>
   /// <param name="data"></param>
   internal TruReader(Dictionary<string, string> data)
   {
      this._data = data;
   }

   /// <summary>
   /// Tries to get a value from the underlying data source.
   /// </summary>
   /// <param name="key">the key for the data</param>
   /// <returns>A string value for the supplied key</returns>
   /// <exception cref="TruTxtException"> is thrown if the key was not present in the data source.</exception>
   private string GetValue(string key)
   {
      if (this._data.TryGetValue(key, out var value))
         return value;

      throw new TruTxtException($"The key, '{key}, could not be found in the reader");
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and returns it as a <c>String</c>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<string> GetString(string key)
   {
      return GetValue(key);
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="long"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<long> GetInt64(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseInt64(value).Match(
         onSome: ΤruΤxtResult<long>.Success,
         onNone: () => ΤruΤxtResult<long>.Failure($"'{value}' cannot be converted to an Int64", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="int"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<int> GetInt32(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseInt32(value).Match(
         onSome: ΤruΤxtResult<int>.Success,
         onNone: () => ΤruΤxtResult<int>.Failure($"'{value}' cannot be converted to an Int32", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="short"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<short> GetInt16(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseInt16(value).Match(
         onSome: ΤruΤxtResult<short>.Success,
         onNone: () => ΤruΤxtResult<short>.Failure($"'{value}' cannot be converted to an Int16", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="byte"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<byte> GetUInt8(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseUInt8(value).Match(
         onSome: ΤruΤxtResult<byte>.Success,
         onNone: () => ΤruΤxtResult<byte>.Failure($"'{value}' cannot be converted to a Byte", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="ulong"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<ulong> GetUInt64(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseUInt64(value).Match(
         onSome: ΤruΤxtResult<ulong>.Success,
         onNone: () => ΤruΤxtResult<ulong>.Failure($"'{value}' cannot be converted to a UInt64", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="UInt32"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<uint> GetUInt32(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseUInt32(value).Match(
         onSome: ΤruΤxtResult<uint>.Success,
         onNone: () => ΤruΤxtResult<uint>.Failure($"'{value}' cannot be converted to a UInt32", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="ushort"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="ushort"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<ushort> GetUInt16(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseUInt16(value).Match(
         onSome: ΤruΤxtResult<ushort>.Success,
         onNone: () => ΤruΤxtResult<ushort>.Failure($"'{value}' cannot be converted to a UInt16", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="SByte"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="SByte"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<sbyte> GetInt8(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseInt8(value).Match(
         onSome: ΤruΤxtResult<sbyte>.Success,
         onNone: () => ΤruΤxtResult<sbyte>.Failure($"'{value}' cannot be converted to a SByte", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="Decimal"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="Decimal"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<decimal> GetDecimal(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseDecimal(value).Match(
         onSome: ΤruΤxtResult<decimal>.Success,
         onNone: () => ΤruΤxtResult<decimal>.Failure($"'{value}' cannot be converted to a Decimal", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="Double"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="Double"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<double> GetDouble(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseDouble(value).Match(
         onSome: ΤruΤxtResult<double>.Success,
         onNone: () => ΤruΤxtResult<double>.Failure($"'{value}' cannot be converted to a Double", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="Single"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="Single"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<float> GetSingle(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseSingle(value).Match(
         onSome: ΤruΤxtResult<float>.Success,
         onNone: () => ΤruΤxtResult<float>.Failure($"'{value}' cannot be converted to a Single", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="Guid"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="Guid"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<Guid> GetGuid(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseGuid(value).Match(
         onSome: ΤruΤxtResult<Guid>.Success,
         onNone: () => ΤruΤxtResult<Guid>.Failure($"'{value}' cannot be converted to a GUID", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="bool"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="bool"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<bool> GetBoolean(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseBool(value).Match(
         onSome: ΤruΤxtResult<bool>.Success,
         onNone: () => ΤruΤxtResult<bool>.Failure($"'{value}' cannot be converted to a boolean", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="DateTime"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="DateTime"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<DateTime> GetDateTime(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseDateTime(value).Match(
         onSome: ΤruΤxtResult<DateTime>.Success,
         onNone: () => ΤruΤxtResult<DateTime>.Failure($"'{value}' cannot be converted to a DateTime", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <c>DateOnly</c>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="DateOnly"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<DateOnly> GetDate(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseDate(value).Match(
         onSome: ΤruΤxtResult<DateOnly>.Success,
         onNone: () => ΤruΤxtResult<DateOnly>.Failure($"'{value}' cannot be converted to a DateOnly", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="TimeOnly"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Failure{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<TimeOnly> GetTime(string key)
   {
      var value = GetValue(key);

      return TruTxtParser.ParseTime(value).Match(
         onSome: ΤruΤxtResult<TimeOnly>.Success,
         onNone: () => ΤruΤxtResult<TimeOnly>.Failure($"'{value}' cannot be converted to a TimeOnly", key, value)
      );
   }

   /// <summary>
   /// Indicates whether the value for the supplied <param name="key"></param> is null, empty, or whitespace.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns><c>true</c> if the value for the supplied <param name="key"></param> is null, empty, or whitespace; otherwise <c>false</c></returns>
   public bool IsEmpty(string key)
   {
      return string.IsNullOrWhiteSpace(GetValue(key));
   }

   private static ΤruΤxtResult<Option<T>> SomeResult<T>(T value) => new Success<Option<T>>(Option<T>.Some(value));
   private static ΤruΤxtResult<Option<T>> NoResult<T>() => new Success<Option<T>>(Option<T>.None());

   private static ΤruΤxtResult<Option<T>> FailOption<T>(string error, string key, string text) =>
      new Failure<Option<T>>(error, key, text);

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to an <see cref="int"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<string>> GetOptionalString(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<string>();

      return TruTxtParser.ParseString(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<string>($"'{value}' cannot be converted to a string", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to an <see cref="sbyte"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<sbyte>> GetOptionalInt8(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<sbyte>();

      return TruTxtParser.ParseInt8(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<sbyte>($"'{value}' cannot be converted to a SByte", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to an <see cref="short"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<short>> GetOptionalInt16(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<short>();

      return TruTxtParser.ParseInt16(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<short>($"'{value}' cannot be converted to a Int16", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to an <see cref="int"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<int>> GetOptionalInt32(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<int>();

      return TruTxtParser.ParseInt32(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<int>($"'{value}' cannot be converted to a Int32", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to an <see cref="long"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<long>> GetOptionalInt64(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<long>();

      return TruTxtParser.ParseInt64(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<long>($"'{value}' cannot be converted to a Int64", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to an <see cref="byte"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<byte>> GetOptionalUInt8(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<byte>();

      return TruTxtParser.ParseUInt8(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<byte>($"'{value}' cannot be converted to a UInt8", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to an <see cref="short"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<ushort>> GetOptionalUInt16(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<ushort>();

      return TruTxtParser.ParseUInt16(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<ushort>($"'{value}' cannot be converted to a UInt16", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to an <see cref="uint"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<uint>> GetOptionalUInt32(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<uint>();

      return TruTxtParser.ParseUInt32(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<uint>($"'{value}' cannot be converted to a UInt32", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to an <see cref="ulong"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<ulong>> GetOptionalUInt64(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<ulong>();

      return TruTxtParser.ParseUInt64(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<ulong>($"'{value}' cannot be converted to a UInt64", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to a <see cref="float"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<float>> GetOptionalSingle(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<float>();

      return TruTxtParser.ParseSingle(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<float>($"'{value}' cannot be converted to a single", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to a <see cref="double"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<double>> GetOptionalDouble(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<double>();

      return TruTxtParser.ParseDouble(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<double>($"'{value}' cannot be converted to a double", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to a <see cref="decimal"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<decimal>> GetOptionalDecimal(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<decimal>();

      return TruTxtParser.ParseDecimal(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<decimal>($"'{value}' cannot be converted to a decimal", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to a <see cref="bool"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<bool>> GetOptionalBoolean(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<bool>();

      return TruTxtParser.ParseBool(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<bool>($"'{value}' cannot be converted to a boolean", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to a <see cref="Guid"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<Guid>> GetOptionalGuid(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<Guid>();

      return TruTxtParser.ParseGuid(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<Guid>($"'{value}' cannot be converted to a GUID", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to a <see cref="DateTime"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<DateTime>> GetOptionalDateTime(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<DateTime>();

      return TruTxtParser.ParseDateTime(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<DateTime>($"'{value}' cannot be converted to a DateTime", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to a <see cref="DateOnly"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<DateOnly>> GetOptionalDate(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<DateOnly>();

      return TruTxtParser.ParseDate(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<DateOnly>($"'{value}' cannot be converted to a DateOnly", key, value)
      );
   }

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to a <see cref="TimeOnly"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="ΤρυΤξτΤρυΤξτResult{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public ΤruΤxtResult<Option<TimeOnly>> GetOptionalTime(string key)
   {
      var value = GetValue(key);

      if (string.IsNullOrWhiteSpace(value))
         return NoResult<TimeOnly>();

      return TruTxtParser.ParseTime(value).Match(
         onSome: SomeResult,
         onNone: () => FailOption<TimeOnly>($"'{value}' cannot be converted to a TimeOnly", key, value)
      );
   }
}