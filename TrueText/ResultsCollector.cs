namespace TrueText;

using System.Collections;

/// <summary>
/// Represents a keyed collection of <see cref="ValidationResult"/>s. 
/// </summary>
public class ResultsCollector<T> : IEnumerable<KeyValuePair<T, ValidationResult>>
{
    // Fields
    private readonly Dictionary<T, ValidationResult> _results;

    public ResultsCollector()
    {
        this._results = new Dictionary<T, ValidationResult>(8);
    }

    /// <summary>
    /// Gets a value indicating whether all the <see cref="ValidationResult"/>s collected are in the valid state.
    /// </summary>
    public bool IsValid { get; private set; }

    /// <summary>
    /// Collects the result into this collection under the specific key; or adds it to an existing entry.
    /// </summary>
    /// <param name="key">The key to collect the result under.</param>
    /// <param name="result">The <see cref="ValidationResult"/> to add</param>
    /// <returns>The <see cref="ValidationResult"/> that has been added</returns>
    public ValidationResult Add(T key, ValidationResult result)
    {
        if (this._results.TryGetValue(key, out var r2))
            this._results[key] = result + r2;
        else
            this._results.Add(key, result);

        // Update the valid status of this ResultCollector
        this.IsValid = this._results.Values.All(r => r.IsValid);

        return result;
    }

    /// <summary>
    /// Gets the result for the supplied <paramref name="key"/>; or an empty <see cref="ValidationResult"/> if not found.
    /// </summary>
    /// <param name="key">The key that the <see cref="ValidationResult"/> was collected under.</param>
    /// <returns>A <see cref="ValidationResult"/> instance</returns>
    public ValidationResult Get(T key)
    {
        if (this._results.TryGetValue(key, out var result))
            return result;

        return ValidationResult.Pure(string.Empty);
    }

    /// <summary>
    /// Matches on the state of this <see cref="ResultsCollector{T}"/> and automatically runs one or the other of the two
    /// provided functions, depending on the state. 
    /// </summary>
    /// <param name="valid">The function to run when the <see cref="ResultsCollector{T}"/> is on a valid state</param>
    /// <param name="invalid">The function that runs when the <see cref="ResultsCollector{T}"/> is in an invalid state</param>
    /// <typeparam name="TResult">The type of the return value</typeparam>
    /// <returns>A <typeparamref name="TResult"/> instance</returns>
    public TResult Match<TResult>(
        Func<TrueReader<T>, TResult> valid,
        Func<ResultsCollector<T>, TResult> invalid
    )
    {
        if (!this.IsValid)
        {
            var dict =
                this._results.Select(
                        pair => KeyValuePair.Create(pair.Key, pair.Value.AsValid().Value)
                    )
                    .ToDictionary();
            return valid(new TrueReader<T>(dict));
        }

        return invalid(this);
    }

    /// <summary>
    /// Matches on the state of this <see cref="ResultsCollector{T}"/> and automatically runs one or the other of the two
    /// provided functions, depending on the state. 
    /// </summary>
    /// <param name="valid">The function to run when the <see cref="ResultsCollector{T}"/> is on a valid state</param>
    /// <param name="invalid">The function that runs when the <see cref="ResultsCollector{T}"/> is in an invalid state</param>
    public void Match(
        Action<TrueReader<T>> valid,
        Action<ResultsCollector<T>> invalid
    )
    {
        if (this.IsValid)
        {
            var dict =
                this._results.Select(
                        pair => KeyValuePair.Create(pair.Key, pair.Value.AsValid().Value)
                    )
                    .ToDictionary();
            valid(new TrueReader<T>(dict));
        }
        else
            invalid(this);
    }

    public IEnumerator<KeyValuePair<T, ValidationResult>> GetEnumerator()
    {
        return _results.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_results).GetEnumerator();
    }

    /// <summary>
    /// Looks up and then compares two results from the <see cref="ResultsCollector{T}"/>
    /// </summary>
    /// <param name="key1">The key value of the first result to compare to</param>
    /// <param name="key2">The key value of the second result to compare with</param>
    /// <param name="message">The error message to add if the comparison fails</param>
    /// <returns>A reference to this <see cref="ResultsCollector{T}"/></returns>
    public ResultsCollector<T> CompareResults(T key1, T key2, string message)
    {
        var result1 = Get(key1);
        var result2 = Get(key2);
        if (result1.IsValid && result2.IsValid)
        {
            if (result1.AsValid().Value != result2.AsValid().Value)
                this.Add(key2, new Invalid(result2.Text, new[] { message }));
        }

        return this;
    }

    /// <summary>
    /// <inheritdoc cref="op_Addition"/>
    /// </summary>
    /// <param name="lhs">The <see cref="ResultsCollector{T}"/> to add the item to</param>
    /// <param name="rhs">A <see cref="Tuple{T1, ValidatioonResult}"/> containing the key to collect the result under.</param>
    /// <returns>The <see cref="ResultsCollector{T}"/></returns>
    public static ResultsCollector<T> operator +(ResultsCollector<T> lhs, Tuple<T, ValidationResult> rhs)
    {
        lhs.Add(rhs.Item1, rhs.Item2);
        return lhs;
    }

    /// <summary>
    /// Creates a new <see cref="ResultsCollector{T}"/> and collects the supplied <paramref name="result"/> to it
    /// using the supplied <paramref name="key"/>
    /// </summary>
    /// <param name="key">The key to collect the result under.</param>
    /// <param name="result">The <see cref="ValidationResult"/> to add</param>
    /// <returns>The new <see cref="ResultsCollector{T}"/></returns>
    public static ResultsCollector<T> Create(T key, ValidationResult result)
    {
        return new ResultsCollector<T>
        {
            { key, result }
        };
    }
}

public static class ResultCollectorExtensions
{
    public static Tuple<T, ValidationResult> WithKey<T>(this ValidationResult result, T key) =>
        Tuple.Create(key, result);
}