using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class CustomSmsSenderVerifyUserAttributeHandler : CognitoTriggerHandler<CustomSmsSenderEvent>
{
    public const string TRIGGER_SOURCE = "CustomSMSSender_VerifyUserAttribute";

    public override string TriggerSource => TRIGGER_SOURCE;

    public CustomSmsSenderVerifyUserAttributeHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}