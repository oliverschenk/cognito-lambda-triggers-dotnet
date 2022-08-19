using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class CustomSmsSenderResendCodeHandler : CognitoTriggerHandler<CustomSmsSenderEvent>
{
    public const string TRIGGER_SOURCE = "CustomSMSSender_ResendCode";

    public override string TriggerSource => TRIGGER_SOURCE;

    public CustomSmsSenderResendCodeHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}