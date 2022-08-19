using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class CustomMessageResendCodeHandler : CognitoTriggerHandler<CustomMessageEvent>
{
    public const string TRIGGER_SOURCE = "CustomMessage_ResendCode";

    public override string TriggerSource => TRIGGER_SOURCE;

    public CustomMessageResendCodeHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }

    // public override async Task<JsonElement> HandleTriggerEvent()
    // {
    //     TriggerEvent.Response.EmailMessage = "This is a test message";
    //     return await Task.FromResult(TriggerEvent);
    // }
}