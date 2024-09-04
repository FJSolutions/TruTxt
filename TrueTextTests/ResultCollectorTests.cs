namespace TrueTextTests;

using TrueText;
using V = TrueText.Validator;

public class ResultCollectorTests
{
    [Fact]
    public void ResultCollectorCreationTest()
    {
        var result = V.Min(3).Apply("FBJ");
        var results = ResultsCollector<string>.Create("Test", result);

        Assert.NotNull(results);
        Assert.NotEmpty(results);
        Assert.True(result.IsValid);
        Assert.True(results.IsValid());
    }

    [Fact]
    public void ResultCollectorAddWithKeyTest()
    {
        var result = V.Min(3).Apply("FBJ");
        var results = ResultsCollector<string>.Create("Francis", result);
        results += V.Min(2).Apply("NJ").WithKey("Nadine");


        Assert.NotNull(results);
        Assert.NotEmpty(results);
        Assert.Equal(2, results.Count());
        Assert.True(result.IsValid);
        Assert.True(results.IsValid());
    }

    [Fact]
    public void ResultCollectorGetByKeyTest()
    {
        var result = V.Min(3).Apply("FBJ");
        var results = ResultsCollector<string>.Create("Francis", result);
        results += V.Min(2).Apply("NJ").WithKey("Nadine");

        var name = results.GetResult("Francis").Reduce();
        Assert.Equal("FBJ", name);
        name = results.GetResult("Nadine").Reduce();
        Assert.Equal("NJ", name);
        name = results.GetResult("Jarrod").Reduce();
        Assert.Equal("", name);
    }
}