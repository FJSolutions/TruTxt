namespace TrueText;

using System.Text;

/// <summary>
/// Represents a policy for validating passwords against. 
/// </summary>
public sealed class PasswordPolicy
{
    // Fields
    private static readonly char[] DigitCharacters = "0123456789".ToArray();
    private static readonly char[] LowercaseCharacters = "abcdefghijklmnopqrstuvwxyz".ToArray();
    private static readonly char[] UppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();

    /// <summary>
    /// Private constructor to prevent external instantiation.
    /// </summary>
    private PasswordPolicy()
    {
        MinimumLength = 5;
        MaximumLength = UInt16.MaxValue;
        IsSpaceAllowed = false;
        RequiredNumberOfLowerCaseLetters = 0;
        RequiredNumberOfUpperCaseLetters = 0;
        RequiredNumberOfDigits = 0;
        RequiredNumberOfSymbols = 0;
        ListOfAcceptedSymbols = "!@#$%^&*".ToArray();
    }

    /// <summary>
    /// The minimum length of the password 
    /// </summary>
    public UInt16 MinimumLength { get; private set; }

    /// <summary>
    /// The maximum length of the password
    /// </summary>
    public UInt16 MaximumLength { get; private set; }

    /// <summary>
    /// The number of upper case letters required for the password
    /// </summary>
    public int RequiredNumberOfUpperCaseLetters { get; private set; }

    /// <summary>
    /// The number of required lower case letters for the password
    /// </summary>
    public int RequiredNumberOfLowerCaseLetters { get; private set; }

    /// <summary>
    /// The number of required numerical digit letters required for the password
    /// </summary>
    public int RequiredNumberOfDigits { get; private set; }

    /// <summary>
    /// The list of recognised symbol characters for the password
    /// </summary>
    public char[] ListOfAcceptedSymbols { get; private set; }

    /// <summary>
    /// The number of required symbol characters required for the password
    /// </summary>
    public int RequiredNumberOfSymbols { get; private set; }

    /// <summary>
    /// Whether spaces are allowed in the password
    /// </summary>
    public bool IsSpaceAllowed { get; private set; }

    /// <summary>
    /// Creates a new password based on the this <see cref="PasswordPolicy"/>.
    /// </summary>
    /// <returns>A <code>string</code> containing the new password.</returns>
    public string NewPassword()
    {
        var length = this.RequiredNumberOfSymbols +
                     this.RequiredNumberOfDigits +
                     this.RequiredNumberOfLowerCaseLetters +
                     this.RequiredNumberOfUpperCaseLetters;
        if (length < this.MinimumLength)
            length = this.MinimumLength;
        if (length < 6)
            length = 6;

        var rand = new Random();
        var characters = new List<char>();
        var noItems = this.RequiredNumberOfSymbols < 1 ? 1 : this.RequiredNumberOfSymbols;

        if (this.ListOfAcceptedSymbols.Length > 0)
        {
            for (var i = 0; i < noItems; i++)
            {
                characters.Add(
                    this.ListOfAcceptedSymbols[rand.Next(this.ListOfAcceptedSymbols.Length)]
                );
            }
        }

        noItems = this.RequiredNumberOfDigits < 1 ? 1 : this.RequiredNumberOfDigits;
        for (var i = 0; i < noItems; i++)
        {
            characters.Add(
                DigitCharacters[rand.Next(DigitCharacters.Length)]
            );
        }

        noItems = this.RequiredNumberOfUpperCaseLetters < 1 ? 1 : this.RequiredNumberOfUpperCaseLetters;
        for (var i = 0; i < noItems; i++)
        {
            characters.Add(
                UppercaseCharacters[rand.Next(UppercaseCharacters.Length)]
            );
        }

        noItems = length - characters.Count;
        for (var i = 0; i < noItems; i++)
        {
            characters.Add(
                LowercaseCharacters[rand.Next(LowercaseCharacters.Length)]
            );
        }

        var sb = new StringBuilder();
        for (var i = length - 1; i >= 0; i--)
        {
            var index = rand.Next(i);
            sb.Append(characters[index]);
            characters.RemoveAt(index);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Creates a password <see cref="PolicyBuilder"/> used to create custom <see cref="PasswordPolicy"/>s.
    /// </summary>
    /// <returns>A <see cref="PolicyBuilder"/> instance.</returns>
    public static PolicyBuilder Create()
    {
        return new PolicyBuilder(new PasswordPolicy());
    }

    /// <summary>
    /// The lenient <see cref="PasswordPolicy"/> only requires a password to be 5 characters long.
    /// </summary>
    /// <returns>A <see cref="PasswordPolicy"/> instance.</returns>
    public static PasswordPolicy Lenient()
    {
        return new PasswordPolicy();
    }

    /// <summary>
    /// A medium-strength <see cref="PasswordPolicy"/> requiring a minimum of 8 characters and at least 1 upper and lower case letter, 1 symbol and 1 number.
    /// </summary>
    /// <returns>A <see cref="PasswordPolicy"/> instance.</returns>
    public static PasswordPolicy Medium()
    {
        return new PasswordPolicy()
        {
            MinimumLength = 8,
            RequiredNumberOfDigits = 1,
            RequiredNumberOfSymbols = 1,
            RequiredNumberOfUpperCaseLetters = 1,
            RequiredNumberOfLowerCaseLetters = 1
        };
    }

    /// <summary>
    /// A strong <see cref="PasswordPolicy"/> requiring a minimum of 12 characters and at least 3 upper and lower case letters, 2 symbols, and 2 numbers.
    /// </summary>
    /// <returns>A <see cref="PasswordPolicy"/> instance.</returns>
    public static PasswordPolicy Strong()
    {
        return new PasswordPolicy()
        {
            MinimumLength = 12,
            RequiredNumberOfDigits = 2,
            RequiredNumberOfSymbols = 2,
            RequiredNumberOfUpperCaseLetters = 3,
            RequiredNumberOfLowerCaseLetters = 3
        };
    }

    /// <summary>
    /// Represents an object that can safely build <see cref="PasswordPolicy"/>s.  
    /// </summary>
    public sealed class PolicyBuilder
    {
        // Fields
        private readonly PasswordPolicy _policy;

        internal PolicyBuilder(PasswordPolicy policy)
        {
            this._policy = policy;
        }

        /// <summary>
        /// Returns the constructed <see cref="PasswordPolicy"/>
        /// </summary>
        /// <returns></returns>
        public PasswordPolicy Build()
        {
            return this._policy;
        }

        /// <summary>
        /// Sets the minimum length of the password for the policy. 
        /// </summary>
        /// <param name="minLength">The minimum length of the password</param>
        /// <returns>The <see cref="PolicyBuilder"/> instance</returns>
        public PolicyBuilder MinimumLength(UInt16 minLength)
        {
            this._policy.MinimumLength = minLength;
            return this;
        }

        /// <summary>
        /// Sets the maximum length of the password for the policy. 
        /// </summary>
        /// <param name="maxLength">The maximum length of the password</param>
        /// <returns>The <see cref="PolicyBuilder"/> instance</returns>
        public PolicyBuilder MaximumLength(UInt16 maxLength)
        {
            this._policy.MaximumLength = maxLength;
            return this;
        }

        /// <summary>
        /// Sets whether spaces are allowed in the password
        /// </summary>
        /// <param name="isSpaceAllowed">Indicates whether spaces are allowed in the password</param>
        /// <returns>The <see cref="PolicyBuilder"/> instance</returns>
        public PolicyBuilder IsSpaceAllowed(bool isSpaceAllowed)
        {
            this._policy.IsSpaceAllowed = isSpaceAllowed;
            return this;
        }

        /// <summary>
        /// Sets the required number of upper case letters in the password for the policy. 
        /// </summary>
        /// <param name="requiredNumberOfUpperCaseLetters">The number of required upper case letters in the password</param>
        /// <returns>The <see cref="PolicyBuilder"/> instance</returns>
        public PolicyBuilder RequiredNumberOfUpperCaseLetters(UInt16 requiredNumberOfUpperCaseLetters)
        {
            this._policy.RequiredNumberOfUpperCaseLetters = requiredNumberOfUpperCaseLetters;
            return this;
        }

        /// <summary>
        /// Sets the required number of lower case letters in the password for the policy. 
        /// </summary>
        /// <param name="requiredNumberOfLowerCaseLetters">The number of required lower case letters in the password</param>
        /// <returns>The <see cref="PolicyBuilder"/> instance</returns>
        public PolicyBuilder RequiredNumberOfLowerCaseLetters(UInt16 requiredNumberOfLowerCaseLetters)
        {
            this._policy.RequiredNumberOfLowerCaseLetters = requiredNumberOfLowerCaseLetters;
            return this;
        }

        /// <summary>
        /// Sets the required number of digit characters in the password for the policy. 
        /// </summary>
        /// <param name="requiredNumberOfDigits">The number of required digit characters in the password</param>
        /// <returns>The <see cref="PolicyBuilder"/> instance</returns>
        public PolicyBuilder RequiredNumberOfDigits(UInt16 requiredNumberOfDigits)
        {
            this._policy.RequiredNumberOfDigits = requiredNumberOfDigits;
            return this;
        }

        /// <summary>
        /// Sets the required number of symbol characters in the password for the policy. 
        /// </summary>
        /// <param name="requiredNumberOfSymbols">The number of required symbol characters in the password</param>
        /// <returns>The <see cref="PolicyBuilder"/> instance</returns>
        public PolicyBuilder RequiredNumberOfSymbols(UInt16 requiredNumberOfSymbols)
        {
            this._policy.RequiredNumberOfSymbols = requiredNumberOfSymbols;
            return this;
        }

        /// <summary>
        /// Sets the recognised symbol characters for the password for the policy. 
        /// </summary>
        /// <param name="listOfAcceptedSymbols">The string containing all the symbol characters allowed in the password</param>
        /// <returns>The <see cref="PolicyBuilder"/> instance</returns>
        public PolicyBuilder ListOfAcceptedSymbols(string listOfAcceptedSymbols)
        {
            this._policy.ListOfAcceptedSymbols = listOfAcceptedSymbols.ToArray();
            return this;
        }
    }
}