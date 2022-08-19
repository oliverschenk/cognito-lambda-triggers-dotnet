using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class PreSignUpExternalProviderHandler : CognitoTriggerHandler<PreSignUpEvent>
{
    public const string TRIGGER_SOURCE = "PreSignUp_ExternalProvider";

    public override string TriggerSource => TRIGGER_SOURCE;
    
    public PreSignUpExternalProviderHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}