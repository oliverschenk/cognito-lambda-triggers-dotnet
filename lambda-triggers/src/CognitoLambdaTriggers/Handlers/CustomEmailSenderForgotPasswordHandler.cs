using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class CustomEmailSenderForgotPasswordHandler : CognitoTriggerHandler<CustomEmailSenderEvent>
{
    public const string TRIGGER_SOURCE = "CustomEmailSender_ForgotPassword";

    public override string TriggerSource => TRIGGER_SOURCE;

    public CustomEmailSenderForgotPasswordHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}