using System.Net.Http.Headers;

namespace TrueText;

public abstract record ValidationResult<T>(string Text, bool IsValid)
{
    /// <summary>
    /// Returns current <see cref="ValidationResult{T}"/> as an <see cref="Invalid"/> instance.
    /// </summary>
    /// <returns>An <see cref="Invalid"/> instance; or <code>null</code>.</returns>
    public Invalid<T> AsInvalid()
    {
        return (Invalid<T>)this;
    }
    
    /// <summary>
    /// Returns current <see cref="ValidationResult{T}"/> as an <see cref="Valid"/> instance.
    /// </summary>
    /// <returns>A <see cref="Valid"/> instance; or <code>null</code>.</returns>
    public Valid<T> AsValid()
    {
        return (Valid<T>)this;
    }

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
    /// Factory method that returns a validFunc <see cref="ValidationResult{T}"/> instance. 
    /// </summary>
    /// <param name="text">The Current value of the text to validate</param>
    /// <returns>A <see cref="ValidationResult{T}"/> instance.</returns>
    public static ValidationResult<string> Valid(string text)
    {
        return new Valid<string>(text, text);
    }

    /// <summary>
    /// Factory method to create an invalidFunc <see cref="ValidationResult{String}"/> instance.
    /// </summary>
    /// <param name="message">the error messages for the validation</param>
    /// <param name="text">The Current value opf the text</param>
    /// <returns>A <see cref="ValidationResult{String}"/> instance.</returns>
    public static ValidationResult<string> Invalid(string text, string message)
    {
        return new Invalid<string>(text, new[] { message });
    }
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

public static class ValidationResulExtensions
{
    /// <summary>
    /// Validates transforming a <see cref="ValidationResult{T}"/> into an <code>int</code>. 
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static ValidationResult<int> AsInt32(this ValidationResult<string> result)
    {
        if (result.IsValid)
        {
            if (int.TryParse(result.Text, out var i))
                return new Valid<int>(i, result.Text);
            return new Invalid<int>( result.Text, new[] { "Unable to parse value as an integer" });
        }

        return new Invalid<int>( result.Text, result.AsInvalid().Errors);
    }
}