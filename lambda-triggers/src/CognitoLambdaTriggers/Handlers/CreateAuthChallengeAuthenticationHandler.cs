using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class CreateAuthChallengeAuthenticationHandler : CognitoTriggerHandler<CreateAuthChallengeEvent>
{
    public const string TRIGGER_SOURCE = "CreateAuthChallenge_Authentication";

    public override string TriggerSource => TRIGGER_SOURCE;

    public CreateAuthChallengeAuthenticationHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}