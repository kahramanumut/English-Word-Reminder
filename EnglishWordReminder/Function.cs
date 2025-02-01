using Amazon.Lambda.Core;
using EnglishWordReminder.Manager;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace EnglishWordReminder;

public class Function
{
    private BotManager _botManager;

    public Function()
    {
        var startup = new Startup();
        IServiceProvider provider = startup.ConfigureServices();

        _botManager = provider.GetRequiredService<BotManager>();
    }


    public async Task<string> FunctionHandler(ILambdaContext context)
    {
        await _botManager.SendWordQuestion();

        return "";
    }
}
