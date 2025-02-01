using Xunit;
using Amazon.Lambda.TestUtilities;

namespace EnglishWordReminder.Lambda.Tests;

public class FunctionTest
{
    [Fact]
    public async Task TestToUpperFunction()
    {
        System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        var upperCase = await function.FunctionHandler(context);

        Assert.Equal("", upperCase);
    }
}
