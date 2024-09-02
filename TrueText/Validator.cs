namespace TrueText;

using Result = ValidationResult<string>;

public sealed class Validator
{
    // Fields
    private readonly Func<string, Result> _func;

    /// <summary>
    /// Creates a new Validator
    /// </summary>
    /// <param name="func">The validation <see cref="Func{String, Result}"/></param>
    public Validator(Func<string, Result> func)
    {
        this._func = func;
    }

    /// <summary>
    /// Applies the validation function to the supplied value, returning a <see cref="ValidationResult{T}"/>
    /// </summary>
    /// <param name="input">The <see cref="String"/> to validate</param>
    /// <returns>A <see cref="ValidationResult{T}"/> instance</returns>
    public Result Apply(string input)
    {
        return _func(input);
    }

    /// <summary>
    /// Combines <see cref="Validator"/>s, returning the combination of their results. 
    /// </summary>
    /// <param name="other">The other <see cref="Validator"/> to combine this one with</param>
    /// <returns>A new <see cref="Validator"/> instance</returns>
    public Validator AndThen(Validator other)
    {
        return new Validator(input =>
        {
            var result = this.Apply(input);
            if (!result.IsValid)
                return result;

            return result + other.Apply(input);
        });
    }

    /// <summary>
    /// Combines <see cref="Validator"/>s, returning the first <see cref="Valid{T}"/> result, if any. 
    /// </summary>
    /// <param name="other">The other <see cref="Validator"/> to combine this one with</param>
    /// <returns>A new <see cref="Validator"/> instance</returns>
    public Validator OrThen(Validator other)
    {
        return new Validator(input =>
        {
            var result = this.Apply(input);
            if (result.IsValid)
                return result;

            return other.Apply(input);
        });
    }

    /// <summary>
    /// Operator for additively combining validators 
    /// (Operator for the <see cref="AndThen"/> method)
    /// </summary>
    /// <param name="lhs">The left hand side <see cref="Validator"/></param>
    /// <param name="rhs">The right hand side <see cref="Validator"/></param>
    public static Validator operator &(Validator lhs, Validator rhs) => lhs.AndThen(rhs);

    /// <returns>A new <see cref="Validator"/> combining the two validations</returns>
    /// <summary>
    /// Operator for alternatively combining validators
    /// (Operator for the <see cref="OrThen"/> method)
    /// </summary>
    /// <param name="lhs">The left hand side <see cref="Validator"/></param>
    /// <param name="rhs">The right hand side <see cref="Validator"/></param>
    /// <returns>A new <see cref="Validator"/> combining the two validations</returns>
    public static Validator operator |(Validator lhs, Validator rhs) => lhs.OrThen(rhs);

    public static bool operator true(Validator validator) => true;

    public static bool operator false(Validator validator) => false;

    /*
     * Factory methods 
     */

    /// <summary>
    /// Creates an validator that does not fail if no value was supplied but uses the supplied validations on any value present
    /// </summary>
    /// <param name="validations"></param>
    /// <returns>An <see cref="Validator"/> instance.</returns>
    public static Validator Optional(params Validator[] validations)
    {
        var optional = new Validator(
            input =>
            {
                input = input?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(input))
                    return Result.Valid(input);

                return Result.Invalid(string.Empty, input);
            }
        );

        return optional.OrThen(validations.Aggregate((acc, next) => acc.AndThen(next)));
    }


    /// <summary>
    /// Creates a required input validator that treats whitespace as an <b>invalid</b> input.
    /// </summary>
    /// <param name="validations">An array of other validations to process once a required value has been found.</param>
    /// <returns>An <see cref="Validator"/> instance.</returns>
    public static Validator Required(params Validator[] validations)
    {
        var required = new Validator(
            input =>
            {
                if (string.IsNullOrWhiteSpace(input))
                    return Result.Invalid("The value cannot be null, empty, or just whitespace",
                        string.Empty);

                return Result.Valid(input);
            }
        );

        return validations.Aggregate(required, (acc, next) => acc.AndThen(next));
    }

    /// <summary>
    /// Creates an <see cref="Validator"/> instance that validates that an input is greater than or equal to the supplied <paramref name="length"/>.
    /// </summary>
    /// <param name="length">The minimum length of the input</param>
    /// <returns>An <see cref="Validator"/> instance.</returns>
    public static Validator Min(int length)
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input.Trim();
            if (input.Length >= length)
                return Result.Valid(input);

            return Result.Invalid($"The input is shorter than {length}", input);
        });
    }

    /// <summary>
    /// Creates an <see cref="Validator"/> instance that validates that an input is shorter than the supplied <paramref name="length"/>.
    /// </summary>
    /// <param name="length">The maximum length of the input</param>
    /// <returns>An <see cref="Validator"/> instance.</returns>
    public static Validator Max(int length)
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input.Trim();
            if (input.Length < length)
                return Result.Valid(input);

            return Result.Invalid($"The input must be sorter than {length}", input);
        });
    }

    /// <summary>
    /// Creates an <see cref="Validator"/> instance that validates that an input is between the supplied <paramref name="min"/> and <paramref name="max"/>.
    /// (Same as supplying <see cref="Min"/> and <see cref="Max"/> validators)
    /// </summary>
    /// <param name="min">The minimum length of the input</param>
    /// <param name="max">The maximum length of the input</param>
    /// <returns>An <see cref="Validator"/> instance.</returns>
    public static Validator Between(int min, int max)
    {
        return Min(min).AndThen(Max(max));
    }
}