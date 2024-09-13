namespace TrueText;

public static class TrueParser
{
    /// <summary>
    /// Parses a <see cref="String"/> value as a <see cref="string"/>
    /// </summary>
    /// <param name="value">The value to parse as a <see cref="String"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<string> ParseString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new Some<string>(string.Empty);

        return new Some<string>(value);
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="sbyte"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="sbyte"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<sbyte> ParseInt8(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (sbyte.TryParse(value, out var val))
                return new Some<sbyte>(val);
        }

        return new None<sbyte>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="short"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="short"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<short> ParseInt16(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (short.TryParse(value, out var val))
                return new Some<short>(val);
        }

        return new None<short>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="int"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="int"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<int> ParseInt32(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (int.TryParse(value, out var val))
                return new Some<int>(val);
        }

        return new None<int>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="long"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="long"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<long> ParseInt64(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (long.TryParse(value, out var val))
                return new Some<long>(val);
        }

        return new None<long>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="byte"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="byte"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<byte> ParseUInt8(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (byte.TryParse(value, out var val))
                return new Some<byte>(val);
        }

        return new None<byte>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="ushort"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="ushort"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<ushort> ParseUInt16(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (ushort.TryParse(value, out var val))
                return new Some<ushort>(val);
        }

        return new None<ushort>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="uint"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="uint"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<uint> ParseUInt32(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (uint.TryParse(value, out var val))
                return new Some<uint>(val);
        }

        return new None<uint>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="ulong"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="ulong"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<ulong> ParseUInt64(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (ulong.TryParse(value, out var val))
                return new Some<ulong>(val);
        }

        return new None<ulong>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="float"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="float"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<float> ParseSingle(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (float.TryParse(value, out var val))
                return new Some<float>(val);
        }

        return new None<float>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="double"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="double"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<double> ParseDouble(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (double.TryParse(value, out var val))
                return new Some<double>(val);
        }

        return new None<double>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="decimal"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="decimal"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<decimal> ParseDecimal(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (decimal.TryParse(value, out var val))
                return new Some<decimal>(val);
        }

        return new None<decimal>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="Guid"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="Guid"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<Guid> ParseGuid(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (Guid.TryParse(value, out var val))
                return new Some<Guid>(val);
        }

        return new None<Guid>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="DateTime"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="DateTime"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<DateTime> ParseDateTime(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (DateTime.TryParse(value, out var val))
                return new Some<DateTime>(val);
        }

        return new None<DateTime>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="DateOnly"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="DateOnly"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<DateOnly> ParseDate(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (DateOnly.TryParse(value, out var val))
                return new Some<DateOnly>(val);
        }

        return new None<DateOnly>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="TimeOnly"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="TimeOnly"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<TimeOnly> ParseTime(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (TimeOnly.TryParse(value, out var val))
                return new Some<TimeOnly>(val);
        }

        return new None<TimeOnly>();
    }

    /// <summary>
    /// Parses a <see cref="String"/> value as an <see cref="Boolean"/>
    /// </summary>
    /// <param name="value">The value to parse as an <see cref="Boolean"/></param>
    /// <returns>A <see cref="Option{TValue}"/></returns>
    public static Option<bool> ParseBool(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            if (bool.TryParse(value, out var val))
                return new Some<bool>(val);
            
            switch (value.ToLowerInvariant())
            {
                case "true":
                case "on":
                case "yes":
                case "1":
                    return new Some<bool>(true);
                case "false":
                case "off":
                case "no":
                case "0":
                    return new Some<bool>(false);
            }
        }

        return new None<bool>();
    }
}

public abstract record Option<TValue>
{
    private protected Option()
    {
    }

    public TResult Match<TResult>(Func<TValue, TResult> some, Func<TResult> none)
    {
        return this switch
        {
            Some<TValue> s => some(s.Value),
            None<TValue> _ => none(),
            _ => throw new TrueTextException("Unknown Option type")
        };
    }

    public Option<TResult> Map<TResult>(Func<TValue, TResult> mapper)
    {
        return this switch
        {
            Some<TValue> s => new Some<TResult>(mapper(s.Value)),
            None<TValue> _ => new None<TResult>(),
            _ => throw new TrueTextException("Unknown Option type")
        };
    }

    public Option<TResult> Bind<TResult>(Func<TValue, Option<TResult>> binder)
    {
        return this switch
        {
            Some<TValue> s => binder(s.Value),
            None<TValue> _ => new None<TResult>(),
            _ => throw new TrueTextException("Unknown Option type")
        };
    }
}

internal sealed record Some<T>(T Value) : Option<T>;

internal sealed record None<T> : Option<T>;