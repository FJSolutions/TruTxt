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
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<string> GetString(string key)
    {
        return GetValue(key);
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="long"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<long> GetInt64(string key)
    {
        var value = GetValue(key);

        try
        {
            return Convert.ToInt64(value);
        }
        catch (Exception)
        {
            return new Failure<long>($"$Unable to convert '{value}' to a long integer", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="int"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<int> GetInt32(string key)
    {
        var value = GetValue(key);

        try
        {
            return Convert.ToInt32(value);
        }
        catch (Exception)
        {
            return new Failure<int>($"$Unable to convert '{value}' to an integer", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="short"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<short> GetInt16(string key)
    {
        var value = GetValue(key);

        try
        {
            return Convert.ToInt16(value);
        }
        catch (Exception)
        {
            return new Failure<short>($"$Unable to convert '{value}' to a short integer", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="byte"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<byte> GetUInt8(string key)
    {
        var value = GetValue(key);

        try
        {
            return Convert.ToByte(value);
        }
        catch (Exception)
        {
            return new Failure<byte>($"$Unable to convert '{value}' to a byte", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="ulong"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<ulong> GetUInt64(string key)
    {
        var value = GetValue(key);

        try
        {
            return Convert.ToUInt64(value);
        }
        catch (Exception)
        {
            return new Failure<ulong>($"$Unable to convert '{value}' to an unsigned long integer", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="UInt32"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<uint> GetUInt32(string key)
    {
        var value = GetValue(key);

        try
        {
            return Convert.ToUInt32(value);
        }
        catch (Exception)
        {
            return new Failure<uint>($"$Unable to convert '{value}' to an unsigned integer", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="ushort"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="ushort"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<ushort> GetUInt16(string key)
    {
        var value = GetValue(key);

        try
        {
            return Convert.ToUInt16(value);
        }
        catch (Exception)
        {
            return new Failure<ushort>($"$Unable to convert '{value}' to an unsigned 16 bit integer", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="SByte"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="SByte"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<sbyte> GetInt8(string key)
    {
        var value = GetValue(key);

        try
        {
            return Convert.ToSByte(value);
        }
        catch (Exception)
        {
            return new Failure<sbyte>($"$Unable to convert '{value}' to a signed byte", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="Decimal"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="Decimal"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<decimal> GetDecimal(string key)
    {
        var value = GetValue(key);

        try
        {
            return Convert.ToDecimal(value);
        }
        catch (Exception)
        {
            return new Failure<decimal>($"$Unable to convert '{value}' to a decimal number", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="Double"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="Double"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<double> GetDouble(string key)
    {
        var value = GetValue(key);

        try
        {
            return Convert.ToDouble(value);
        }
        catch (Exception)
        {
            return new Failure<double>($"$Unable to convert '{value}' to a double precision floating point number", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="Single"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="Single"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<float> GetSingle(string key)
    {
        var value = GetValue(key);

        try
        {
            return Convert.ToSingle(value);
        }
        catch (Exception)
        {
            return new Failure<float>($"$Unable to convert '{value}' to a single precision floating point number", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="Guid"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="Guid"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<Guid> GetGuid(string key)
    {
        var value = GetValue(key);

        try
        {
            return Guid.Parse(value);
        }
        catch (Exception)
        {
            return new Failure<Guid>($"$Unable to convert '{value}' to a GUID", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="bool"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="bool"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<bool> GetBoolean(string key)
    {
        var value = GetValue(key);

        switch (value.ToLowerInvariant())
        {
            case "true":
            case "on":
            case "yes":
            case "1":
                return true;
            case "false":
            case "off":
            case "no":
            case "0":
                return false;
        }

        return new Failure<bool>($"$Unable to convert '{value}' to a boolean", key, value);
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="DateTime"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="DateTime"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<DateTime> GetDateTime(string key)
    {
        var value = GetValue(key);

        try
        {
            return Convert.ToDateTime(value);
        }
        catch (Exception)
        {
            return new Failure<DateTime>($"$Unable to convert '{value}' to a date and time", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <c>DateOnly</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="DateOnly"/> value of the key in the data source.</returns>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<DateOnly> GetDate(string key)
    {
        var value = GetValue(key);

        try
        {
            return DateOnly.Parse(value);
        }
        catch (Exception)
        {
            return new Failure<DateOnly>($"$Unable to convert '{value}' to a date", key, value);
        }
    }

    /// <summary>
    /// Tries to get a value from the validated TrueText validation results source, and tries to convert it to a <see cref="TimeOnly"/>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>A <see cref="Result{A}"/> containing the value is successful; or a <see cref="Failure{T}"/> with an error message</returns>
    public Result<TimeOnly> GetTime(string key)
    {
        var value = GetValue(key);

        try
        {
            return TimeOnly.Parse(value);
        }
        catch (Exception)
        {
            return new Failure<TimeOnly>($"$Unable to convert '{value}' to a time", key, value);
        }
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

    public Result<int> GetOptionalInt32(string key, int defaultValue = default)
    {
        var value = GetValue(key);

        if (string.IsNullOrWhiteSpace(value))
            return defaultValue;

        //! TODO Refactor to have c single place where this conversion is done, internally.
        return GetInt32(key);
    }
}

// Result type

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
    /// <param name="fail">The function to execute if the <see cref="Result{A}"/> is a <see cref="Failure{T}"/></param>
    /// <typeparam name="TResult">The return type of the method</typeparam>
    /// <returns>An instance of <typeparam name="TResult"></typeparam></returns>
    public TResult Match<TResult>(Func<TValue, TResult> ok, Func<string, string, string, TResult> fail)
    {
        return this switch
        {
            Ok<TValue>(var v) => ok(v),
            Failure<TValue>(var message, var key, var text) => fail(message, key, text),
            _ => throw new TrueTextException("Unknown Result type")
        };
    }

    /// <summary>
    /// Matches this <see cref="Result{A}"/> against the two supplied functions. 
    /// </summary>
    /// <param name="ok">The action to execute if the <see cref="Result{A}"/> is an <see cref="Ok{T}"/></param>
    /// <param name="fail">The action to execute if the <see cref="Result{A}"/> is a <see cref="Failure{T}"/></param>
    public void Match(Action<TValue> ok, Action<string, string, string> fail)
    {
        switch (this)
        {
            case Ok<TValue>(var v):
                ok(v);
                break;
            case Failure<TValue>(var message, var key, var text):
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
            Failure<TValue> failure => new Failure<TResult>(failure.Error, failure.Key, failure.Text),
            _ => throw new TrueTextException("Unknown result type!")
        };
    }

    /// <summary>
    /// Performs a function on the <see cref="Result{A}"/> if it is a <see cref="Ok{T}"/> which can return a <see cref="Failure{T}"/> 
    /// </summary>
    /// <param name="binder">The binding function</param>
    /// <typeparam name="TResult">The return type for the <see cref="Result{B}"/></typeparam>
    /// <returns>A <see cref="Result{B}"/></returns>
    public Result<TResult> Bind<TResult>(Func<TValue, Result<TResult>> binder)
    {
        return this switch
        {
            Ok<TValue> valid => binder(valid.Value),
            Failure<TValue> failure => new Failure<TResult>(failure.Error, failure.Key, failure.Text),
            _ => throw new TrueTextException("Unknown result type!")
        };
    }

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
public sealed record Failure<TValue>(string Error, string Key, string Text) : Result<TValue>;

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
            : new Failure<TValue>("Unmatched predicate in where clause", string.Empty, string.Empty);
}