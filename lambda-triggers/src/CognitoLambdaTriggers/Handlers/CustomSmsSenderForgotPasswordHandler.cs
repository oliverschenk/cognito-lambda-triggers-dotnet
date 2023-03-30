using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class CustomSmsSenderForgotPasswordHandler : CognitoTriggerHandler<CustomSmsSenderEvent>
{
    public const string TRIGGER_SOURCE = "CustomSMSSender_ForgotPassword";

    public override string TriggerSource => TRIGGER_SOURCE;

    public CustomSmsSenderForgotPasswordHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}