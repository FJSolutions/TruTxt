using TruTxt.Common;

namespace TruTxtTests;

using TruTxt;

using Xunit.Abstractions;

using static TruTxtParser;

public class TruTxtParserTests(ITestOutputHelper output)
{
   [Fact]
   public void ParseStringTest()
   {
      var result =
         ParseString("").Match(
            onSome: _ => string.Empty,
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeInt8Test()
   {
      var result =
         ParseInt8("7").Match(
            onSome: value =>
            {
               Assert.Equal(7, value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneInt8Test()
   {
      var result =
         ParseInt8("A").Match(
            onSome: _ =>
            {
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );
   }

   [Fact]
   public void ParseSomeInt16Test()
   {
      var result =
         ParseInt16("7").Match(
            onSome: value =>
            {
               Assert.Equal(7, value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneInt16Test()
   {
      var result =
         ParseInt16("A").Match(
            onSome: _ =>
            {
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeInt32Test()
   {
      var result =
         ParseInt32("7").Match(
            onSome: value =>
            {
               Assert.Equal(7, value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneInt32Test()
   {
      var result =
         ParseInt32("A").Match(
            onSome: _ =>
            {
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeInt64Test()
   {
      var result =
         ParseInt64("7").Match(
            onSome: value =>
            {
               Assert.Equal(7, value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneInt64Test()
   {
      var result =
         ParseInt64("A").Match(
            onSome: _ =>
            {
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeUInt8Test()
   {
      var result =
         ParseUInt8("7").Match(
            onSome: value =>
            {
               Assert.Equal(7, value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneUInt8Test()
   {
      var result =
         ParseUInt8("A").Match(
            onSome: _ =>
            {
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeUInt16Test()
   {
      var result =
         ParseUInt16("7").Match(
            onSome: value =>
            {
               Assert.Equal(7, value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneUInt16Test()
   {
      var result =
         ParseUInt16("A").Match(
            onSome: _ =>
            {
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeUInt32Test()
   {
      var result =
         ParseUInt32("7").Match(
            onSome: value =>
            {
               Assert.True(7 == value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneUInt32Test()
   {
      var result =
         ParseUInt32("A").Match(
            onSome: _ =>
            {
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeUInt64Test()
   {
      var result =
         ParseUInt64("7").Match(
            onSome: value =>
            {
               Assert.True(7 == value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneUInt64Test()
   {
      var result =
         ParseUInt64("A").Match(
            onSome: _ =>
            {
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeFloatTest()
   {
      var result =
         ParseSingle("7.0").Match(
            onSome: value =>
            {
               Assert.Equal(7.0, value, 0.0001);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneFloatTest()
   {
      var result =
         ParseUInt64("A").Match(
            onSome: _ =>
            {
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeDoubleTest()
   {
      var result =
         ParseDouble("7.0").Match(
            onSome: value =>
            {
               Assert.Equal(7.0, value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneDoubleTest()
   {
      var result =
         ParseUInt64("A").Match(
            onSome: _ =>
            {
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeDecimalTest()
   {
      var result =
         ParseDecimal("7.0").Match(
            onSome: value =>
            {
               Assert.Equal(7.0M, value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneDecimalTest()
   {
      var result =
         ParseUInt64("A").Match(
            onSome: _ =>
            {
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeGuidTest()
   {
      var result =
         ParseGuid("B808889A-3545-4DD7-A4C5-5AF6FBAAD098").Match(
            onSome: value =>
            {
               Assert.Equal(Guid.Parse("B808889A35454DD7A4C55AF6FBAAD098"), value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneGuidTest()
   {
      var result =
         ParseUInt64("B808889A-3545-4DD7-A4C5-5AF6FBAAD09Z").Match(
            onSome: _ =>
            {
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeDateTimeTest()
   {
      var result =
         ParseDateTime("1965-10-25 00:12:01").Match(
            onSome: value =>
            {
               Assert.Equal(new DateTime(1965, 10, 25, 0, 12, 1), value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneDateTimeTest()
   {
      var result =
         ParseDateTime("Oct 25 1965 25:12:01").Match(
            onSome: value =>
            {
               output.WriteLine("{0}", value);
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeDateTest()
   {
      var result =
         ParseDate("1965-10-25").Match(
            onSome: value =>
            {
               Assert.Equal(new DateOnly(1965, 10, 25), value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNoneDateTest()
   {
      var result =
         ParseDate("Oct 32 1965").Match(
            onSome: value =>
            {
               output.WriteLine("{0}", value);
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseSomeTimeTest()
   {
      var result =
         ParseTime("23:59:59").Match(
            onSome: value =>
            {
               Assert.Equal(new TimeOnly(23, 59, 59), value);
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.Fail();
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void ParseNonTimeTest()
   {
      var result =
         ParseTime("23:59:60").Match(
            onSome: value =>
            {
               output.WriteLine("{0}", value);
               Assert.Fail();
               return string.Empty;
            },
            onNone: () =>
            {
               Assert.True(true);
               return string.Empty;
            }
         );

      Assert.Empty(result);
   }

   [Fact]
   public void GenericParseStringTest()
   {
      var result = ParseObject("Francis", typeof(string));

      Assert.Equal(Option<object>.Some("Francis"), result);
   }

   [Fact]
   public void GenericParseInt8Test()
   {
      var result = ParseObject("58", typeof(sbyte));

      Assert.Equivalent(Option<object>.Some(58), result);
   }

   [Fact]
   public void GenericParseInt16Test()
   {
      var result = ParseObject("58", typeof(short));

      Assert.Equivalent(Option<object>.Some(58), result);
   }

   [Fact]
   public void GenericParseInt32Test()
   {
      var result = ParseObject("58", typeof(int));

      Assert.Equivalent(Option<object>.Some(58), result);
   }

   [Fact]
   public void GenericParseInt64Test()
   {
      var result = ParseObject("58", typeof(long));

      Assert.Equivalent(Option<object>.Some(58L), result);
   }

   [Fact]
   public void GenericParseUInt8Test()
   {
      var result = ParseObject("58", typeof(byte));

      Assert.Equivalent(Option<object>.Some(58), result);
   }

   [Fact]
   public void GenericParseUInt16Test()
   {
      var result = ParseObject("58", typeof(ushort));

      Assert.Equivalent(Option<object>.Some(58), result);
   }

   [Fact]
   public void GenericParseUInt32Test()
   {
      var result = ParseObject("58", typeof(uint));

      Assert.Equivalent(Option<object>.Some(58), result);
   }

   [Fact]
   public void GenericParseUInt64Test()
   {
      var result = ParseObject("58", typeof(ulong));

      Assert.Equivalent(Option<object>.Some(58), result);
   }

   [Fact]
   public void GenericParseDoubleTest()
   {
      var result = ParseObject("58.0", typeof(double));

      Assert.Equivalent(Option<object>.Some(58.0), result);
   }

   [Fact]
   public void GenericParseSingleTest()
   {
      var result = ParseObject("58.0", typeof(float));

      Assert.Equivalent(Option<object>.Some(58.0f), result);
   }

   [Fact]
   public void GenericParseDecimalTest()
   {
      var result = ParseObject("58.0", typeof(decimal));

      Assert.Equivalent(Option<object>.Some(58.0M), result);
   }

   [Fact]
   public void GenericParseGuidTest()
   {
      var result = ParseObject("9655DB059EE3443DA24FF3776A70814E", typeof(Guid));

      Assert.Equivalent(Option<object>.Some(Guid.Parse("9655DB05-9EE3-443D-A24F-F3776A70814E")), result);
   }

   [Fact]
   public void GenericParseDateTimeTest()
   {
      var result = ParseObject("2024-10-08 15:12:47", typeof(DateTime));

      Assert.Equivalent(Option<object>.Some(new DateTime(2024, 10, 8, 15, 12, 47)), result);
   }

   [Fact]
   public void GenericParseTimeTest()
   {
      var result = ParseObject("15:12:47", typeof(TimeOnly));

      Assert.Equivalent(Option<object>.Some(new TimeOnly(15, 12, 47)), result);
   }

   [Fact]
   public void GenericParseDateTest()
   {
      var result = ParseObject("2024-10-08", typeof(DateOnly));

      Assert.Equivalent(Option<object>.Some(new DateOnly(2024, 10, 8)), result);
   }
}