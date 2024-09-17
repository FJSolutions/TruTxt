﻿namespace TrueTextTests;

using TrueText;
using V = TrueText.Validator;

public class TrueReaderTests
{
    // private readonly ITestOutputHelper _output;
    //
    // public TrueReaderTests(ITestOutputHelper output)
    // {
    //     this._output = output;
    // }

    private record Person(string Name, int Age);

    // [Fact]
    // public void MathTrueReaderValidTest()
    // {
    //     var results = ResultsCollector.Create("Name", V.Min(3).Apply("Francis"))
    //                   + V.Required(V.IsInteger()).Apply("58").WithKey("Age");
    //
    //     results.Match(
    //         valid: Assert.NotNull,
    //         invalid: _ => Assert.Fail()
    //     );
    // }
    //
    // [Fact]
    // public void MatchTrueReaderInvalidTest()
    // {
    //     var results = ResultsCollector.Create("Name", V.Min(3).Apply("Francis"))
    //                   + V.Required(V.IsInteger()).Apply("nan").WithKey("Age");
    //
    //     results.Match(
    //         valid: _ => Assert.Fail(),
    //         invalid: Assert.NotNull
    //     );
    // }

    [Fact]
    public void TrueReaderReadStringSuccessfulTest()
    {
        var results = ResultsCollector.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required(V.IsInteger()).Apply("58").WithKey("Age");

        results.MapWithReader<Person, string>(
            reader =>
            {
                var result =
                    from name in reader.GetString("Name")
                    from age in reader.GetInt32("Age")
                    select new Person(name, age);

                if (result is Ok<Person> ok)
                {
                    var person = ok.Value;

                    return person;
                }

                Assert.Fail();

                return new Person(string.Empty, 0);
            },
            valid: person =>
            {
                Assert.Equal("Francis", person.Name);
                Assert.Equal(58, person.Age);

                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);

                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadByteTest()
    {
        var results = ResultsCollector.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required(V.IsInteger()).Apply("58").WithKey("Age");

        results.MapWithReader<Person, string>(
            reader =>
                from name in reader.GetString("Name")
                from age in reader.GetInt8("Age")
                select new Person(name, age),
            valid: person =>
            {
                Assert.Equal("Francis", person.Name);
                Assert.Equal(58, person.Age);

                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);

                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadShortTest()
    {
        var results = ResultsCollector.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required(V.IsInteger()).Apply("58").WithKey("Age");

        results.MapWithReader<Person, string>(
            reader =>
                from name in reader.GetString("Name")
                from age in reader.GetInt16("Age")
                select new Person(name, age),
            valid: person =>
            {
                Assert.Equal("Francis", person.Name);
                Assert.Equal(58, person.Age);

                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);

                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadIntUnsuccessfulTest()
    {
        var results = ResultsCollector.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58A").WithKey("Age");

        results.MapWithReader<Person, string>(
            reader =>
                from name in reader.GetString("Name")
                from age in reader.GetInt32("Age")
                select new Person(name, age),
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // Int8
    [Fact]
    public void TrueReaderReadInt8SuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58"));

        results.MapWithReader<sbyte, string>(
            reader => from age in reader.GetInt8("Age") select age,
            valid: age =>
            {
                Assert.Equal(58, age);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadInt8UnsuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58A"));

        results.MapWithReader<sbyte, string>(
            reader => from age in reader.GetInt8("Age") select age,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // Int16
    [Fact]
    public void TrueReaderReadInt16SuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58"));

        results.MapWithReader<short, string>(
            reader => from age in reader.GetInt16("Age") select age,
            valid: age =>
            {
                Assert.Equal(58, age);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadInt16UnsuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58A"));

        results.MapWithReader<short, string>(
            reader => from age in reader.GetInt16("Age") select age,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // Int64
    [Fact]
    public void TrueReaderReadInt64SuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58"));

        results.MapWithReader(
            reader => from age in reader.GetInt64("Age") select age,
            valid: age =>
            {
                Assert.Equal(58, age);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadInt64UnsuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58A"));

        results.MapWithReader(
            reader => from age in reader.GetInt64("Age") select age,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // UInt8
    [Fact]
    public void TrueReaderReadUInt8SuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58"));

        results.MapWithReader(
            reader => from age in reader.GetUInt8("Age") select age,
            valid: age =>
            {
                Assert.Equal(58, age);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadUint8UnsuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58A"));

        results.MapWithReader(
            reader => from age in reader.GetUInt8("Age") select age,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // UInt16
    [Fact]
    public void TrueReaderReadUInt16SuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58"));

        results.MapWithReader(
            reader => from age in reader.GetUInt16("Age") select age,
            valid: age =>
            {
                Assert.Equal(58, age);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadUint16UnsuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58A"));

        results.MapWithReader(
            reader => from age in reader.GetUInt16("Age") select age,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // UInt32
    [Fact]
    public void TrueReaderReadUInt32SuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58"));

        results.MapWithReader(
            reader => from age in reader.GetUInt32("Age") select age,
            valid: age =>
            {
                Assert.True(58 == age);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadUint32UnsuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58A"));

        results.MapWithReader(
            reader => from age in reader.GetUInt32("Age") select age,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // UInt64
    [Fact]
    public void TrueReaderReadUInt64SuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58"));

        results.MapWithReader(
            reader => from age in reader.GetUInt64("Age") select age,
            valid: age =>
            {
                Assert.True(58 == age);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadUint64UnsuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58A"));

        results.MapWithReader(
            reader => from age in reader.GetUInt64("Age") select age,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // Single
    [Fact]
    public void TrueReaderReadSingleSuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58.0"));

        results.MapWithReader(
            reader => from age in reader.GetSingle("Age") select age,
            valid: age =>
            {
                Assert.Equal(58.0, age, 0.0001);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadSingleUnsuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58.A"));

        results.MapWithReader(
            reader => from age in reader.GetSingle("Age") select age,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // Double
    [Fact]
    public void TrueReaderReadDoubleSuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58.0"));

        results.MapWithReader(
            reader => from age in reader.GetDouble("Age") select age,
            valid: age =>
            {
                Assert.Equal(58.0, age);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadDoubleUnsuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58.A"));

        results.MapWithReader(
            reader => from age in reader.GetDouble("Age") select age,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // Decimal
    [Fact]
    public void TrueReaderReadDecimalSuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58.0"));

        results.MapWithReader(
            reader => from age in reader.GetDecimal("Age") select age,
            valid: age =>
            {
                Assert.Equal(58M, age);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadDecimalUnsuccessfulTest()
    {
        var results = ResultsCollector.Create("Age", V.Required().Apply("58.A"));

        results.MapWithReader(
            reader => from age in reader.GetDecimal("Age") select age,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // DateTime
    [Fact]
    public void TrueReaderReadDateTimeSuccessfulTest()
    {
        var results =
            ResultsCollector.Create("DoB", V.IsDateTime("MMM dd yyyy hh:mm tt").Apply("Oct 25 1965 12:01 pm"));

        results.MapWithReader(
            reader => from age in reader.GetDateTime("DoB") select age,
            valid: birthday =>
            {
                Assert.Equal(new DateTime(1965, 10, 25, 12, 1, 0), birthday);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadDateTimeUnsuccessfulTest()
    {
        var results =
            ResultsCollector.Create("DoB", V.IsDateTime("MMM dd yyyy hh:mm tt").Apply("Oct 25 1965 13:01 pm"));

        results.MapWithReader(
            reader => from age in reader.GetDateTime("DoB") select age,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // Date
    [Fact]
    public void TrueReaderReadDateSuccessfulTest()
    {
        var results = ResultsCollector.Create("DoB", V.IsDate("MMM dd yyyy").Apply("Oct 25 1965"));

        results.MapWithReader(
            reader => from age in reader.GetDate("DoB") select age,
            valid: birthday =>
            {
                Assert.Equal(new DateOnly(1965, 10, 25), birthday);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadDateUnsuccessfulTest()
    {
        var results = ResultsCollector.Create("DoB", V.IsDate("MMM dd yyyy").Apply("10 25 1965"));

        results.MapWithReader(
            reader => from age in reader.GetDate("DoB") select age,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // Time
    [Fact]
    public void TrueReaderReadTimeSuccessfulTest()
    {
        var results = ResultsCollector.Create("DoB", V.IsTime("HH:mm:ss").Apply("13:15:01"));

        results.MapWithReader(
            reader => from age in reader.GetTime("DoB") select age,
            valid: birthday =>
            {
                Assert.Equal(new TimeOnly(13, 15, 01), birthday);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadTimeUnsuccessfulTest()
    {
        var results = ResultsCollector.Create("DoB", V.IsTime("hh:mm:ss").Apply("13:15:01"));

        results.MapWithReader(
            reader => from age in reader.GetTime("DoB") select age,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }

    // GUID
    [Fact]
    public void TrueReaderReadGuidSuccessfulTest()
    {
        var results = ResultsCollector.Create("ID", V.Required().Apply("6F59801E-1125-48D3-A458-8BAB82A2B4F9"));

        results.MapWithReader(
            reader => from id in reader.GetGuid("ID") select id,
            valid: id =>
            {
                Assert.Equal(Guid.Parse("6F59801E-1125-48D3-A458-8BAB82A2B4F9"), id);
                return string.Empty;
            },
            invalid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            }
        );
    }

    [Fact]
    public void TrueReaderReadGuidUnsuccessfulTest()
    {
        var results = ResultsCollector.Create("ID", V.Required().Apply("6F59801E-1125-48D3-A458-8BAB82A2B4FZ"));

        results.MapWithReader(
            reader => from id in reader.GetGuid("ID") select id,
            valid: _ =>
            {
                Assert.Fail();
                return string.Empty;
            },
            invalid: r =>
            {
                Assert.NotEmpty(r);
                return string.Empty;
            }
        );
    }
}