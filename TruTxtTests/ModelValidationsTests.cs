namespace TruTxtTests;

using TruTxt;

using V = TruTxt.Validator;
using Validations = TruTxt.ModelValidations<Person>;

// ReSharper disable once ClassNeverInstantiated.Global
internal record Person(string FirstName, string SecondName, int Age);

public class ModelValidationsTests
{
   [Fact]
   public void GetPropertyNameAlwaysReturnsValidResultTest()
   {
      var results = Validations.Empty();
      Assert.Equal(ValidationResult.Empty(), results.Get(p => p.FirstName));
   }

   [Fact]
   public void ResultCollectorCreationTest()
   {
      var result = V.Min(3).Apply("FBJ");
      var results = Validations.Runner().Invoke(p => p.FirstName, result);

      Assert.NotNull(results);
      Assert.NotEmpty(results);
      Assert.True(result.IsValid);
      Assert.True(results.IsValid);
   }

   [Fact]
   public void ResultCollectorAddWithKeyTest()
   {
      var result = V.Min(3).Apply("FBJ");
      var runner = Validations.Runner();
      var results = runner(p => p.FirstName, result)
                    + runner(p => p.SecondName, V.Min(2).Apply("NJ"));

      Assert.NotNull(results);
      Assert.NotEmpty(results);
      Assert.Equal(2, results.Count());
      Assert.True(result.IsValid);
      Assert.True(results.IsValid);
   }

   [Fact]
   public void ResultCollectorGetByKeyTest()
   {
      var result = V.Min(3).Apply("FBJ");
      var runner = Validations.Runner();
      var results = runner(p => p.FirstName, result)
                    + runner(p => p.SecondName, V.Min(2).Apply("NJ"));

      var name = results.Get(p => p.FirstName).Reduce();
      Assert.Equal("FBJ", name);
      name = results.Get(p => p.SecondName).Reduce();
      Assert.Equal("NJ", name);
      name = results.Get("Jarrod").Reduce();
      Assert.Equal("", name);
   }

   // ReSharper disable once ClassNeverInstantiated.Local
   private record ChangePassword(string Uid, string NewPassword, string ConfirmPassword);

   [Fact]
   public void ComparisonValidTest()
   {
      var newPassword = PasswordPolicy.Medium().NewPassword();
      var runner = ModelValidations<ChangePassword>.Runner();
      var results =
         runner(p => p.NewPassword, V.Password(PasswordPolicy.Medium()).Apply(newPassword))
         + runner(p => p.ConfirmPassword, V.Password(PasswordPolicy.Medium()).Apply(newPassword));

      Assert.True(results.IsValid);

      results = results.CompareResults(p => p.NewPassword, p => p.ConfirmPassword, "The passwords don't match");

      Assert.True(results.IsValid);
   }

   [Fact]
   public void ComparisonInvalidTest()
   {
      var newPassword = PasswordPolicy.Medium().NewPassword();
      var newPassword2 = PasswordPolicy.Medium().NewPassword();
      var runner = ModelValidations<ChangePassword>.Runner();
      var results =
         runner(p => p.NewPassword, V.Password(PasswordPolicy.Medium()).Apply(newPassword))
         + runner(p => p.ConfirmPassword, V.Password(PasswordPolicy.Medium()).Apply(newPassword2));

      Assert.True(results.IsValid);

      var message = "The passwords don't match";
      results = results.CompareResults(p => p.NewPassword, p => p.ConfirmPassword, message);

      Assert.False(results.IsValid);
      Assert.Equal(message, results.Get(p => p.ConfirmPassword).AsInvalid().Errors[0]);
   }
}