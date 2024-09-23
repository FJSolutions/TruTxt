namespace TruTxtTests;

using TruTxt;
using Xunit.Abstractions;
using static TruTxt.TruParser;

public class TruParserTests(ITestOutputHelper output)
{
    [Fact]
    public void ParseStringTest()
    {
        var result =
            ParseString("").Match(
                some: _ => string.Empty,
                none: () =>
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
                some: value =>
                {
                    Assert.Equal(7, value);
                    return string.Empty;
                },
                none: () =>
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
                some: _ =>
                {
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.Equal(7, value);
                    return string.Empty;
                },
                none: () =>
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
                some: _ =>
                {
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.Equal(7, value);
                    return string.Empty;
                },
                none: () =>
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
                some: _ =>
                {
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.Equal(7, value);
                    return string.Empty;
                },
                none: () =>
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
                some: _ =>
                {
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.Equal(7, value);
                    return string.Empty;
                },
                none: () =>
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
                some: _ =>
                {
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.Equal(7, value);
                    return string.Empty;
                },
                none: () =>
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
                some: _ =>
                {
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.True(7 == value);
                    return string.Empty;
                },
                none: () =>
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
                some: _ =>
                {
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.True(7 == value);
                    return string.Empty;
                },
                none: () =>
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
                some: _ =>
                {
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.Equal(7.0, value, 0.0001);
                    return string.Empty;
                },
                none: () =>
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
                some: _ =>
                {
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.Equal(7.0, value);
                    return string.Empty;
                },
                none: () =>
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
                some: _ =>
                {
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.Equal(7.0M, value);
                    return string.Empty;
                },
                none: () =>
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
                some: _ =>
                {
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.Equal(Guid.Parse("B808889A35454DD7A4C55AF6FBAAD098"), value);
                    return string.Empty;
                },
                none: () =>
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
                some: _ =>
                {
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.Equal(new DateTime(1965, 10, 25, 0, 12, 1), value);
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    output.WriteLine("{0}", value);
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.Equal(new DateOnly(1965, 10, 25), value);
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    output.WriteLine("{0}", value);
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    Assert.Equal(new TimeOnly(23, 59, 59), value);
                    return string.Empty;
                },
                none: () =>
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
                some: value =>
                {
                    output.WriteLine("{0}", value);
                    Assert.Fail();
                    return string.Empty;
                },
                none: () =>
                {
                    Assert.True(true);
                    return string.Empty;
                }
            );

        Assert.Empty(result);
    }
}