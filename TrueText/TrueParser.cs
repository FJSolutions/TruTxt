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
            return Option<string>.Some(string.Empty);

        return Option<string>.Some(value);
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
                return Option<sbyte>.Some(val);
        }

        return Option<sbyte>.None();
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
                return Option<short>.Some(val);
        }

        return Option<short>.None();
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
                return Option<int>.Some(val);
        }

        return Option<int>.None();
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
                return Option<long>.Some(val);
        }

        return Option<long>.None();
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
                return Option<byte>.Some(val);
        }

        return Option<byte>.None();
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
                return Option<ushort>.Some(val);
        }

        return Option<ushort>.None();
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
                return Option<uint>.Some(val);
        }

        return Option<uint>.None();
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
                return Option<ulong>.Some(val);
        }

        return Option<ulong>.None();
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
                return Option<float>.Some(val);
        }

        return Option<float>.None();
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
                return Option<double>.Some(val);
        }

        return Option<double>.None();
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
                return Option<decimal>.Some(val);
        }

        return Option<decimal>.None();
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
                return Option<Guid>.Some(val);
        }

        return Option<Guid>.None();
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
                return Option<DateTime>.Some(val);
        }

        return Option<DateTime>.None();
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
                return Option<DateOnly>.Some(val);
        }

        return Option<DateOnly>.None();
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
                return Option<TimeOnly>.Some(val);
        }

        return Option<TimeOnly>.None();
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