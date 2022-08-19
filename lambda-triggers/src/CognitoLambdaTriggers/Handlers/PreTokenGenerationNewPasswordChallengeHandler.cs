using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class PreTokenGenerationNewPasswordChallengeHandler : CognitoTriggerHandler<PreTokenGenerationEvent>
{
    public const string TRIGGER_SOURCE = "TokenGeneration_NewPasswordChallenge";

    public override string TriggerSource => TRIGGER_SOURCE;

    public PreTokenGenerationNewPasswordChallengeHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}