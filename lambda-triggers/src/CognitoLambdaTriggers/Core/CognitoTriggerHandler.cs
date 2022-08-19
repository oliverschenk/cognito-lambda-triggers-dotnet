using System.Text.Json;
using Amazon.Lambda.Core;

namespace CognitoLambdaTriggers.Core;

internal abstract class CognitoTriggerHandler<T> : ICognitoTriggerHandler where T : ICognitoTriggerEvent
{
    public T TriggerEvent { get; }

    public abstract string TriggerSource { get; }

    protected ILambdaLogger Logger { get; }

    protected CognitoTriggerHandler(JsonElement cognitoEvent, ILambdaLogger logger)
    {
        TriggerEvent = cognitoEvent.Deserialize<T>();

        Logger = logger;
    }

    public virtual JsonElement HandleTriggerEvent()
    {
        Logger.LogInformation($"Handling Cognito event trigger source '${TriggerSource}'.");

        Logger.LogDebug(JsonSerializer.Serialize(TriggerEvent));

        return JsonSerializer.SerializeToElement(TriggerEvent);
    }

    public virtual async Task<JsonElement> HandleTriggerEventAsync()
    {
        return await Task.FromResult(HandleTriggerEvent());
    }
}