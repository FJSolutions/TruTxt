namespace TrueText;

/// <summary>
/// A data structure that represents the presence ort absence of a valuer 
/// </summary>
/// <typeparam name="TValue">The type of the value that maybe present</typeparam>
public abstract record Option<TValue>
{
    /// <summary>
    /// The constructor is private to prevent any other sub-types other than Some and None 
    /// </summary>
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

    /// <summary>
    /// Maps the value contained in this <see cref="Option{TValue}"/>, if present, using the <param name="mapper"></param> function
    /// </summary>
    /// <param name="mapper">The function to use to transform the value</param>
    /// <typeparam name="TResult">The return value of the <see cref="Option{TValue}"/></typeparam>
    /// <returns>An instance of a <see cref="Option{TValue}"/></returns>
    public Option<TResult> Map<TResult>(Func<TValue, TResult> mapper)
    {
        return this switch
        {
            Some<TValue> s => Option<TResult>.Some(mapper(s.Value)),
            None<TValue> _ => No.Value,
            _ => throw new TrueTextException("Unknown Option type")
        };
    }

    /// <summary>
    /// Performs a transformation on the value of this <see cref="Option{TValue}"/>, if one is present, using the supplied
    /// <param name="binder"></param> function.
    /// </summary>
    /// <param name="binder">A function that will transform the source value</param>
    /// <typeparam name="TResult">The type of the result of the transformation</typeparam>
    /// <returns>An <see cref="Option{TValue}"/></returns>
    public Option<TResult> Bind<TResult>(Func<TValue, Option<TResult>> binder)
    {
        return this switch
        {
            Some<TValue> s => binder(s.Value),
            None<TValue> _ => No.Value,
            _ => throw new TrueTextException("Unknown Option type")
        };
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
    public static Option<TValue> Some(TValue value) => new Some<TValue>(value);
}

/// <summary>
/// Represents an <see cref="Option{TValue}"/> that contains some value
/// </summary>
/// <param name="Value">The contained value</param>
/// <typeparam name="TValue">The type of the contained value</typeparam>
internal sealed record Some<TValue>(TValue Value) : Option<TValue>;

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
    public static No Value { get; } = new No();
}