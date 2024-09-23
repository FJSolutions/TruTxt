namespace TruTxt;

using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// A data structure that represents the presence ort absence of a valuer 
/// </summary>
/// <typeparam name="TValue">The type of the value that maybe present</typeparam>
public abstract record Option<TValue>
{
    /// <summary>
    /// The constructor is private to prevent any other subtypes other than Some and None 
    /// </summary>
    private protected Option()
    {
    }

    public static implicit operator Option<TValue>(No no) => new None<TValue>();

    /// <summary>
    /// Creates a <see cref="None"/> instance, typed as an <see cref="Option{TValue}"/>
    /// </summary>
    /// <returns>A <see cref="None"/> <see cref="Option{TValue}"/></returns>
    public static Option<TValue> None() => new None<TValue>();

    /// <summary>
    /// Creates a <see cref="Some"/> instance with the supplied value, typed as an <see cref="Option{TValue}"/>
    /// </summary>
    /// <param name="value">The value of the <see cref="Some"/></param>
    /// <returns>A <see cref="Some"/> <see cref="Option{TValue}"/></returns>
    public static Option<TValue> Some([NotNull] TValue value) => new Some<TValue>(value);
}

/// <summary>
/// Represents an <see cref="Option{TValue}"/> that contains some value
/// </summary>
/// <param name="Value">The contained value</param>
/// <typeparam name="TValue">The type of the contained value</typeparam>
internal sealed record Some<TValue>([NotNull] TValue Value) : Option<TValue>;

/// <summary>
/// Represents an <see cref="Option{TValue}"/> that contains no value
/// </summary>
/// <typeparam name="TValue">The type of the value that is absent</typeparam>
internal sealed record None<TValue> : Option<TValue>;

/// <summary>
/// A helper class for returning a value that is implicitly converted to a strongly types <see cref="None{TValue}"/> 
/// </summary>
public sealed record No
{
    /// <summary>
    /// A private constructor to prevent external instantiation
    /// </summary>
    private No()
    {
    }

    /// <summary>
    /// Gets a no-value instance that can be implicitly converted to a strongly types <see cref="None{TValue}"/>
    /// </summary>
    public static No Value { get; } = new();
}

public static class OptionExtensions
{
    [Pure]
    public static TResult Match<TValue, TResult>(this Option<TValue> option, Func<TValue, TResult> some,
        Func<TResult> none)
    {
        return option switch
        {
            Some<TValue> s => some(s.Value),
            None<TValue> _ => none(),
            _ => throw new TruTxtException("Unknown Option type")
        };
    }


    /// <summary>
    /// Maps the value contained in this <see cref="Option{TValue}"/>, if present, using the <param name="mapper"></param> function
    /// </summary>
    /// <param name="option"></param>
    /// <param name="mapper">The function to use to transform the value</param>
    /// <typeparam name="TResult">The return value of the <see cref="Option{TValue}"/></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns>An instance of a <see cref="Option{TValue}"/></returns>
    [Pure]
    public static Option<TResult> Map<TValue, TResult>(this Option<TValue> option, Func<TValue, TResult> mapper)
    {
        return option switch
        {
            Some<TValue> s => Option<TResult>.Some(mapper(s.Value)),
            None<TValue> _ => No.Value,
            _ => throw new TruTxtException("Unknown Option type")
        };
    }

    /// <summary>
    /// Performs a transformation on the value of this <see cref="Option{TValue}"/>, if one is present, using the supplied
    /// <param name="binder"></param> function.
    /// </summary>
    /// <param name="option"></param>
    /// <param name="binder">A function that will transform the source value</param>
    /// <typeparam name="TResult">The type of the result of the transformation</typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns>An <see cref="Option{TValue}"/></returns>
    [Pure]
    public static Option<TResult> Bind<TValue, TResult>(this Option<TValue> option,
        Func<TValue, Option<TResult>> binder)
    {
        return option switch
        {
            Some<TValue> s => binder(s.Value),
            None<TValue> _ => No.Value,
            _ => throw new TruTxtException("Unknown Option type")
        };
    }

    /// <summary>
    /// Reduces an <see cref="Option{TValue}"/> to a value. 
    /// </summary>
    /// <param name="option"></param>
    /// <param name="defaultValue">The value to return if the <see cref="Option{TValue}"/> is a
    /// <see cref="None{TValue}"/></param>
    /// <returns>A <typeparam name="TValue"> value</typeparam></returns>
    /// <exception cref="TruTxtException"></exception>
    [Pure]
    public static TValue Reduce<TValue>(this Option<TValue> option, [NotNull] TValue defaultValue)
    {
        return option switch
        {
            Some<TValue> s => s.Value,
            None<TValue> _ => defaultValue,
            _ => throw new TruTxtException("Unknown Option type")
        };
    }

    /// <summary>
    /// Reduces an <see cref="Option{TValue}"/> to a value. 
    /// </summary>
    /// <param name="option"></param>
    /// <param name="defaultValue">A function that returns a value if the <see cref="Option{TValue}"/> is a
    /// <see cref="None{TValue}"/></param>
    /// <returns>A <typeparam name="TValue"> value</typeparam></returns>
    /// <exception cref="TruTxtException"></exception>
    [Pure]
    public static TValue Reduce<TValue>(this Option<TValue> option, Func<TValue> defaultValue)
    {
        return option switch
        {
            Some<TValue> s => s.Value,
            None<TValue> _ => defaultValue(),
            _ => throw new TruTxtException("Unknown Option type")
        };
    }
}