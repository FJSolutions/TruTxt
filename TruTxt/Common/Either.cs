namespace TruTxt.Common;

public interface IEither<TLeft, TRight>
{
   TResult Match<TResult>(Func<TLeft, TResult> onLeft, Func<TRight, TResult> onRight);
}

public record Left<TLeft, TRight>(TLeft Value) : IEither<TLeft, TRight>
{
   public TResult Match<TResult>(Func<TLeft, TResult> onLeft, Func<TRight, TResult> onRight)
   {
      return onLeft(Value);
   }
}

public record Right<TLeft, TRight>(TRight Value) : IEither<TLeft, TRight>
{
   public TResult Match<TResult>(Func<TLeft, TResult> onLeft, Func<TRight, TResult> onRight)
   {
      return onRight(Value);
   }
}

public static class Either<L, R>
{
   public static T Identity<T>(T value) => value;
   
   public static IEither<L, R> Left(L left) => new Left<L, R>(left);
}