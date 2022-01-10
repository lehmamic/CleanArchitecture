using Xunit.Abstractions;

namespace CleanArchitecture.Testing.IntegrationTests.Utils.Logging;

public class TestOutputHelperResolver : ITestOutputHelper
{
    private ITestOutputHelper? _testOutput;

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static TestOutputHelperResolver()
    {
    }

    private TestOutputHelperResolver()
    {
    }

    public static TestOutputHelperResolver Instance { get; } = new TestOutputHelperResolver();

    public ITestOutputHelper? TestOutput
    {
        get => _testOutput;
        set
        {
            _testOutput = value;
            if (_testOutput is null)
            {
                return;
            }

#pragma warning disable CA2000
            var converter = new TestOutputTextWriter(_testOutput);
#pragma warning restore CA2000

            Console.SetOut(converter);
            Console.SetError(converter);
        }
    }

    public void WriteLine(string message)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (message is null)
        {
            return;
        }

        TestOutput?.WriteLine(message);
    }

    public void WriteLine(string format, params object[] args)
    {
#pragma warning disable CA1508
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (format is null)
        {
            return;
        }

        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (args is null)
        {
            return;
        }
#pragma warning restore

        TestOutput?.WriteLine(format, args);
    }
}
