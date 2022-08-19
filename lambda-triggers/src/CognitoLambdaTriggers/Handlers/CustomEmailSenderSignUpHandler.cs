using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class CustomEmailSenderSignUpHandler : CognitoTriggerHandler<CustomEmailSenderEvent>
{
    public const string TRIGGER_SOURCE = "CustomEmailSender_SignUp";

    public override string TriggerSource => TRIGGER_SOURCE;

    public CustomEmailSenderSignUpHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}