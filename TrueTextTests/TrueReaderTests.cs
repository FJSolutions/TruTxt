namespace TrueTextTests;

using TrueText;
using V = TrueText.Validator;

public class TrueReaderTests
{
    private record Person(string Name, int Age);


    [Fact]
    public void MathTrueReaderValidTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required(V.IsInteger()).Apply("58").WithKey("Age");

        results.Match(
            valid: Assert.NotNull,
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void MatchTrueReaderInvalidTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required(V.IsInteger()).Apply("nan").WithKey("Age");

        results.Match(
            valid: _ => Assert.Fail(),
            invalid: Assert.NotNull
        );
    }

    [Fact]
    public void TrueReaderReadStringSuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required(V.IsInteger()).Apply("58").WithKey("Age");

        results.Match(
            valid: reader =>
            {
                var person = new Person(
                    reader.GetString("Name"),
                    reader.GetInt32("Age")
                );

                Assert.NotNull(person);
                Assert.Equal("Francis", person.Name);
                Assert.Equal(58, person.Age);
            },
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadByteTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required(V.IsInteger()).Apply("58").WithKey("Age");

        results.Match(
            valid: reader =>
            {
                var person = new Person(
                    reader.GetString("Name"),
                    reader.GetInt8("Age")
                );

                Assert.NotNull(person);
                Assert.Equal("Francis", person.Name);
                Assert.Equal(58, person.Age);
            },
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadShortTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required(V.IsInteger()).Apply("58").WithKey("Age");

        results.Match(
            valid: reader =>
            {
                var person = new Person(
                    reader.GetString("Name"),
                    reader.GetInt16("Age")
                );

                Assert.NotNull(person);
                Assert.Equal("Francis", person.Name);
                Assert.Equal(58, person.Age);
            },
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadIntUnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58A").WithKey("Age");

        results.Match(
            valid: reader => Assert.Throws<TrueTextException>(() => reader.GetInt32("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadInt8UnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58A").WithKey("Age");

        results.Match(
            valid: reader => Assert.Throws<TrueTextException>(() => reader.GetInt8("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadInt8SuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58").WithKey("Age");

        results.Match(
            valid: reader => Assert.Equal(58, reader.GetInt8("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadInt16UnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58A").WithKey("Age");

        results.Match(
            valid: reader => Assert.Throws<TrueTextException>(() => reader.GetInt16("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadInt16SuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58").WithKey("Age");

        results.Match(
            valid: reader => Assert.Equal(58, reader.GetInt16("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadInt64SuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58").WithKey("Age");

        results.Match(
            valid: reader => Assert.Equal(58, reader.GetInt64("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadInt64UnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58B").WithKey("Age");

        results.Match(
            valid: reader => Assert.Throws<TrueTextException>(() => reader.GetInt64("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadUInt64SuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58").WithKey("Age");

        results.Match(
            valid: reader => Assert.Equal(58U, reader.GetUInt64("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadUInt64UnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58B").WithKey("Age");

        results.Match(
            valid: reader => Assert.Throws<TrueTextException>(() => reader.GetUInt64("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadUInt32SuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58").WithKey("Age");

        results.Match(
            valid: reader => Assert.Equal(58U, reader.GetUInt32("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadUInt32UnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58B").WithKey("Age");

        results.Match(
            valid: reader => Assert.Throws<TrueTextException>(() => reader.GetUInt64("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadUInt16SuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58").WithKey("Age");

        results.Match(
            valid: reader => Assert.Equal(58U, reader.GetUInt16("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadUInt16UnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58B").WithKey("Age");

        results.Match(
            valid: reader => Assert.Throws<TrueTextException>(() => reader.GetUInt16("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadUInt8SuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58").WithKey("Age");

        results.Match(
            valid: reader => Assert.Equal(58U, reader.GetUInt8("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadUInt8UnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("58B").WithKey("Age");

        results.Match(
            valid: reader => Assert.Throws<TrueTextException>(() => reader.GetUInt8("Age")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadDoubleSuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("1.88").WithKey("Height");

        results.Match(
            valid: reader => Assert.Equal(1.88D, reader.GetDouble("Height")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadDoubleUnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("1.88 M").WithKey("Height");

        results.Match(
            valid: reader => Assert.Throws<TrueTextException>(() => reader.GetDouble("Height")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadSingleSuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("1.88").WithKey("Height");

        results.Match(
            valid: reader => Assert.Equal(1.88, reader.GetSingle("Height"), 4),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadSingleUnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("1.88 M").WithKey("Height");

        results.Match(
            valid: reader => Assert.Throws<TrueTextException>(() => reader.GetSingle("Height")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadSingleDecimalTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("1.88").WithKey("Height");

        results.Match(
            valid: reader => Assert.Equal(1.88M, reader.GetDecimal("Height")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadDecimalUnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("1.88 M").WithKey("Height");

        results.Match(
            valid: reader => Assert.Throws<TrueTextException>(() => reader.GetDecimal("Height")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadBoolSuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("true").WithKey("IsActive");

        results.Match(
            valid: reader => Assert.True(reader.GetBoolean("IsActive")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadBoolYesSuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("Yes").WithKey("IsActive");

        results.Match(
            valid: reader => Assert.True(reader.GetBoolean("IsActive")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadBoolOnSuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("On").WithKey("IsActive");

        results.Match(
            valid: reader => Assert.True(reader.GetBoolean("IsActive")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadBoolFalseSuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("False").WithKey("IsActive");

        results.Match(
            valid: reader => Assert.False(reader.GetBoolean("IsActive")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadBoolOffSuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("Off").WithKey("IsActive");

        results.Match(
            valid: reader => Assert.False(reader.GetBoolean("IsActive")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadBoolNoSuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("No").WithKey("IsActive");

        results.Match(
            valid: reader => Assert.False(reader.GetBoolean("IsActive")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadBoolUnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("maybe").WithKey("IsActive");

        results.Match(
            valid: reader => Assert.Throws<TrueTextException>(() => reader.GetBoolean("IsActive")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadGuidSuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("A3976519-1B13-4F5E-8014-6D514F4FF76E").WithKey("ID");

        results.Match(
            valid: reader => Assert.Equal(Guid.Parse("A3976519-1B13-4F5E-8014-6D514F4FF76E"), reader.GetGuid("ID")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadGuidUnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.Required().Apply("A3976519-1B13-4F5E-8014-6D514F5FF76E").WithKey("IsID");

        results.Match(
            valid: reader => Assert.Throws<TrueTextException>(() => reader.GetBoolean("ID")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadDateTimeSuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.IsDateTime("dd/MM/yyyy").Apply("25/10/1965").WithKey("DoB");

        results.Match(
            valid: reader => Assert.Equal(new DateTime(1965, 10, 25), reader.GetDateTime("DoB")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadDateTimeUnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.IsDateTime("dd/MM/yyyy").Apply("1965-10-25").WithKey("DoB");

        results.Match(
            valid: reader => Assert.Fail(),
            invalid: i => Assert.Equal("1965-10-25", i.Get("DoB").Text)
        );
    }

    [Fact]
    public void TrueReaderReadDateSuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.IsDate("MM/dd/yyyy").Apply("10/25/1965").WithKey("DoB");

        results.Match(
            valid: reader => Assert.Equal(new DateOnly(1965, 10, 25), reader.GetDate("DoB")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadDateUnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.IsDateTime("dd/MM/yyyy").Apply("1965-10-25").WithKey("DoB");

        results.Match(
            valid: reader => Assert.Fail(),
            invalid: i => Assert.Equal("1965-10-25", i.Get("DoB").Text)
        );
    }

    [Fact]
    public void TrueReaderReadTimeSuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.IsTime("H:mm").Apply("8:17").WithKey("DoB");

        results.Match(
            valid: reader => Assert.Equal(new TimeOnly(8, 17, 0), reader.GetTime("DoB")),
            invalid: _ => Assert.Fail()
        );
    }

    [Fact]
    public void TrueReaderReadTimeUnsuccessfulTest()
    {
        var results = ResultsCollector<string>.Create("Name", V.Min(3).Apply("Francis"))
                      + V.IsDateTime("HH:mm").Apply("1965-10-25").WithKey("DoB");

        results.Match(
            valid: reader => Assert.Fail(),
            invalid: i => Assert.Equal("1965-10-25", i.Get("DoB").Text)
        );
    }
}