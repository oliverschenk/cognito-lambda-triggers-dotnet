using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class PreSignUpSignUpHandler : CognitoTriggerHandler<PreSignUpEvent>
{
    public const string TRIGGER_SOURCE = "PreSignUp_SignUp";

    public override string TriggerSource => TRIGGER_SOURCE;

    public PreSignUpSignUpHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }

    public override JsonElement HandleTriggerEvent()
    {
        TriggerEvent.Response.AutoConfirmUser = true;

        return base.HandleTriggerEvent();
    }
}