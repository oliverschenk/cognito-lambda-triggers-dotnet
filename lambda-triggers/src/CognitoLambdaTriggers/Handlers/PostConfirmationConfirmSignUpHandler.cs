using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class PostConfirmationConfirmSignUpHandler : CognitoTriggerHandler<PostConfirmationEvent>
{
    public const string TRIGGER_SOURCE = "PostConfirmation_ConfirmSignUp";

    public override string TriggerSource => TRIGGER_SOURCE;

    public PostConfirmationConfirmSignUpHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
        
    }
}