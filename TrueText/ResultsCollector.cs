using System.Collections;
using System.Security.AccessControl;

namespace TrueText;

/// <summary>
/// Represents a keyed collection of <see cref="ValidationResult{T}"/>s. 
/// </summary>
public class ResultsCollector<T> : IEnumerable<KeyValuePair<T, ValidationResult<string>>>
{
    // Fields
    private readonly Dictionary<T, ValidationResult<string>> _results;

    public ResultsCollector()
    {
        this._results = new Dictionary<T, ValidationResult<string>>(8);
    }

    /// <summary>
    /// Indicates whether all the <see cref="ValidationResult{T}"/>s collected are in the valid state.
    /// </summary>
    /// <returns><c>true</c> if all the collected <see cref="ValidationResult{T}"/>s are valid; otherwise <c>false</c></returns>
    public bool IsValid()
    {
        return this._results.Values.All(result => result.IsValid);
    }

    /// <summary>
    /// Collects the result into this collection under the specific key; or adds it to an existing entry.
    /// </summary>
    /// <param name="key">The key to collect the result under.</param>
    /// <param name="result">The <see cref="ValidationResult{T}"/> to add</param>
    /// <returns>The <see cref="ValidationResult{T}"/> that has been added</returns>
    public ValidationResult<string> Add(T key, ValidationResult<string> result)
    {
        if (this._results.TryGetValue(key, out var r2))
            this._results[key] = result + r2;
        else
            this._results.Add(key, result);

        return result;
    }

    /// <summary>
    /// Gets the result for the supplied <paramref name="key"/>; or an empty <see cref="ValidationResult{T}"/> if not found.
    /// </summary>
    /// <param name="key">The key that the <see cref="ValidationResult{T}"/> was collected under.</param>
    /// <returns>A <see cref="ValidationResult{T}"/> instance</returns>
    public ValidationResult<string> GetResult(T key)
    {
        if (this._results.TryGetValue(key, out var result))
            return result;

        return ValidationResult<string>.Pure(string.Empty);
    }

    public IEnumerator<KeyValuePair<T, ValidationResult<string>>> GetEnumerator()
    {
        return _results.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_results).GetEnumerator();
    }

    /// <summary>
    /// <inheritdoc cref="op_Addition"/>
    /// </summary>
    /// <param name="lhs">The <see cref="ResultsCollector{T}"/> to add the item to</param>
    /// <param name="rhs">A <see cref="Tuple{T1, ValidatioonResult}"/> containing the key to collect the result under.</param>
    /// <returns>The <see cref="ResultsCollector{T}"/></returns>
    public static ResultsCollector<T> operator +(ResultsCollector<T> lhs, Tuple<T, ValidationResult<string>> rhs)
    {
        lhs.Add(rhs.Item1, rhs.Item2);
        return lhs;
    }

    /// <summary>
    /// Creates a new <see cref="ResultsCollector{T}"/> and collects the supplied <paramref name="result"/> to it
    /// using the supplied <paramref name="key"/>
    /// </summary>
    /// <param name="key">The key to collect the result under.</param>
    /// <param name="result">The <see cref="ValidationResult{T}"/> to add</param>
    /// <returns>The new <see cref="ResultsCollector{T}"/></returns>
    public static ResultsCollector<T> Create(T key, ValidationResult<string> result)
    {
        return new ResultsCollector<T>
        {
            { key, result }
        };
    }
}

public static class ResultCollectorExtensions
{
    public static Tuple<T, ValidationResult<string>> WithKey<T>(this ValidationResult<string> result, T key) =>
        Tuple.Create(key, result);
}