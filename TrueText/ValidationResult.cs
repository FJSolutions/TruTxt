namespace TrueText;

/// <summary>
/// An abstract base record type for the <see cref="Valid{T}"/> and <see cref="Invalid{T}"/> record types 
/// </summary>
/// <param name="Text">The text that was being validated</param>
/// <param name="IsValid">An indicator of whether the result is valid or not</param>
/// <typeparam name="T">The type of the Value property of a <see cref="Valid{T}"/> result.</typeparam>
public abstract record ValidationResult<T>(string Text, bool IsValid)
{
    /// <summary>
    /// Returns current <see cref="ValidationResult{T}"/> as an <see cref="Invalid"/> instance.
    /// </summary>
    /// <returns>An <see cref="Invalid"/> instance; or <code>null</code>.</returns>
    public Invalid<T> AsInvalid() => (Invalid<T>)this;

    /// <summary>
    /// Returns current <see cref="ValidationResult{T}"/> as an <see cref="Valid"/> instance.
    /// </summary>
    /// <returns>A <see cref="Valid"/> instance; or <code>null</code>.</returns>
    public Valid<T> AsValid() => (Valid<T>)this;

    /// <summary>
    /// Overrides the `+` operator to combine <see cref="ValidationResult{T}"/>.
    /// </summary>
    /// <param name="lhs">The left hand side <see cref="ValidationResult{T}"/></param>
    /// <param name="rhs">The right hand side <see cref="ValidationResult{T}"/></param>
    /// <returns>A new, combined <see cref="ValidationResult{T}"/></returns>
    public static ValidationResult<T> operator +(ValidationResult<T> lhs, ValidationResult<T> rhs)
    {
        if (lhs.IsValid && rhs.IsValid)
            return rhs;
        if (lhs.IsValid && !rhs.IsValid)
            return rhs;
        if (!lhs.IsValid && rhs.IsValid)
            return lhs;

        var errors = new List<string>();
        errors.AddRange(((lhs as Invalid<T>)!).Errors);
        errors.AddRange(((rhs as Invalid<T>)!).Errors);
        return new Invalid<T>(rhs.Text, errors.ToArray());
    }


    /// <summary>
    /// Provides a matching function that returns a <typeparam name="T"></typeparam>
    /// </summary>
    /// <param name="validFunc">The <see cref="Func{TResult}"/> function to fire when this <see cref="ValidationResult{T}"/> is <see cref="Valid"/>.</param>
    /// <param name="invalidFunc">The <see cref="Func{TResult}"/> function to fire when this <see cref="ValidationResult{T}"/> is <see cref="Invalid"/>.</param>
    public TResult Match<TResult>(Func<Valid<T>, TResult> validFunc, Func<Invalid<T>, TResult> invalidFunc)
    {
        if (this.IsValid)
            return validFunc(this.AsValid());

        return invalidFunc(this.AsInvalid());
    }

    /// <summary>
    /// Maps the Value of a <see cref="Valid{T}"/> <see cref="ValidationResult{T}"/> to a new <see cref="Valid{TResult}"/> 
    /// </summary>
    /// <param name="fn">The <see cref="Func{T, TResult}">"></see> to transform the Value of a <see cref="Valid{T}"/></param>
    /// <typeparam name="TResult">The type of the mapped return value</typeparam>
    /// <returns>A <see cref="ValidationResult{T}"/> instance</returns>
    public ValidationResult<TResult> Map<TResult>(Func<T, TResult> fn) => this.IsValid
        ? new Valid<TResult>(fn(this.AsValid().Value), this.Text)
        : new Invalid<TResult>(this.Text, this.AsInvalid().Errors);

    /// <summary>
    /// Filters the <see cref="ValidationResult{T}"/>, if it is a <see cref="Valid{T}"/> result then the Value is
    /// checked against the <param name="predicate"></param> and passed through if successful; or replaced with an <see cref="Invalid{T}"/>
    /// using the supplied <paramref name="message"/>.
    /// </summary>
    /// <param name="predicate">A predicate function to validate the <see cref="ValidationResult{T}"/> agains</param>
    /// <param name="message">The message to use in case the <param name="predicate"> fails</param></param>
    /// <returns>A <see cref="ValidationResult{T}"/> instance</returns>
    public ValidationResult<T> Filter(Predicate<T> predicate, string message)
    {
        if (this.IsValid)
        {
            return predicate(this.AsValid().Value)
                ? this
                : new Invalid<T>(this.Text, new[] { message });
        }

        return this;
    }

    /// <summary>
    /// Reduces the <see cref="ValidationResult{T}"/> to its value
    /// (Also sometimes called OrElse) 
    /// </summary>
    /// <param name="orElse">The value to supply if this is an <see cref="Invalid"/> result.</param>
    /// <returns>A <typeparam name="T"></typeparam> value</returns>
    public T Reduce(T orElse) => this.IsValid ? this.AsValid().Value : orElse;

    /// <summary>
    /// Reduces the <see cref="ValidationResult{T}"/> to its value
    /// (Also sometimes called OrElse) 
    /// </summary>
    /// <param name="orElse">An <see cref="Func{T}"/> that produces a value to supply if this is an <see cref="Invalid"/> result.</param>
    /// <returns>A <typeparam name="T"></typeparam> value</returns>
    public T Reduce(Func<T> orElse) => this.IsValid ? this.AsValid().Value : orElse();

    /// <summary>
    /// Factory method that returns a validFunc <see cref="ValidationResult{T}"/> instance. 
    /// </summary>
    /// <param name="text">The Current value of the text to validate</param>
    /// <returns>A <see cref="ValidationResult{T}"/> instance.</returns>
    public static ValidationResult<string> Valid(string text) =>
        new Valid<string>(text, text);

    /// <summary>
    /// Factory method to create an invalidFunc <see cref="ValidationResult{String}"/> instance.
    /// </summary>
    /// <param name="message">the error messages for the validation</param>
    /// <param name="text">The Current value opf the text</param>
    /// <returns>A <see cref="ValidationResult{String}"/> instance.</returns>
    public static ValidationResult<string> Invalid(string text, string message) =>
        new Invalid<string>(text, new[] { message });

    /// <summary>
    /// Creates a new <see cref="Valid"/> <see cref="ValidationResult{T}"/> from the given input 
    /// </summary>
    /// <param name="text">The text to lift into the <see cref="ValidationResult{T}"/></param>
    /// <returns>A <see cref="ValidationResult{String}"/> instance.</returns>
    public static ValidationResult<string> Pure(string text) => Valid(text);
}

/// <summary>
/// Represents a generic, valid result of a validation
/// </summary>
/// <param name="Value">The valid value</param>
/// <param name="Text">The input text to the <see cref="Valid{T}"/> result.</param>
/// <typeparam name="T">The type of the valid <see cref="ValidationResult{T}"/></typeparam>
public sealed record Valid<T>(T Value, string Text) : ValidationResult<T>(Text, true);

/// <summary>
/// Represents a generic, invalid result of a validation
/// </summary>
/// <param name="Text">The input text to the <see cref="Valid{T}"/> result.</param>
/// <typeparam name="T">The type of the valid <see cref="ValidationResult{T}"/></typeparam>
public sealed record Invalid<T>(string Text, string[] Errors) : ValidationResult<T>(Text, false);

/// <summary>
/// Static class that contains extension methods for <see cref="ValidationResult{T}"/>s.
/// </summary>
public static class ValidationResultExtensions
{
    /// <summary>
    /// Validates transforming a <see cref="ValidationResult{T}"/> into an <see cref="int"/>. 
    /// </summary>
    /// <param name="result">The source <see cref="ValidationResult{String}"/> to chain to.</param>
    /// <returns>A <see cref="ValidationResult{Int32}"/></returns>
    public static ValidationResult<int> AsInt32(this ValidationResult<string> result)
    {
        if (result.IsValid)
        {
            if (int.TryParse(result.Text, out var i))
                return new Valid<int>(i, result.Text);
            return new Invalid<int>(result.Text, new[] { "Unable to parse value as an integer" });
        }

        return new Invalid<int>(result.Text, result.AsInvalid().Errors);
    }
}