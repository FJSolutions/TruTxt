using System.Diagnostics;

namespace TrueText;

/// <summary>
/// A class that can read validated text and trues to convert it to strongly typed values
/// </summary>
public class TrueReader<T>
{
    private readonly Dictionary<T, string> _data;

    /// <summary>
    /// the constructor is internal so it can only be created from 
    /// </summary>
    /// <param name="data"></param>
    internal TrueReader(Dictionary<T, string> data)
    {
        this._data = data;
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and returns it as a <c>String</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The string value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public string GetString(T key)
    {
        if (this._data.TryGetValue(key, out var value))
            return value;

        throw new TrueTextException("The key could not be found in the reader");
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>Int64</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="long"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public long GetInt64(T key)
    {
        var value = GetString(key);

        try
        {
            return Convert.ToInt64(value);
        }
        catch (Exception ex)
        {
            throw new TrueTextException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>Int32</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="int"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public int GetInt32(T key)
    {
        var value = GetString(key);

        try
        {
            return Convert.ToInt32(value);
        }
        catch (Exception ex)
        {
            throw new TrueTextException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>Int16</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="short"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public short GetInt16(T key)
    {
        var value = GetString(key);

        try
        {
            return Convert.ToInt16(value);
        }
        catch (Exception ex)
        {
            throw new TrueTextException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>Byte</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="byte"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public byte GetUInt8(T key)
    {
        var value = GetString(key);

        try
        {
            return Convert.ToByte(value);
        }
        catch (Exception ex)
        {
            throw new TrueTextException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>UInt64</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="ulong"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public ulong GetUInt64(T key)
    {
        var value = GetString(key);

        try
        {
            return Convert.ToUInt64(value);
        }
        catch (Exception ex)
        {
            throw new TrueTextException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>UInt32</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="uint"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public uint GetUInt32(T key)
    {
        var value = GetString(key);

        try
        {
            return Convert.ToUInt32(value);
        }
        catch (Exception ex)
        {
            throw new TrueTextException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>UInt16</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="ushort"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public ushort GetUInt16(T key)
    {
        var value = GetString(key);

        try
        {
            return Convert.ToUInt16(value);
        }
        catch (Exception ex)
        {
            throw new TrueTextException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>SByte</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="SByte"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public sbyte GetInt8(T key)
    {
        var value = GetString(key);

        try
        {
            return Convert.ToSByte(value);
        }
        catch (Exception ex)
        {
            throw new TrueTextException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>Decimal</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="Decimal"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public decimal GetDecimal(T key)
    {
        var value = GetString(key);

        try
        {
            return Convert.ToDecimal(value);
        }
        catch (Exception ex)
        {
            throw new TrueTextException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>Double</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="Double"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public double GetDouble(T key)
    {
        var value = GetString(key);

        try
        {
            return Convert.ToDouble(value);
        }
        catch (Exception ex)
        {
            throw new TrueTextException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>Single</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="Single"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public float GetSingle(T key)
    {
        var value = GetString(key);

        try
        {
            return Convert.ToSingle(value);
        }
        catch (Exception ex)
        {
            throw new TrueTextException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>Guid</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="Guid"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public Guid GetGuid(T key)
    {
        var value = GetString(key);

        try
        {
            return Guid.Parse(value);
        }
        catch (Exception ex)
        {
            throw new TrueTextException(ex.Message, ex);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>Bool</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="bool"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public bool GetBoolean(T key)
    {
        var value = GetString(key);

        switch (value.ToLowerInvariant())
        {
            case "true":
            case "on":
            case "yes":
                return true;
            case "false":
            case "off":
            case "no":
                return false;
        }

        throw new TrueTextException($"$Unable to convert '{value}' to a boolean");
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>DateTime</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="DateTime"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public DateTime GetDateTime(T key)
    {
        var value = GetString(key);

        try
        {
            return Convert.ToDateTime(value);
        }
        catch (Exception e)
        {
            throw new TrueTextException($"$Unable to convert '{value}' to a date and time", e);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>DateOnly</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="DateOnly"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public DateOnly GetDate(T key)
    {
        var value = GetString(key);

        try
        {
            return DateOnly.Parse(value);
        }
        catch (Exception e)
        {
            throw new TrueTextException($"$Unable to convert '{value}' to a date and time", e);
        }
    }

    /// <summary>
    /// Gets a value from the validated TrueText validation results source, and tries to convert it to a <c>TimeOnly</c>.
    /// </summary>
    /// <param name="key">The key value to look-up in the data store</param>
    /// <returns>The <see cref="TimeOnly"/> value of the key in the data source.</returns>
    /// <exception cref="TrueTextException">If the key is not found in the data source an exception is thrown</exception>
    public TimeOnly GetTime(T key)
    {
        var value = GetString(key);

        try
        {
            return TimeOnly.Parse(value);
        }
        catch (Exception e)
        {
            throw new TrueTextException($"$Unable to convert '{value}' to a date and time", e);
        }
    }
}