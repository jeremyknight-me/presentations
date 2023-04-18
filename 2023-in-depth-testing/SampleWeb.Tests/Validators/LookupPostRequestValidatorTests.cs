using FluentValidation.TestHelper;
using SampleWeb.Requests;
using SampleWeb.Validators;

namespace SampleWeb.Tests.Validators;

public class LookupPostRequestValidatorTests
{
    private readonly LookupPostRequestValidator sut;

    public LookupPostRequestValidatorTests()
    {
        this.sut = new();
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("abc 123")]
    public void Validate_NoErrors(string name)
    {
        var request = new LookupPostRequest { Name = name };
        var result = this.sut.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")]
    public void Validate_Errors(string name)
    {
        var request = new LookupPostRequest { Name = name };
        var result = this.sut.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}
