using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class PostConfirmationConfirmForgotPasswordHandler : CognitoTriggerHandler<PostConfirmationEvent>
{
    public const string TRIGGER_SOURCE = "PostConfirmation_ConfirmForgotPassword";

    public override string TriggerSource => TRIGGER_SOURCE;

    public PostConfirmationConfirmForgotPasswordHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}