namespace TrueText;

using Result = ValidationResult<string>;

public sealed class Validator
{
    // Fields
    private readonly Func<string, Result> _func;
    
    public Validator(Func<string, Result> func)
    {
        this._func = func;
    }
}
