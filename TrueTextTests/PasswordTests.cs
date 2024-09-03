using TrueText;

namespace TrueTextTests;

using V = TrueText.Validator;

public class PasswordTests
{
    [Fact]
    public void BasicPolicyMinValidTest()
    {
        var policy = PasswordPolicy.Create()
            .MinimumLength(3)
            .Build();
        var result = V.Password(policy)
            .Apply("FBJ");
        
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void BasicPolicyMinInvalidTest()
    {
        var policy = PasswordPolicy.Create()
            .MinimumLength(3)
            .Build();
        var result = V.Password(policy)
            .Apply("Ox");
        
        Assert.False(result.IsValid);
    }
    
    [Fact]
    public void BasicPolicyMaxValidTest()
    {
        var policy = PasswordPolicy.Create()
            .MaximumLength(15)
            .Build();
        var result = V.Password(policy)
            .Apply("Fifteen");
        
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void BasicPolicyMaxInvalidTest()
    {
        var policy = PasswordPolicy.Create()
            .MaximumLength(5)
            .Build();
        var result = V.Password(policy)
            .Apply("Fifteen");
        
        Assert.False(result.IsValid);
    }
    
    [Fact]
    public void BasicPolicyNoLowercaseTest()
    {
        var policy = PasswordPolicy.Create()
            .RequiredNumberOfLowerCaseLetters(1)
            .Build();
        var result = V.Password(policy)
            .Apply("FBJ");
        
        Assert.False(result.IsValid);
    }
    
    [Fact]
    public void BasicPolicyLowercaseTest()
    {
        var policy = PasswordPolicy.Create()
            .RequiredNumberOfLowerCaseLetters(1)
            .Build();
        var result = V.Password(policy)
            .Apply("FBj");
        
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void BasicPolicyNoUppercaseTest()
    {
        var policy = PasswordPolicy.Create()
            .RequiredNumberOfUpperCaseLetters(1)
            .Build();
        var result = V.Password(policy)
            .Apply("fbj");
        
        Assert.False(result.IsValid);
    }
    
    [Fact]
    public void BasicPolicyUppercaseTest()
    {
        var policy = PasswordPolicy.Create()
            .RequiredNumberOfUpperCaseLetters(1)
            .Build();
        var result = V.Password(policy)
            .Apply("Fbj");
        
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void BasicPolicySymbolsTest()
    {
        var policy = PasswordPolicy.Create()
            .RequiredNumberOfSymbols(1)
            .Build();
        var result = V.Password(policy)
            .Apply("F@J");
        
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void BasicPolicyAllowWhitespaceTest()
    {
        var policy = PasswordPolicy.Create()
            .IsSpaceAllowed(true)
            .Build();
        var result = V.Password(policy)
            .Apply("F J");
        
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void BasicPolicyNoAllowWhitespaceTest()
    {
        var policy = PasswordPolicy.Create()
            .IsSpaceAllowed(false)
            .Build();
        var result = V.Password(policy)
            .Apply("F J");
        
        Assert.False(result.IsValid);
    }
}