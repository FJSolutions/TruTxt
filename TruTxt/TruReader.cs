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
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<string> GetString(string key)
   {
      return GetValue(key);
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="long"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<long> GetInt64(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseInt64(value).Match(
         some: Result<long>.Ok,
         none: () => Result<long>.Fail($"'{value}' cannot be converted to an Int64", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="int"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<int> GetInt32(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseInt32(value).Match(
         some: Result<int>.Ok,
         none: () => Result<int>.Fail($"'{value}' cannot be converted to an Int32", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="short"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<short> GetInt16(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseInt16(value).Match(
         some: Result<short>.Ok,
         none: () => Result<short>.Fail($"'{value}' cannot be converted to an Int16", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="byte"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<byte> GetUInt8(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseUInt8(value).Match(
         some: Result<byte>.Ok,
         none: () => Result<byte>.Fail($"'{value}' cannot be converted to a Byte", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="ulong"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<ulong> GetUInt64(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseUInt64(value).Match(
         some: Result<ulong>.Ok,
         none: () => Result<ulong>.Fail($"'{value}' cannot be converted to a UInt64", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="UInt32"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<uint> GetUInt32(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseUInt32(value).Match(
         some: Result<uint>.Ok,
         none: () => Result<uint>.Fail($"'{value}' cannot be converted to a UInt32", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="ushort"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="ushort"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<ushort> GetUInt16(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseUInt16(value).Match(
         some: Result<ushort>.Ok,
         none: () => Result<ushort>.Fail($"'{value}' cannot be converted to a UInt16", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="SByte"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="SByte"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<sbyte> GetInt8(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseInt8(value).Match(
         some: Result<sbyte>.Ok,
         none: () => Result<sbyte>.Fail($"'{value}' cannot be converted to a SByte", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="Decimal"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="Decimal"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<decimal> GetDecimal(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseDecimal(value).Match(
         some: Result<decimal>.Ok,
         none: () => Result<decimal>.Fail($"'{value}' cannot be converted to a Decimal", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="Double"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="Double"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<double> GetDouble(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseDouble(value).Match(
         some: Result<double>.Ok,
         none: () => Result<double>.Fail($"'{value}' cannot be converted to a Double", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="Single"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="Single"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<float> GetSingle(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseSingle(value).Match(
         some: Result<float>.Ok,
         none: () => Result<float>.Fail($"'{value}' cannot be converted to a Single", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="Guid"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="Guid"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<Guid> GetGuid(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseGuid(value).Match(
         some: Result<Guid>.Ok,
         none: () => Result<Guid>.Fail($"'{value}' cannot be converted to a GUID", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="bool"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="bool"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<bool> GetBoolean(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseBool(value).Match(
         some: Result<bool>.Ok,
         none: () => Result<bool>.Fail($"'{value}' cannot be converted to a boolean", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="DateTime"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="DateTime"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<DateTime> GetDateTime(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseDateTime(value).Match(
         some: Result<DateTime>.Ok,
         none: () => Result<DateTime>.Fail($"'{value}' cannot be converted to a DateTime", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <c>DateOnly</c>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>The <see cref="DateOnly"/> value of the key in the data source.</returns>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<DateOnly> GetDate(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseDate(value).Match(
         some: Result<DateOnly>.Ok,
         none: () => Result<DateOnly>.Fail($"'{value}' cannot be converted to a DateOnly", key, value)
      );
   }

   /// <summary>
   /// Tries to get a value from the validated TruTxt validation results source, and tries to convert it to a <see cref="TimeOnly"/>.
   /// </summary>
   /// <param name="key">The key value to look-up in the data store</param>
   /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
   public Result<TimeOnly> GetTime(string key)
   {
      var value = GetValue(key);

      return TruParser.ParseTime(value).Match(
         some: Result<TimeOnly>.Ok,
         none: () => Result<TimeOnly>.Fail($"'{value}' cannot be converted to a TimeOnly", key, value)
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

   private static Result<Option<T>> SomeResult<T>(T value) => new Ok<Option<T>>(Option<T>.Some(value));
   private static Result<Option<T>> NoResult<T>() => new Ok<Option<T>>(Option<T>.None());

   private static Result<Option<T>> FailOption<T>(string error, string key, string text) =>
      new Fail<Option<T>>(error, key, text);

   /// <summary>
   /// Tries to get an optional value from the data source and convert it to an <see cref="int"/>.
   /// <para>If the source value is empty or whitespace, then the default value is returned as a success value</para>
   /// </summary>
   /// <param name="key"></param>
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<string>> GetOptionalString(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<sbyte>> GetOptionalInt8(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<short>> GetOptionalInt16(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<int>> GetOptionalInt32(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<long>> GetOptionalInt64(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<byte>> GetOptionalUInt8(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<ushort>> GetOptionalUInt16(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<uint>> GetOptionalUInt32(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<ulong>> GetOptionalUInt64(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<float>> GetOptionalSingle(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<double>> GetOptionalDouble(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<decimal>> GetOptionalDecimal(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<bool>> GetOptionalBoolean(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<Guid>> GetOptionalGuid(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<DateTime>> GetOptionalDateTime(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<DateOnly>> GetOptionalDate(string key)
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
   /// <returns>A <see cref="Result{TValue}"/> containing an <see cref="Option{TValue}"/></returns>
   public Result<Option<TimeOnly>> GetOptionalTime(string key)
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