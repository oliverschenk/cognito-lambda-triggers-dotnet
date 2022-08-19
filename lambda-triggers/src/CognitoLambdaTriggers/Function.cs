using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Handlers;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AwsCognitoAngular.Triggers;

public class Function
{
    private ILambdaLogger Logger;

    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.CamelCaseLambdaJsonSerializer))]
    public async Task<dynamic> FunctionHandler(JsonElement cognitoEvent, ILambdaContext context)
    {
        Logger = context.Logger;

        Logger.LogDebug("ENVIRONMENT VARIABLES: " + JsonSerializer.Serialize(System.Environment.GetEnvironmentVariables()));
        Logger.LogDebug("CONTEXT: " + JsonSerializer.Serialize(context));

        Logger.LogDebug("EVENT: " + JsonSerializer.Serialize(cognitoEvent));

        return await new CognitoEventHandlerFactory(Logger).GetHandler(cognitoEvent).HandleTriggerEventAsync();
    }
}
