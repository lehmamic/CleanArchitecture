using System.Collections.Immutable;

namespace CleanArchitecture.SharedKernel.Models;

public class Result
{
    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToImmutableArray();
    }

    public bool Succeeded { get; set; }

    public IImmutableList<string> Errors { get; set; }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
}
