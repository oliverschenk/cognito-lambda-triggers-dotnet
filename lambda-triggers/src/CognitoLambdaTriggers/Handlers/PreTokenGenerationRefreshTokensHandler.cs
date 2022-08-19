using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class PreTokenGenerationRefreshTokensHandler : CognitoTriggerHandler<PreTokenGenerationEvent>
{
    public const string TRIGGER_SOURCE = "TokenGeneration_RefreshTokens";

    public override string TriggerSource => TRIGGER_SOURCE;

    public PreTokenGenerationRefreshTokensHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}