using CleanArchitecture.Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace CleanArchitecture.Application.Tests.Common.Exceptions;

public class ValidationExceptionTest
{
    [Fact]
    public void Constructor_WithoutParameters_CreatesAnEmptyErrorDictionary()
    {
        // act
        var exception = new ValidationException();

        // assert
        exception.Errors.Keys.Should().BeEquivalentTo(Array.Empty<string>());
    }

    [Fact]
    public void Constructor_WithSingleValidationFailure_CreatesASingleElementErrorDictionary()
    {
        // arrange
        var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Age", "must be over 18"),
            };

        // act
        var exception = new ValidationException(failures);

        // assert
        exception.Errors.Keys.Should().BeEquivalentTo("Age");
        exception.Errors["Age"].Should().BeEquivalentTo("must be over 18");
    }

    [Fact]
    public void Constructor_WithMultipleValidationFailureForMultipleProperties_CreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        // arrange
        var failures = new List<ValidationFailure>
            {
                new("Age", "must be 18 or older"),
                new("Age", "must be 25 or younger"),
                new("Password", "must contain at least 8 characters"),
                new("Password", "must contain a digit"),
                new("Password", "must contain upper case letter"),
                new("Password", "must contain lower case letter"),
            };

        // act
        var exception = new ValidationException(failures);

        // assert
        exception.Errors.Keys.Should().BeEquivalentTo("Password", "Age");

        exception.Errors["Age"].Should().BeEquivalentTo("must be 25 or younger", "must be 18 or older");

        exception.Errors["Password"].Should().BeEquivalentTo("must contain lower case letter", "must contain upper case letter", "must contain at least 8 characters", "must contain a digit");
    }
}
