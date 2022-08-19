using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class VerifyAuthChallengeResponseAuthenticationHandler : CognitoTriggerHandler<VerifyAuthChallengeEvent>
{
    public const string TRIGGER_SOURCE = "VerifyAuthChallengeResponse_Authentication";

    public override string TriggerSource => TRIGGER_SOURCE;

    public VerifyAuthChallengeResponseAuthenticationHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}