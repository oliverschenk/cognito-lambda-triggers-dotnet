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
        // This will set a user to be auto confirmed and no confirmation codes will be sent
        TriggerEvent.Response.AutoConfirmUser = true;
    
        // The user will be able to login immediately, but they must verify their email/phone 
        // using other custom flows, such as through an action in an app or website, 
        // or an Admin API call

        return base.HandleTriggerEvent();
    }
}