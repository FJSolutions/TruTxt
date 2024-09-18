namespace TrueText;

/// <summary>
/// A class that can read validated text and trues to convert it to strongly typed values
/// </summary>
public class TrueReader
{
    private readonly Dictionary<string, string> _data;

    /// <summary>
    /// The constructor is internal so it can only be created from 
    /// </summary>
    /// <param name="data"></param>
    internal TrueReader(Dictionary<string, string> data)
    {
        this._data = data;
    }

    private string GetValue(string key)
    {
        if (this._data.TryGetValue(key, out var value))
            return value;

        throw new TrueTextException($"The key, '{key}, could not be found in the reader");
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and returns it as a <c>String</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<string> GetString(string key)
    {
        return GetValue(key);
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="long"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<long> GetInt64(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseInt64(value).Match(
            some: Result<long>.Ok,
            none: () => Result<long>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="int"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<int> GetInt32(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseInt32(value).Match(
            some: Result<int>.Ok,
            none: () => Result<int>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="short"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<short> GetInt16(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseInt16(value).Match(
            some: Result<short>.Ok,
            none: () => Result<short>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="byte"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<byte> GetUInt8(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseUInt8(value).Match(
            some: Result<byte>.Ok,
            none: () => Result<byte>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="ulong"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<ulong> GetUInt64(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseUInt64(value).Match(
            some: Result<ulong>.Ok,
            none: () => Result<ulong>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="UInt32"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<uint> GetUInt32(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseUInt32(value).Match(
            some: Result<uint>.Ok,
            none: () => Result<uint>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="ushort"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="ushort"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<ushort> GetUInt16(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseUInt16(value).Match(
            some: Result<ushort>.Ok,
            none: () => Result<ushort>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="SByte"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="SByte"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<sbyte> GetInt8(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseInt8(value).Match(
            some: Result<sbyte>.Ok,
            none: () => Result<sbyte>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="Decimal"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="Decimal"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<decimal> GetDecimal(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseDecimal(value).Match(
            some: Result<decimal>.Ok,
            none: () => Result<decimal>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="Double"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="Double"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<double> GetDouble(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseDouble(value).Match(
            some: Result<double>.Ok,
            none: () => Result<double>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="Single"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="Single"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<float> GetSingle(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseSingle(value).Match(
            some: Result<float>.Ok,
            none: () => Result<float>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="Guid"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="Guid"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<Guid> GetGuid(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseGuid(value).Match(
            some: Result<Guid>.Ok,
            none: () => Result<Guid>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="bool"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="bool"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<bool> GetBoolean(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseBool(value).Match(
            some: Result<bool>.Ok,
            none: () => Result<bool>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="DateTime"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<DateTime> GetDateTime(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseDateTime(value).Match(
            some: Result<DateTime>.Ok,
            none: () => Result<DateTime>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <c>DateOnly</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="DateOnly"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<DateOnly> GetDate(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseDate(value).Match(
            some: Result<DateOnly>.Ok,
            none: () => Result<DateOnly>.Fail($"'{value}' cannot be converted to an integer", key, value)
        );
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="TimeOnly"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Fail{TValue}"/> with an error message</returns>
    public Result<TimeOnly> GetTime(string key)
    {
        var value = GetValue(key);

        return TrueParser.ParseTime(value).Match(
            some: Result<TimeOnly>.Ok,
            none: () => Result<TimeOnly>.Fail($"'{value}' cannot be converted to an integer", key, value)
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
    private static Result<Option<T>> FailOption<T>(string error, string key, string text) => new Fail<Option<T>>(error, key, text);

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

        return TrueParser.ParseString(value).Match(
            some: SomeResult,
            none: () => FailOption<string>($"'{value}' cannot be converted to an string", key, value)
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

        return TrueParser.ParseInt32(value).Match(
            some: SomeResult,
            none: () => FailOption<int>($"'{value}' cannot be converted to an integer", key, value)
        );
    }
}

/**************************************
 *
 * Result type
 * 
 *************************************/

/// <summary>
/// Represents the result of a <see cref="TrueReader"/> read method. 
/// </summary>
/// <typeparam name="TValue"></typeparam>
public abstract record Result<TValue>
{
    /// <summary>
    /// Matches this <see cref="Result{A}"/> against the two supplied functions. 
    /// </summary>
    /// <param name="ok">The function to execute if the <see cref="Result{A}"/> is an <see cref="Ok{T}"/></param>
    /// <param name="fail">The function to execute if the <see cref="Result{A}"/> is a <see cref="Fail{TValue}"/></param>
    /// <typeparam name="TResult">The return type of the method</typeparam>
    /// <returns>An instance of <typeparam name="TResult"></typeparam></returns>
    public TResult Match<TResult>(Func<TValue, TResult> ok, Func<string, string, string, TResult> fail)
    {
        return this switch
        {
            Ok<TValue>(var v) => ok(v),
            Fail<TValue>(var message, var key, var text) => fail(message, key, text),
            _ => throw new TrueTextException("Unknown Result type")
        };
    }

    /// <summary>
    /// Matches this <see cref="Result{A}"/> against the two supplied functions. 
    /// </summary>
    /// <param name="ok">The action to execute if the <see cref="Result{A}"/> is an <see cref="Ok{T}"/></param>
    /// <param name="fail">The action to execute if the <see cref="Result{A}"/> is a <see cref="Fail{TValue}"/></param>
    public void Match(Action<TValue> ok, Action<string, string, string> fail)
    {
        switch (this)
        {
            case Ok<TValue>(var v):
                ok(v);
                break;
            case Fail<TValue>(var message, var key, var text):
                fail(message, key, text);
                break;
            default:
                throw new TrueTextException("Unknown Result type");
        }
    }

    /// <summary>
    /// Maps the value of this <see cref="Result{A}"/> if it is an <see cref="Ok{T}"/> 
    /// </summary>
    /// <param name="mapper">The mapper function</param>
    /// <typeparam name="TResult">The return type of the mapping</typeparam>
    /// <returns>A <see cref="Result{B}"/></returns>
    public Result<TResult> Map<TResult>(Func<TValue, TResult> mapper)
    {
        return this switch
        {
            Ok<TValue> valid => mapper(valid.Value),
            Fail<TValue> failure => Result<TResult>.Fail(failure.Error, failure.Key, failure.Text),
            _ => throw new TrueTextException("Unknown result type!")
        };
    }

    /// <summary>
    /// Performs a function on the <see cref="Result{A}"/> if it is a <see cref="Ok{T}"/> which can return a <see cref="Fail{TValue}"/> 
    /// </summary>
    /// <param name="binder">The binding function</param>
    /// <typeparam name="TResult">The return type for the <see cref="Result{B}"/></typeparam>
    /// <returns>A <see cref="Result{B}"/></returns>
    public Result<TResult> Bind<TResult>(Func<TValue, Result<TResult>> binder)
    {
        return this switch
        {
            Ok<TValue> valid => binder(valid.Value),
            Fail<TValue> failure => Result<TResult>.Fail(failure.Error, failure.Key, failure.Text),
            _ => throw new TrueTextException("Unknown result type!")
        };
    }

    public static Result<TValue> Ok(TValue value) => new Ok<TValue>(value);
    
    public static Result<TValue> Fail(string error, string key, string text) => new Fail<TValue>(error, key, text);

    /// <summary>
    /// Implicitly converts a value to a <see cref="Ok{A}"/>
    /// </summary>
    /// <param name="value">The value to put in a <see cref="Ok{A}"/></param>
    /// <returns>A <see cref="Result{A}"/></returns>
    public static implicit operator Result<TValue>(TValue value) => new Ok<TValue>(value);
}

/// <summary>
/// Represents the successful <see cref="Result{A}"/> type containing a value
/// </summary>
/// <param name="Value">The value the <see cref="Ok{A}"/> contains</param>
/// <typeparam name="TValue">The type of the <see cref="Result{A}"/>'s value</typeparam>
public sealed record Ok<TValue>(TValue Value) : Result<TValue>;

/// <summary>
/// Represents the failure <see cref="Result{A}"/>, containing an error message
/// </summary>
/// <param name="Error">The error message</param>
public sealed record Fail<TValue>(string Error, string Key, string Text) : Result<TValue>;

/// <summary>
/// LINQ extensions for the <see cref="Result{A}"/> type 
/// </summary>
public static class ResultExtensions
{
    public static Result<TResult> Select<TValue, TResult>(this Result<TValue> result, Func<TValue, TResult> mapper) =>
        result.Map(mapper);

    public static Result<TResult> SelectMany<TValue, TResult>(this Result<TValue> result,
        Func<TValue, Result<TResult>> binder) => result.Bind(binder);

    public static Result<TResult> SelectMany<TValue, TIntermediate, TResult>
    (this Result<TValue> result, Func<TValue, Result<TIntermediate>> binder,
        Func<TValue, TIntermediate, TResult> combiner) =>
        result.Bind(a => binder(a).Map(b => combiner(a, b)));

    public static Result<TValue> Where<TValue>(this Result<TValue> result, Predicate<TValue> predicate) =>
        result.Match(ok: v => predicate(v), fail: (_, _, _) => false)
            ? result
            : Result<TValue>.Fail("Unmatched predicate in where clause", string.Empty, string.Empty);
}