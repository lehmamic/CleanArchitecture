using System.Text;
using Xunit.Abstractions;

namespace CleanArchitecture.Testing.IntegrationTests.Utils.Logging;

public class TestOutputTextWriter : TextWriter
{
    private readonly ITestOutputHelper _output;

    public TestOutputTextWriter(ITestOutputHelper output)
    {
        _output = output;
    }

    public override Encoding Encoding => Encoding.UTF8;

    public override void WriteLine(string? value)
    {
        if (value is null)
        {
            return;
        }

        try
        {
            _output.WriteLine(value);
        }
        catch (InvalidOperationException)
        {
            // it can happen that PACT writes logs in a test preparation part which will cause an exception in xunit
        }
    }

    public override void WriteLine(string format, params object?[] arg)
    {
#pragma warning disable CA1508
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (format is null)
        {
            return;
        }

        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (arg is null)
        {
            return;
        }
#pragma warning restore

        try
        {
            _output.WriteLine(format, arg);
        }
        catch (InvalidOperationException)
        {
            // it can happen that PACT writes logs in a test preparation part which will cause an exception in xunit
        }
    }

    public override void Flush()
    {
    }
}
