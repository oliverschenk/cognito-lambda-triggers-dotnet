using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Handlers;

namespace CognitoLambdaTriggers;

public class CognitoEventHandlerFactory
{
    private ILambdaLogger Logger;

    private static bool IsCognitoTriggerHandler(Type type) =>
        type.IsParticularGeneric(typeof(CognitoTriggerHandler<>));

    public CognitoEventHandlerFactory(ILambdaLogger logger)
    {
        Logger = logger;
    }

    public ICognitoTriggerHandler GetHandler(JsonElement cognitoEvent)
    {
        CognitoTriggerEventBase triggerEvent = cognitoEvent.Deserialize<CognitoTriggerEventBase>();

        Logger.LogDebug($"Trigger source: {triggerEvent.TriggerSource}.");

        var handlers = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.AnyBaseType(IsCognitoTriggerHandler)
                select type;

        foreach (var handler in handlers)
        {
            Logger.LogDebug($"Found handler: '{handler.Name}'");

            var property = handler.GetField("TRIGGER_SOURCE");

            if (property != null)
            {
                Logger.LogDebug("Found TRIGGER_SOURCE property.");

                var value = property.GetValue(null);

                if (value != null && value.ToString() == triggerEvent.TriggerSource)
                {
                    Logger.LogDebug("Trigger source matches event.");
                    Logger.LogInformation($"Using Cognito event handler {handler.Name}.");
                    return (ICognitoTriggerHandler)Activator.CreateInstance(handler, cognitoEvent, Logger);
                }
            }
        }

        Logger.LogWarning($"Could not find a suitable handler for trigger source '{triggerEvent.TriggerSource}'");
        return new NoActionHandler(cognitoEvent, Logger);
    }
}