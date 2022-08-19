using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class CustomEmailSenderUpdateUserAttributeHandler : CognitoTriggerHandler<CustomEmailSenderEvent>
{
    public const string TRIGGER_SOURCE = "CustomEmailSender_UpdateUserAttribute";

    public override string TriggerSource => TRIGGER_SOURCE;

    public CustomEmailSenderUpdateUserAttributeHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}