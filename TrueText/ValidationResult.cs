namespace TrueText;

/// <summary>
/// An abstract base record type for the <see cref="Valid"/> and <see cref="InValid"/> record types 
/// </summary>
/// <param name="Text">The text that was being validated</param>
/// <param name="IsValid">An indicator of whether the result is valid or not</param>
/// <typeparam name="T">The type of the Value property of a <see cref="Valid"/> result.</typeparam>
public abstract record ValidationResult(string Text, bool IsValid)
{
    /// <summary>
    /// Returns current <see cref="ValidationResult"/> as an <see cref="Invalid"/> instance.
    /// </summary>
    /// <returns>An <see cref="Invalid"/> instance; or <code>null</code>.</returns>
    public Invalid AsInvalid() => (Invalid)this;

    /// <summary>
    /// Returns current <see cref="ValidationResult"/> as an <see cref="Valid"/> instance.
    /// </summary>
    /// <returns>A <see cref="Valid"/> instance; or <code>null</code>.</returns>
    public Valid AsValid() => (Valid)this;

    /// <summary>
    /// Overrides the `+` operator to combine <see cref="ValidationResult"/>.
    /// </summary>
    /// <param name="lhs">The left hand side <see cref="ValidationResult"/></param>
    /// <param name="rhs">The right hand side <see cref="ValidationResult"/></param>
    /// <returns>A new, combined <see cref="ValidationResult"/></returns>
    public static ValidationResult operator +(ValidationResult lhs, ValidationResult rhs)
    {
        if (lhs.IsValid && rhs.IsValid)
            return rhs;
        if (lhs.IsValid && !rhs.IsValid)
            return rhs;
        if (!lhs.IsValid && rhs.IsValid)
            return lhs;

        var errors = new List<string>();
        errors.AddRange(((lhs as Invalid)!).Errors);
        errors.AddRange(((rhs as Invalid)!).Errors);
        return new Invalid(rhs.Text, errors.ToArray());
    }


    /// <summary>
    /// Provides a matching function that returns a <typeparam name="T"></typeparam>
    /// </summary>
    /// <param name="validFunc">The <see cref="Func{TResult}"/> function to fire when this <see cref="ValidationResult"/> is <see cref="Valid"/>.</param>
    /// <param name="invalidFunc">The <see cref="Func{TResult}"/> function to fire when this <see cref="ValidationResult"/> is <see cref="Invalid"/>.</param>
    public TResult Match<TResult>(Func<Valid, TResult> validFunc, Func<Invalid, TResult> invalidFunc)
    {
        if (this.IsValid)
            return validFunc(this.AsValid());

        return invalidFunc(this.AsInvalid());
    }

    /// <summary>
    /// Maps the Value of a <see cref="Valid"/> <see cref="ValidationResult"/> to a new <see cref="Valid"/> 
    /// </summary>
    /// <param name="fn">The <see cref="Func{T, TResult}">"></see> to transform the Value of a <see cref="Valid"/></param>
    /// <returns>A <see cref="ValidationResult"/> instance</returns>
    public ValidationResult Map(Func<string, string> fn) => this.IsValid
        ? new Valid(fn(this.AsValid().Value), this.Text)
        : new Invalid(this.Text, this.AsInvalid().Errors);

    /// <summary>
    /// Filters the <see cref="ValidationResult"/>, if it is a <see cref="Valid"/> result then the Value is
    /// checked against the <param name="predicate"></param> and passed through if successful; or replaced with an <see cref="InValid"/>
    /// using the supplied <paramref name="message"/>.
    /// </summary>
    /// <param name="predicate">A predicate function to validate the <see cref="ValidationResult"/> agains</param>
    /// <param name="message">The message to use in case the <param name="predicate"> fails</param></param>
    /// <returns>A <see cref="ValidationResult"/> instance</returns>
    public ValidationResult Filter(Predicate<string> predicate, string message)
    {
        if (this.IsValid)
        {
            return predicate(this.AsValid().Value)
                ? this
                : new Invalid(this.Text, new[] { message });
        }

        return this;
    }

    /// <summary>
    /// Reduces the <see cref="ValidationResult"/> to its value
    /// (Also sometimes called OrElse) 
    /// </summary>
    /// <param name="orElse">The value to supply if this is an <see cref="Invalid"/> result.</param>
    /// <returns>A <typeparam name="T"></typeparam> value</returns>
    public string Reduce(string orElse = default) => this.IsValid ? this.AsValid().Value : orElse;

    /// <summary>
    /// Reduces the <see cref="ValidationResult"/> to its value
    /// (Also sometimes called OrElse) 
    /// </summary>
    /// <param name="orElse">An <see cref="Func{T}"/> that produces a value to supply if this is an <see cref="Invalid"/> result.</param>
    /// <returns>A <typeparam name="T"></typeparam> value</returns>
    public string Reduce(Func<string> orElse) => this.IsValid ? this.AsValid().Value : orElse();

    /// <summary>
    /// Factory method that returns a validFunc <see cref="ValidationResult"/> instance. 
    /// </summary>
    /// <param name="text">The Current value of the text to validate</param>
    /// <returns>A <see cref="ValidationResult"/> instance.</returns>
    public static ValidationResult Valid(string text) =>
        new Valid(text, text);

    /// <summary>
    /// Factory method to create an invalidFunc <see cref="ValidationResult"/> instance.
    /// </summary>
    /// <param name="message">the error messages for the validation</param>
    /// <param name="text">The Current value opf the text</param>
    /// <returns>A <see cref="ValidationResult"/> instance.</returns>
    public static ValidationResult Invalid(string text, string message) =>
        new Invalid(text, new[] { message });

    /// <summary>
    /// Creates a new <see cref="Valid"/> <see cref="ValidationResult"/> from the given input 
    /// </summary>
    /// <param name="text">The text to lift into the <see cref="ValidationResult"/></param>
    /// <returns>A <see cref="ValidationResult"/> instance.</returns>
    public static ValidationResult Pure(string text) => Valid(text);
}

/// <summary>
/// Represents a generic, valid result of a validation
/// </summary>
/// <param name="Value">The valid value</param>
/// <param name="Text">The input text to the <see cref="Valid"/> result.</param>
/// <typeparam name="T">The type of the valid <see cref="ValidationResult"/></typeparam>
public sealed record Valid(string Value, string Text) : ValidationResult(Text, true);

/// <summary>
/// Represents a generic, invalid result of a validation
/// </summary>
/// <param name="Text">The input text to the <see cref="Valid"/> result.</param>
/// <typeparam name="T">The type of the valid <see cref="ValidationResult"/></typeparam>
public sealed record Invalid(string Text, string[] Errors) : ValidationResult(Text, false);
