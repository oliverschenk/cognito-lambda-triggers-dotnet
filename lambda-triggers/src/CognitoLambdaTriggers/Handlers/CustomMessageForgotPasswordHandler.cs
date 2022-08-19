using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class CustomMessageForgotPasswordHandler : CognitoTriggerHandler<CustomMessageEvent>
{
    public const string TRIGGER_SOURCE = "CustomMessage_ForgotPassword";

    public override string TriggerSource => TRIGGER_SOURCE;

    public CustomMessageForgotPasswordHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }

    // public override async Task<JsonElement> HandleTriggerEvent()
    // {
    //     TriggerEvent.Response.EmailMessage = "This is a test message";
    //     return await Task.FromResult(TriggerEvent);
    // }
}