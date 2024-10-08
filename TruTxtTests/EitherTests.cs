namespace TruTxtTests;

using static TruTxt.Common.Either<string, int>;

public class EitherTests
{
   
   [Fact]
   public void EiterLeftTest()
   {
      var left = Left("An error");
      var result = left.Match(
         onLeft: e => e + "!",
         onRight: _ => string.Empty
      );
      Assert.Equal("An error!", result);
   }
}