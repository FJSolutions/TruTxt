namespace TrueTextTests;

using V = TrueText.Validator;

public class ValidatorExtractorTests
{
    [Fact]
    public void TrimValidatorTest()
    {
        var result = V.Trim()
            .Apply(" FBJ\t")
            .Reduce(string.Empty);
        
        Assert.Equal("FBJ", result);
    }

    [Fact]
    public void NoTrimValidatorTest()
    {
        var result = V.Trim()
            .Apply("fBj7")
            .Reduce(string.Empty);
        
        Assert.Equal("fBj7", result);
    }

    [Fact]
    public void ExtractDigitsSuccessfullyTest()
    {
        var result = V.ExtractNumber()
            .Apply("Tel: (011) 793-5432 ")
            .Reduce(string.Empty);
        
        Assert.Equal("0117935432", result);
    }

    [Fact]
    public void NoDigitsToExtractTest()
    {
        var result = V.ExtractNumber()
            .Apply("This contains no. digits!")
            .Reduce(string.Empty);
        
        Assert.Empty(result);
    }

    [Fact]
    public void ExtractDigitsFromIntegerSuccessfullyTest()
    {
        var result = V.ExtractNumber()
            .Apply("$ 1,000")
            .Reduce(string.Empty);
        
        Assert.Equal("1000", result);
    }

    [Fact]
    public void ExtractDigitsFromDecimalSuccessfullyTest()
    {
        var result = V.ExtractNumber()
            .Apply("$ 1,000.01")
            .Reduce(string.Empty);
        
        Assert.Equal("1000.01", result);
    }

    [Fact]
    public void ExtractIntegerFromDecimalUnSuccessfullyTest()
    {
        var result = V.ExtractNumber(false)
            .Apply("$ 1,000.01");
        
        Assert.False(result.IsValid);
    }
}