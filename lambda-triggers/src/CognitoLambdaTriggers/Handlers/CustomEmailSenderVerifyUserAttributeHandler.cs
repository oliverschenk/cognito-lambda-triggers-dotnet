using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class CustomEmailSenderVerifyUserAttributeHandler : CognitoTriggerHandler<CustomEmailSenderEvent>
{
    public const string TRIGGER_SOURCE = "CustomEmailSender_VerifyUserAttribute";

    public override string TriggerSource => TRIGGER_SOURCE;

    public CustomEmailSenderVerifyUserAttributeHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}