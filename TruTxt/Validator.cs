namespace TruTxt;

using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// A data-structure that contains the validation or transformation logic core to the library.
/// </summary>
public sealed class Validator
{
    // Static
    private static ConcurrentDictionary<string, Regex> _regexes;

    // Fields
    private readonly Func<string, ValidationResult> _func;

    /// <summary>
    /// Creates a new Validator`
    /// </summary>
    /// <param name="func">The Validator <see cref="Func{String, ValidationResult}"/></param>
    public Validator(Func<string, ValidationResult> func)
    {
        this._func = func;
    }

    /// <summary>
    /// Applies the Validator function to the supplied value, returning a <see cref="ValidationResult"/>
    /// </summary>
    /// <param name="input">The <see cref="String"/> to validate</param>
    /// <returns>A <see cref="ValidationResult"/> instance</returns>
    public ValidationResult Apply(string input)
    {
        return _func(input);
    }

    /// <summary>
    /// Combines <see cref="Validator"/>s, returning the combination of their ValidationResults. 
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
    /// Combines <see cref="Validator"/>s, returning the first <see cref="Valid"/> ValidationResult, if any. 
    /// </summary>
    /// <param name="other">The other <see cref="Validator"/> to combine this one with</param>
    /// <returns>A new <see cref="Validator"/> instance</returns>
    public Validator OrElse(Validator other)
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
    /// (Operator for the <see cref="OrElse"/> method)
    /// </summary>
    /// <param name="lhs">The left hand side <see cref="Validator"/></param>
    /// <param name="rhs">The right hand side <see cref="Validator"/></param>
    /// <returns>A new <see cref="Validator"/> combining the two Validators</returns>
    public static Validator operator |(Validator lhs, Validator rhs) => lhs.OrElse(rhs);

    /// <summary>
    /// The <c>true</c> operator is required to be overloaded for the logical && and || operators to be derived 
    /// </summary>
    /// <param name="validator"></param>
    /// <returns><c>true</c></returns>
    public static bool operator true(Validator validator) => true;

    /// <summary>
    /// The <c>false</c> operator is required to be overloaded for the logical && and || operators to be derived 
    /// </summary>
    /// <param name="validator"></param>
    /// <returns><c>false</c></returns>
    public static bool operator false(Validator validator) => false;

    /********************************************
     *
     * Private static members
     * 
     ********************************************/

    private static Regex GetRegexForPattern(string pattern)
    {
        _regexes ??= new ConcurrentDictionary<string, Regex>();

        return _regexes.GetOrAdd(pattern,
            (_) => new Regex(pattern, RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase));
    }

    /*********************************************
     * 
     * Factory Validator transformation methods
     * 
     *********************************************/

    /// <summary>
    /// Creates a transforming validator that trims whitespace from the beginning and end of input. 
    /// </summary>
    public static Validator Trim()
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input.Trim();
            return ValidationResult.Pure(input);
        });
    }

    /// <summary>
    /// Creates an extraction validator that extracts numbers from an input string
    /// </summary>
    /// <returns>An <see cref="Validator"/> instance</returns>
    public static Validator ExtractNumber(bool allowDecimals = true)
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input;
            var sb = new StringBuilder(input.Length);
            var noDecimalPoints = 0;
            var hasDigits = false;

            foreach (var c in input)
            {
                if (char.IsDigit(c))
                {
                    sb.Append(c);
                    hasDigits = true;
                }

                if (c == '.')
                {
                    sb.Append('.');
                    noDecimalPoints += 1;
                }
            }

            if (hasDigits)
            {
                if (!allowDecimals && noDecimalPoints > 0)
                    return ValidationResult.Invalid(input,
                        $"A whole number cannot be reliably extracted from '{input}'");

                if (noDecimalPoints > 1)
                    return ValidationResult.Invalid(input,
                        "There are too many decimal points in the extracted number!");

                if (sb.ToString()[0] == '.')
                    ValidationResult.Invalid(input, "An extracted number cannot end with a decimal point");

                return ValidationResult.Pure(sb.ToString());
            }

            return ValidationResult.Invalid(input, $"No number could be extracted from '{input}' ");
        });
    }

    /*************************************
     * 
     * Factory Validator creation methods
     * 
     *************************************/

    /// <summary>
    /// Creates an validator that does not fail if no value was supplied but uses the supplied Validators on any value present
    /// </summary>
    /// <param name="validators">An array of validators to process if there is a value to validate</param>
    /// <returns>An <see cref="Validator"/> instance.</returns>
    public static Validator Optional(params Validator[] validators)
    {
        var optional = new Validator(
            input =>
            {
                input = string.IsNullOrEmpty(input) ? string.Empty : input;

                if (string.IsNullOrWhiteSpace(input))
                    return ValidationResult.Valid(string.Empty);

                if (validators.Length == 0)
                    return ValidationResult.Valid(input);

                // Invalid because it has content and that content needs to be validated 
                return ValidationResult.Invalid(input, string.Empty);
            }
        );

        return validators.Length == 0
            ? optional
            : optional.OrElse(validators.Aggregate((acc, next) => acc.AndThen(next)));
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
                input = string.IsNullOrEmpty(input) ? string.Empty : input;
                if (string.IsNullOrWhiteSpace(input))
                    return ValidationResult.Invalid(string.Empty,
                        "The value cannot be null, empty, or just whitespace");

                return ValidationResult.Valid(input);
            }
        );

        return validators.Length == 0
            ? required
            : validators.Aggregate(required, (acc, next) => acc.AndThen(next));
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
            input = string.IsNullOrEmpty(input) ? string.Empty : input;
            if (input.Length >= length)
                return ValidationResult.Valid(input);

            return ValidationResult.Invalid(input, $"The input is shorter than {length}");
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
            input = string.IsNullOrEmpty(input) ? string.Empty : input;
            if (input.Length < length)
                return ValidationResult.Valid(input);

            return ValidationResult.Invalid(input, $"The input must be sorter than {length}");
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
    public static Validator IsInteger(bool allowSign = true)
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input;

            var sb = new StringBuilder(input.Length);
            var noSigns = 0;

            foreach (var c in input)
            {
                if (char.IsDigit(c))
                    sb.Append(c);
                else if (c == '-')
                {
                    noSigns += 1;
                    sb.Append(c);
                }
                else if (c == '_')
                    continue;
                else
                    return ValidationResult.Invalid(input, "The input should only contain numbers");
            }

            if (sb.Length == 0)
                return ValidationResult.Invalid(input, "The input contains no numbers");

            switch (noSigns)
            {
                case 0:
                    break;
                case 1:
                    if (allowSign && sb[0] != '-')
                        return ValidationResult.Invalid(input, "The sign must be the first character in the input");
                    if (!allowSign)
                        return ValidationResult.Invalid(input, "Signs are not allowed in the input");
                    break;
                default:
                    return ValidationResult.Invalid(input, "The input is ambiguous as a signed integer");
            }

            return ValidationResult.Valid(sb.ToString());
        });
    }

    /// <summary>
    /// Creates an <see cref="Validator"/> instance that checks that the input only comprises of only numbers with an decimal point (ignoring whitespace).
    /// </summary>
    /// <param name="allowSigned">Indicates if the decimal number can have a preceding plus or minus sign</param>
    /// <returns>An <see cref="Validator"/> instance</returns>
    public static Validator IsDecimal(bool allowSigned = true)
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input;

            var sb = new StringBuilder(input.Length);
            var decimalPointCount = 0;
            var signCount = 0;

            foreach (var c in input)
            {
                if (char.IsDigit(c))
                    sb.Append(c);
                else if (c == '.')
                {
                    decimalPointCount += 1;
                    sb.Append('.');
                }
                else if (c == '-')
                {
                    signCount += 1;
                    sb.Append(c);
                }
                else
                    return ValidationResult.Invalid(input, $"'{input}' is not a valid decimal number");
            }

            if (sb.Length == 0)
                return ValidationResult.Invalid(input, $"'{input}' is not a valid decimal number");
            if (decimalPointCount > 1)
                return ValidationResult.Invalid(input, $"'{input}' has more than one decimal pint in it");

            switch (signCount)
            {
                case 0:
                    break;
                case 1:
                    if (allowSigned && sb[0] != '-')
                        return ValidationResult.Invalid(input, $"'{input}' is not a valid decimal number");
                    if (!allowSigned)
                        return ValidationResult.Invalid(input, $"'{input}' is not allowed to be negative");
                    break;
                default:
                    return ValidationResult.Invalid(input, $"'{input}' has more than one decimal pint in it");
            }

            return ValidationResult.Valid(sb.ToString());
        });
    }

    /// <summary>
    /// Creates a validator that will parse the input as a boolean value.
    /// <para>The following values are valid boolean strings (case insensitive)
    /// <ul>
    /// <li>true (<see langword="true"/>)</li>
    /// <li>on (<see langword="true"/>)</li>
    /// <li>yes (<see langword="true"/>)</li>
    /// <li>1 (<see langword="true"/>)</li>
    /// <li>false (<see langword="false"/>)</li>
    /// <li>off (<see langword="false"/>)</li>
    /// <li>no (<see langword="false"/>)</li>
    /// <li>0 (<see langword="false"/>)</li>
    /// </ul> 
    /// </para>
    /// </summary>
    /// <returns>An <see cref="Validator"/> instance</returns>
    public static Validator IsBoolean()
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input;

            return TruParser.ParseBool(input).Match(
                some: _ => ValidationResult.Valid(input),
                none: () => ValidationResult.Invalid(input, $"Unrecognised boolean input '{input}'")
            );
        });
    }

    /// <summary>
    /// Creates a <see cref="System.Text.RegularExpressions.Regex"/> pattern validator.
    /// </summary>
    /// <returns>An <see cref="Validator"/> instance</returns>
    public static Validator Regex([StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
    {
        var regex = GetRegexForPattern(pattern);

        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input;
            if (regex.IsMatch(input))
                return ValidationResult.Valid(input.ToLowerInvariant());

            return ValidationResult.Invalid(input, $"'{input}' is not a valid email address");
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
            input = string.IsNullOrEmpty(input) ? string.Empty : input;

            if (policy.MaximumLength < input.Length)
                return ValidationResult.Invalid(input, "The password is too long");

            if (policy.MinimumLength > input.Length)
                return ValidationResult.Invalid(input, "The password is too short");

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
                if (char.IsWhiteSpace(c))
                    hasSpace = true;
                if (char.IsDigit(c))
                    digitCount += 1;
            }

            var result = ValidationResult.Valid(input);

            if (!policy.IsSpaceAllowed && hasSpace)
                result += ValidationResult.Invalid(input, "The password may not contain spaces");
            if (policy.RequiredNumberOfLowerCaseLetters > lowerCaseCount)
                result += ValidationResult.Invalid(input,
                    $"There must be at least {policy.RequiredNumberOfLowerCaseLetters} lower case letters");
            if (policy.RequiredNumberOfUpperCaseLetters > upperCaseCount)
                result += ValidationResult.Invalid(input,
                    $"There must be at least {policy.RequiredNumberOfUpperCaseLetters} upper case letters");
            if (policy.RequiredNumberOfDigits > digitCount)
                result += ValidationResult.Invalid(input,
                    $"There must be at least {policy.RequiredNumberOfDigits} digits (number characters)");
            if (policy.RequiredNumberOfSymbols > symbolCount)
                result += ValidationResult.Invalid(input,
                    $"There must be at least {policy.RequiredNumberOfSymbols} symbol characters (${policy.ListOfAcceptedSymbols})");

            return result;
        });
    }

    /// <summary>
    /// A <see cref="Validator"/> that tries to parse an input string to a <see cref="DateTime"/> based on the date pattern provided.
    /// <para>The pattern is a standard format pattern.</para> 
    /// </summary>
    /// <param name="pattern">The <see cref="DateTime"/> format pattern.</param>
    /// <returns>A <see cref="Validator"/> instance</returns>
    public static Validator IsDateTime([StringSyntax(StringSyntaxAttribute.DateTimeFormat)] string pattern)
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input;

            if (DateTime.TryParseExact(input, pattern, null, DateTimeStyles.None, out var dt))
            {
                // The "s" format string parameter output the DateTime in sortable ISO 8601 format 
                return new Valid(dt.ToString("s"), input);
            }

            return ValidationResult.Invalid(input,
                $"Could not parse the input as a date and time in the format supplied ('{pattern}').");
        });
    }

    /// <summary>
    /// A <see cref="Validator"/> that tries to parse an input string to a <see cref="DateOnly"/> based on the date pattern provided.
    /// <para>The pattern is a standard format pattern.</para> 
    /// </summary>
    /// <param name="pattern">The <see cref="DateOnly"/> format pattern.</param>
    /// <returns>A <see cref="Validator"/> instance</returns>
    public static Validator IsDate([StringSyntax(StringSyntaxAttribute.DateOnlyFormat)] string pattern)
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input;

            if (DateOnly.TryParseExact(input, pattern, null, DateTimeStyles.None, out var dt))
                return new Valid(dt.ToString("yyyy-MM-dd"), input);

            return ValidationResult.Invalid(input,
                $"Could not parse the input as date only in the format supplied ('{pattern}').");
        });
    }

    /// <summary>
    /// A <see cref="Validator"/> that tries to parse an input string to a <see cref="TimeOnly"/> based on the date pattern provided.
    /// <para>The pattern is a standard format pattern.</para> 
    /// </summary>
    /// <param name="pattern">The <see cref="TimeOnly"/> format pattern.</param>
    /// <returns>A <see cref="Validator"/> instance</returns>
    public static Validator IsTime([StringSyntax(StringSyntaxAttribute.TimeOnlyFormat)] string pattern)
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input;

            if (TimeOnly.TryParseExact(input, pattern, null, DateTimeStyles.None, out var dt))
                return new Valid(dt.ToString("HH:mm:ss"), input);

            return ValidationResult.Invalid(input,
                $"Could not parse the input as time only in the format supplied ('{pattern}').");
        });
    }

    /// <summary>
    /// A <see cref="Validator"/> that tries to parse an input string to a <see cref="Guid"/> based on the date pattern provided.
    /// <para>The pattern is a standard format pattern.</para> 
    /// </summary>
    /// <returns>A <see cref="Validator"/> instance</returns>
    public static Validator IsGuid()
    {
        return new Validator(input =>
        {
            input = string.IsNullOrEmpty(input) ? string.Empty : input;

            return TruParser.ParseGuid(input).Match(
                some: _ => ValidationResult.Valid(input),
                none: () => ValidationResult.Invalid(input, $"Unable to parse '{input}' as a GUID")
            );
        });
    }
}