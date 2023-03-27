
using System.Text.Json;

namespace CognitoLambdaTriggers.Core;

public interface ICognitoTriggerHandler
{
    string TriggerSource { get; }

    JsonElement HandleTriggerEvent();
    Task<JsonElement> HandleTriggerEventAsync();
}