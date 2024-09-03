namespace TrueText;

/// <summary>
/// Represents a keyed collection of <see cref="ValidationResult{T}"/>s. 
/// </summary>
public class ResultsCollector<T>
{
    // Fields
    private readonly List<Tuple<IEquatable<T>, ValidationResult<string>>> _results;

    public ResultsCollector()
    {
        this._results = new List<Tuple<IEquatable<T>, ValidationResult<string>>>(8);
    }

    /// <summary>
    /// Indicates whether all the <see cref="ValidationResult{T}"/>s collected are in the valid state.
    /// </summary>
    /// <returns><c>true</c> if all the collected <see cref="ValidationResult{T}"/>s are valid; otherwise <c>false</c></returns>
    public bool IsValid()
    {
        return this._results.All(t => t.Item2.IsValid);
    }

    public bool AddResult(T key, ValidationResult<T> result)
    {
        throw new NotImplementedException();
    }
}