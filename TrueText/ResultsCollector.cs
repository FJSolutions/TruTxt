namespace TrueText;

using System.Collections;

/// <summary>
/// Represents a keyed collection of <see cref="ValidationResult"/>s. 
/// </summary>
public class ResultsCollector : IEnumerable<KeyValuePair<string, ValidationResult>>
{
    // Fields
    private readonly Dictionary<string, ValidationResult> _results;

    public ResultsCollector()
    {
        this._results = new Dictionary<string, ValidationResult>(8);
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
    public ValidationResult Add(string key, ValidationResult result)
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
    public ValidationResult Get(string key)
    {
        if (this._results.TryGetValue(key, out var result))
            return result;

        return ValidationResult.Pure(string.Empty);
    }

    /// <summary>
    /// Matches on the state of this <see cref="ResultsCollector"/> and automatically runs one or the other of the two
    /// provided functions, depending on the state. 
    /// </summary>
    /// <param name="valid">The function to run when the <see cref="ResultsCollector"/> is on a valid state</param>
    /// <param name="invalid">The function that runs when the <see cref="ResultsCollector"/> is in an invalid state</param>
    /// <typeparam name="TResult">The type of the return value</typeparam>
    /// <returns>A <typeparamref name="TResult"/> instance</returns>
    public TResult Match<TResult>(
        Func<TrueReader, TResult> valid,
        Func<ResultsCollector, TResult> invalid
    )
    {
        if (!this.IsValid)
        {
            var dict =
                this._results.Select(
                        pair => KeyValuePair.Create(pair.Key, pair.Value.AsValid().Value)
                    )
                    .ToDictionary();
            return valid(new TrueReader(dict));
        }

        return invalid(this);
    }

    /// <summary>
    /// Matches on the state of this <see cref="ResultsCollector"/> and automatically runs one or the other of the two
    /// provided functions, depending on the state. 
    /// </summary>
    /// <param name="valid">The function to run when the <see cref="ResultsCollector"/> is on a valid state</param>
    /// <param name="invalid">The function that runs when the <see cref="ResultsCollector"/> is in an invalid state</param>
    public void Match(
        Action<TrueReader> valid,
        Action<ResultsCollector> invalid
    )
    {
        if (this.IsValid)
        {
            var dict =
                this._results.Select(
                        pair => KeyValuePair.Create(pair.Key, pair.Value.AsValid().Value)
                    )
                    .ToDictionary();
            valid(new TrueReader(dict));
        }
        else
            invalid(this);
    }

    /// <summary>
    /// Maps this <see cref="ResultsCollector"/> through a <see cref="TrueReader"/> data mapper and then to a the <param name="valid"></param> function.
    /// <para>the point of the <param name="dataProcessor"></param> function is to safely transform valid text input to strongly typed values.</para> 
    /// </summary>
    /// <param name="dataProcessor">The data processing function</param>
    /// <param name="valid">The final, function with a valid data model</param>
    /// <param name="invalid">The function to run when the input is not in a valid state</param>
    /// <typeparam name="TModel">The type of the data model</typeparam>
    /// <typeparam name="TResult">The function's result type</typeparam>
    /// <returns>A <see cref="TResult"/></returns>
    public TResult MapWithReader<TModel, TResult>(
        Func<TrueReader, Result<TModel>> dataProcessor,
        Func<TModel, TResult> valid,
        Func<ResultsCollector, TResult> invalid)
    {
        // If this is a valid result collector
        if (this.IsValid)
        {
            // Run the data processor and get an interim value to pass into the valid function
            var dict =
                this._results.Select(
                        pair => KeyValuePair.Create(pair.Key, pair.Value.AsValid().Value)
                    )
                    .ToDictionary();
            var dataResult = dataProcessor(new TrueReader(dict));

            // Call the valid function if data processing was successful and return its result 
            if (dataResult is Ok<TModel> data)
                return valid(data.Value);

            // Otherwise, fallthrough after adding the data processor failure error to this collector
            if (dataResult is Failure<TModel> failure)
                this.Add(failure.Key, new Invalid(failure.Text, new[] { failure.Error }));
        }

        // Process the errors
        return invalid(this);
    }

    public IEnumerator<KeyValuePair<string, ValidationResult>> GetEnumerator()
    {
        return _results.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_results).GetEnumerator();
    }

    /// <summary>
    /// Looks up and then compares two results from the <see cref="ResultsCollector"/>
    /// </summary>
    /// <param name="key1">The key value of the first result to compare to</param>
    /// <param name="key2">The key value of the second result to compare with</param>
    /// <param name="message">The error message to add if the comparison fails</param>
    /// <returns>A reference to this <see cref="ResultsCollector"/></returns>
    public ResultsCollector CompareResults(string key1, string key2, string message)
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
    /// <param name="lhs">The <see cref="ResultsCollector"/> to add the item to</param>
    /// <param name="rhs">A <see cref="Tuple{T1, ValidatioonResult}"/> containing the key to collect the result under.</param>
    /// <returns>The <see cref="ResultsCollector"/></returns>
    public static ResultsCollector operator +(ResultsCollector lhs, Tuple<string, ValidationResult> rhs)
    {
        lhs.Add(rhs.Item1, rhs.Item2);
        return lhs;
    }

    /// <summary>
    /// Creates a new <see cref="ResultsCollector"/> and collects the supplied <paramref name="result"/> to it
    /// using the supplied <paramref name="key"/>
    /// </summary>
    /// <param name="key">The key to collect the result under.</param>
    /// <param name="result">The <see cref="ValidationResult"/> to add</param>
    /// <returns>The new <see cref="ResultsCollector"/></returns>
    public static ResultsCollector Create(string key, ValidationResult result)
    {
        return new ResultsCollector
        {
            { key, result }
        };
    }
}

public static class ResultCollectorExtensions
{
    public static Tuple<string, ValidationResult> WithKey(this ValidationResult result, string key) =>
        Tuple.Create(key, result);
}