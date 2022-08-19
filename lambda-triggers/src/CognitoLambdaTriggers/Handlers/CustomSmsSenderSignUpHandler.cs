using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class CustomSmsSenderSignUpHandler : CognitoTriggerHandler<CustomSmsSenderEvent>
{
    public const string TRIGGER_SOURCE = "CustomSMSSender_SignUp";

    public override string TriggerSource => TRIGGER_SOURCE;

    public CustomSmsSenderSignUpHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}