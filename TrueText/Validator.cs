namespace TrueText;

using System.Text;
using System.Text.RegularExpressions;
using Result = ValidationResult<string>;

public sealed class Validator
{
    // Fields
    private readonly Func<string, Result> _func;

    /// <summary>
    /// Creates a new Validator
    /// </summary>
    /// <param name="func">The Validator <see cref="Func{String, Result}"/></param>
    public Validator(Func<string, Result> func)
    {
        this._func = func;
    }

    /// <summary>
    /// Applies the Validator function to the supplied value, returning a <see cref="ValidationResult{T}"/>
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

    /// <returns>A new <see cref="Validator"/> combining the two Validators</returns>
    /// <summary>
    /// Operator for alternatively combining validators
    /// (Operator for the <see cref="OrThen"/> method)
    /// </summary>
    /// <param name="lhs">The left hand side <see cref="Validator"/></param>
    /// <param name="rhs">The right hand side <see cref="Validator"/></param>
    /// <returns>A new <see cref="Validator"/> combining the two Validators</returns>
    public static Validator operator |(Validator lhs, Validator rhs) => lhs.OrThen(rhs);

    public static bool operator true(Validator validator) => true;

    public static bool operator false(Validator validator) => false;

    /*
     * 
     * Factory Validator transformation methods
     * 
     */

    /// <summary>
    /// Creates a transforming validator that trims whitespace from the beginning and end of input. 
    /// </summary>
    public static Validator Trim()
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input.Trim();
            return Result.Pure(input);
        });
    }

    /// <summary>
    /// Creates an extraction validator that extracts numbers from an input string
    /// </summary>
    /// <returns>An <see cref="Validator"/> instance</returns>
    public static Validator ExtractDigits()
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input.Trim();
            var sb = new StringBuilder(input.Length);

            foreach (var c in input)
            {
                if (char.IsDigit(c))
                    sb.Append(c);
                else if (c == '.')
                    sb.Append('.');
            }

            var numberString = sb.ToString();
            if (numberString.StartsWith('.') || numberString.EndsWith('.') || numberString.Count(c => c == '.') > 1)
                return Result.Invalid($"'{sb.ToString()}' is not a valid number", input);

            return Result.Pure(sb.ToString());
        });
    }

    /*
     * 
     * Factory Validator creation methods
     * 
     */

    /// <summary>
    /// Creates an validator that does not fail if no value was supplied but uses the supplied Validators on any value present
    /// </summary>
    /// <param name="validators"></param>
    /// <returns>An <see cref="Validator"/> instance.</returns>
    public static Validator Optional(params Validator[] validators)
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

        return optional.OrThen(validators.Aggregate((acc, next) => acc.AndThen(next)));
    }


    /// <summary>
    /// Creates a required input validator that treats whitespace as an <b>invalid</b> input.
    /// </summary>
    /// <param name="validators">An array of other Validators to process once a required value has been found.</param>
    /// <returns>An <see cref="Validator"/> instance.</returns>
    public static Validator Required(params Validator[] validators)
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

        return validators.Aggregate(required, (acc, next) => acc.AndThen(next));
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


    /// <summary>
    /// Creates an <see cref="Validator"/> instance that checks that the input only comprises of only numbers (ignoring whitespace).
    /// </summary>
    /// <returns>An <see cref="Validator"/> instance</returns>
    public static Validator IsInteger()
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input.Trim();
            var sb = new StringBuilder(input.Length);

            foreach (var c in input)
            {
                if (char.IsDigit(c))
                    sb.Append(c);
                else if (char.IsWhiteSpace(c))
                    continue;
                else
                    return Result.Invalid("The input should only contain numbers", input);
            }

            return Result.Valid(sb.ToString());
        });
    }

    /// <summary>
    /// Creates an <see cref="Validator"/> instance that checks that the input only comprises of only numbers with an decimal point (ignoring whitespace).
    /// </summary>
    /// <returns>An <see cref="Validator"/> instance</returns>
    public static Validator IsDecimal()
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input.Trim();

            var sb = new StringBuilder(input.Length);
            var decimalPointCount = 0;

            foreach (var c in input)
            {
                if (char.IsDigit(c))
                    sb.Append(c);
                else if (c == '.')
                {
                    decimalPointCount += 1;
                    sb.Append('.');
                }
                else if (char.IsWhiteSpace(c))
                    continue;
                else
                    return Result.Invalid($"'{input}' is not a valid decimal number", input);
            }

            if (sb.Length == 0)
                return Result.Invalid($"'{input}' is not a valid decimal number", input);
            if (decimalPointCount > 1)
                return Result.Invalid($"'{input}' has more than one decimal pint in it", input);

            return Result.Valid(sb.ToString());
        });
    }

    /// <summary>
    /// Creates a <see cref="System.Text.RegularExpressions.Regex"/> pattern validator.
    /// </summary>
    /// <returns>An <see cref="Validator"/> instance</returns>
    public static Validator Regex(string pattern)
    {
        var regex = new Lazy<Regex>(() => new Regex(pattern,
            RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase));

        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input.Trim();
            if (regex.Value.IsMatch(input))
                return Result.Valid(input.ToLowerInvariant());

            return Result.Invalid($"'{input}' is not a valid email address", input);
        });
    }

    /// <summary>
    /// Creates an email validator
    /// </summary>
    /// <returns>An <see cref="Validator"/> instance</returns>
    public static Validator IsEmail()
    {
        return Regex(@"^([\w\.\-_]+)?\w+@[\w-_]+(\.\w+){1,}$");
    }

    /// <summary>
    /// Creates a password validator
    /// </summary>
    /// <param name="policy">The <see cref="PasswordPolicy"/> instance to use to validate a password.</param>
    /// <returns>An <see cref="Validator"/> instance</returns>
    public static Validator Password(PasswordPolicy policy)
    {
        return new Validator(input =>
        {
            input = input ?? string.Empty;

            if (policy.MaximumLength < input.Length)
                return Result.Invalid("The password is too long", input);

            if (policy.MinimumLength > input.Length)
                return Result.Invalid("The password is too short", input);

            var upperCaseCount = 0;
            var lowerCaseCount = 0;
            var symbolCount = 0;
            var digitCount = 0;
            var hasSpace = false;
            foreach (var c in input)
            {
                if (char.IsLower(c))
                    lowerCaseCount += 1;
                if (char.IsUpper(c))
                    upperCaseCount += 1;
                if (policy.ListOfAcceptedSymbols.Contains(c))
                    symbolCount += 1;
                if (c == ' ')
                    hasSpace = true;
                if (char.IsDigit(c))
                    digitCount += 1;
            }

            var result = Result.Valid(input);
            if (!policy.IsSpaceAllowed && hasSpace)
                result += Result.Invalid("The password may not contain spaces", input);
            if (policy.RequiredNumberOfLowerCaseLetters > lowerCaseCount)
                result += Result.Invalid(
                    $"There must be at least {policy.RequiredNumberOfLowerCaseLetters} lower case letters", input);
            if (policy.RequiredNumberOfUpperCaseLetters > upperCaseCount)
                result += Result.Invalid(
                    $"There must be at least {policy.RequiredNumberOfUpperCaseLetters} upper case letters", input);
            if (policy.RequiredNumberOfDigits > digitCount)
                result += Result.Invalid(
                    $"There must be at least {policy.RequiredNumberOfDigits} digits (number characters)", input);
            if (policy.RequiredNumberOfSymbols > symbolCount)
                result += Result.Invalid(
                    $"There must be at least {policy.RequiredNumberOfSymbols} symbol characters (${policy.ListOfAcceptedSymbols})",
                    input);

            return result;
        });
    }
}