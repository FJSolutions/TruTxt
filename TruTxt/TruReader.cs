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
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<string> GetString(string key)
   {
      return GetValue(key);
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="long"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<long> GetInt64(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseInt64(value).Match(
         some: ΤruΤxtResult<long>.Ok,
         none: () => ΤruΤxtResult<long>.Error($"'{value}' cannot be converted to an Int64", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="int"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<int> GetInt32(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseInt32(value).Match(
         some: ΤruΤxtResult<int>.Ok,
         none: () => ΤruΤxtResult<int>.Error($"'{value}' cannot be converted to an Int32", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="short"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<short> GetInt16(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseInt16(value).Match(
         some: ΤruΤxtResult<short>.Ok,
         none: () => ΤruΤxtResult<short>.Error($"'{value}' cannot be converted to an Int16", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="byte"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<byte> GetUInt8(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseUInt8(value).Match(
         some: ΤruΤxtResult<byte>.Ok,
         none: () => ΤruΤxtResult<byte>.Error($"'{value}' cannot be converted to a Byte", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="ulong"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<ulong> GetUInt64(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseUInt64(value).Match(
         some: ΤruΤxtResult<ulong>.Ok,
         none: () => ΤruΤxtResult<ulong>.Error($"'{value}' cannot be converted to a UInt64", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="UInt32"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<uint> GetUInt32(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseUInt32(value).Match(
         some: ΤruΤxtResult<uint>.Ok,
         none: () => ΤruΤxtResult<uint>.Error($"'{value}' cannot be converted to a UInt32", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="ushort"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="ushort"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<ushort> GetUInt16(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseUInt16(value).Match(
         some: ΤruΤxtResult<ushort>.Ok,
         none: () => ΤruΤxtResult<ushort>.Error($"'{value}' cannot be converted to a UInt16", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="SByte"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="SByte"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<sbyte> GetInt8(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseInt8(value).Match(
         some: ΤruΤxtResult<sbyte>.Ok,
         none: () => ΤruΤxtResult<sbyte>.Error($"'{value}' cannot be converted to a SByte", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="Decimal"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="Decimal"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<decimal> GetDecimal(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseDecimal(value).Match(
         some: ΤruΤxtResult<decimal>.Ok,
         none: () => ΤruΤxtResult<decimal>.Error($"'{value}' cannot be converted to a Decimal", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="Double"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="Double"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<double> GetDouble(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseDouble(value).Match(
         some: ΤruΤxtResult<double>.Ok,
         none: () => ΤruΤxtResult<double>.Error($"'{value}' cannot be converted to a Double", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="Single"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="Single"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<float> GetSingle(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseSingle(value).Match(
         some: ΤruΤxtResult<float>.Ok,
         none: () => ΤruΤxtResult<float>.Error($"'{value}' cannot be converted to a Single", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="Guid"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="Guid"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<Guid> GetGuid(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseGuid(value).Match(
         some: ΤruΤxtResult<Guid>.Ok,
         none: () => ΤruΤxtResult<Guid>.Error($"'{value}' cannot be converted to a GUID", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="bool"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="bool"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<bool> GetBoolean(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseBool(value).Match(
         some: ΤruΤxtResult<bool>.Ok,
         none: () => ΤruΤxtResult<bool>.Error($"'{value}' cannot be converted to a boolean", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="DateTime"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="DateTime"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<DateTime> GetDateTime(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseDateTime(value).Match(
         some: ΤruΤxtResult<DateTime>.Ok,
         none: () => ΤruΤxtResult<DateTime>.Error($"'{value}' cannot be converted to a DateTime", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <c>DateOnly</c>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="DateOnly"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<DateOnly> GetDate(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseDate(value).Match(
         some: ΤruΤxtResult<DateOnly>.Ok,
         none: () => ΤruΤxtResult<DateOnly>.Error($"'{value}' cannot be converted to a DateOnly", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="TimeOnly"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="ΤruΤxtResult{TValue}"/> containing the value is successful; or a <see cref="Error{TValue}"/> with an error message</returns>
   public ΤruΤxtResult<TimeOnly> GetTime(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseTime(value).Match(
         some: ΤruΤxtResult<TimeOnly>.Ok,
         none: () => ΤruΤxtResult<TimeOnly>.Error($"'{value}' cannot be converted to a TimeOnly", key, value)
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

   private static ΤruΤxtResult<Option<T>> SomeResult<T>(T value) => new Ok<Option<T>>(Option<T>.Some(value));
   private static ΤruΤxtResult<Option<T>> NoResult<T>() => new Ok<Option<T>>(Option<T>.None());

   private static ΤruΤxtResult<Option<T>> FailOption<T>(string error, string key, string text) =>
      new Error<Option<T>>(error, key, text);

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

      return TruParser.ParseString(value).Match(
         some: SomeResult,
         none: () => FailOption<string>($"'{value}' cannot be converted to a string", key, value)
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

      return TruParser.ParseInt8(value).Match(
         some: SomeResult,
         none: () => FailOption<sbyte>($"'{value}' cannot be converted to a SByte", key, value)
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

      return TruParser.ParseInt16(value).Match(
         some: SomeResult,
         none: () => FailOption<short>($"'{value}' cannot be converted to a Int16", key, value)
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

      return TruParser.ParseInt32(value).Match(
         some: SomeResult,
         none: () => FailOption<int>($"'{value}' cannot be converted to a Int32", key, value)
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

      return TruParser.ParseInt64(value).Match(
         some: SomeResult,
         none: () => FailOption<long>($"'{value}' cannot be converted to a Int64", key, value)
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

      return TruParser.ParseUInt8(value).Match(
         some: SomeResult,
         none: () => FailOption<byte>($"'{value}' cannot be converted to a UInt8", key, value)
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

      return TruParser.ParseUInt16(value).Match(
         some: SomeResult,
         none: () => FailOption<ushort>($"'{value}' cannot be converted to a UInt16", key, value)
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

      return TruParser.ParseUInt32(value).Match(
         some: SomeResult,
         none: () => FailOption<uint>($"'{value}' cannot be converted to a UInt32", key, value)
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

      return TruParser.ParseUInt64(value).Match(
         some: SomeResult,
         none: () => FailOption<ulong>($"'{value}' cannot be converted to a UInt64", key, value)
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

      return TruParser.ParseSingle(value).Match(
         some: SomeResult,
         none: () => FailOption<float>($"'{value}' cannot be converted to a single", key, value)
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

      return TruParser.ParseDouble(value).Match(
         some: SomeResult,
         none: () => FailOption<double>($"'{value}' cannot be converted to a double", key, value)
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

      return TruParser.ParseDecimal(value).Match(
         some: SomeResult,
         none: () => FailOption<decimal>($"'{value}' cannot be converted to a decimal", key, value)
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

      return TruParser.ParseBool(value).Match(
         some: SomeResult,
         none: () => FailOption<bool>($"'{value}' cannot be converted to a boolean", key, value)
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

      return TruParser.ParseGuid(value).Match(
         some: SomeResult,
         none: () => FailOption<Guid>($"'{value}' cannot be converted to a GUID", key, value)
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

      return TruParser.ParseDateTime(value).Match(
         some: SomeResult,
         none: () => FailOption<DateTime>($"'{value}' cannot be converted to a DateTime", key, value)
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

      return TruParser.ParseDate(value).Match(
         some: SomeResult,
         none: () => FailOption<DateOnly>($"'{value}' cannot be converted to a DateOnly", key, value)
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

      return TruParser.ParseTime(value).Match(
         some: SomeResult,
         none: () => FailOption<TimeOnly>($"'{value}' cannot be converted to a TimeOnly", key, value)
      );
   }
}