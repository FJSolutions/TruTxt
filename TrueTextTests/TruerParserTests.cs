using Xunit.Abstractions;

namespace TrueTextTests;

using static TrueText.TrueParser;

public class TruerParserTests
{
    private readonly ITestOutputHelper _output;
    
    public TruerParserTests(ITestOutputHelper output)
    {
        this._output = output;
    }

    [Fact]
    public void ParseStringTest()
    {
        ParseString("").Match(
            some: s => string.Empty,
            none: () =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void ParseSomeInt8Test()
    {
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
    }

    [Fact]
    public void ParseNoneInt8Test()
    {
        ParseInt8("A").Match(
            some: value =>
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
    }

    [Fact]
    public void ParseNoneInt16Test()
    {
        ParseInt16("A").Match(
            some: value =>
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
    public void ParseSomeInt32Test()
    {
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
    }

    [Fact]
    public void ParseNoneInt32Test()
    {
        ParseInt32("A").Match(
            some: value =>
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
    public void ParseSomeInt64Test()
    {
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
    }

    [Fact]
    public void ParseNoneInt64Test()
    {
        ParseInt64("A").Match(
            some: value =>
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
    public void ParseSomeUInt8Test()
    {
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
    }

    [Fact]
    public void ParseNoneUInt8Test()
    {
        ParseUInt8("A").Match(
            some: value =>
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
    public void ParseSomeUInt16Test()
    {
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
    }

    [Fact]
    public void ParseNoneUInt16Test()
    {
        ParseUInt16("A").Match(
            some: value =>
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
    public void ParseSomeUInt32Test()
    {
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
    }

    [Fact]
    public void ParseNoneUInt32Test()
    {
        ParseUInt32("A").Match(
            some: value =>
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
    public void ParseSomeUInt64Test()
    {
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
    }

    [Fact]
    public void ParseNoneUInt64Test()
    {
        ParseUInt64("A").Match(
            some: value =>
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
    public void ParseSomeFloatTest()
    {
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
    }

    [Fact]
    public void ParseNoneFloatTest()
    {
        ParseUInt64("A").Match(
            some: value =>
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
    public void ParseSomeDoubleTest()
    {
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
    }

    [Fact]
    public void ParseNoneDoubleTest()
    {
        ParseUInt64("A").Match(
            some: value =>
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
    public void ParseSomeDecimalTest()
    {
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
    }

    [Fact]
    public void ParseNoneDecimalTest()
    {
        ParseUInt64("A").Match(
            some: value =>
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
    public void ParseSomeGuidTest()
    {
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
    }

    [Fact]
    public void ParseNoneGuidTest()
    {
        ParseUInt64("B808889A-3545-4DD7-A4C5-5AF6FBAAD09Z").Match(
            some: value =>
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
    public void ParseSomeDateTimeTest()
    {
        ParseDateTime("1965-10-25 00:12:01").Match(
            some: value =>
            {
                Assert.Equal(new DateTime(1965, 10, 25, 0,12,1), value);
                return string.Empty;
            },
            none: () =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void ParseNoneDateTimeTest()
    {
        ParseDateTime("Oct 25 1965 25:12:01").Match(
            some: value =>
            {
                this._output.WriteLine("{0}", value);
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
    public void ParseSomeDateTest()
    {
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
    }

    [Fact]
    public void ParseNoneDateTest()
    {
        ParseDate("Oct 32 1965").Match(
            some: value =>
            {
                this._output.WriteLine("{0}", value);
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
    public void ParseSomeTimeTest()
    {
        ParseTime("23:59:59").Match(
            some: value =>
            {
                Assert.Equal(new TimeOnly(23,59,59), value);
                return string.Empty;
            },
            none: () =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void ParseNonTimeTest()
    {
        ParseTime("23:59:60").Match(
            some: value =>
            {
                this._output.WriteLine("{0}", value);
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
}