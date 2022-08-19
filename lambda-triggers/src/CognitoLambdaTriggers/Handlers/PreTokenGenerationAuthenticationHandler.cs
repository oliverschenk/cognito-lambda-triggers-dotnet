using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class PreTokenGenerationAuthenticationHandler : CognitoTriggerHandler<PreTokenGenerationEvent>
{
    public const string TRIGGER_SOURCE = "TokenGeneration_Authentication";

    public override string TriggerSource => TRIGGER_SOURCE;

    public PreTokenGenerationAuthenticationHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}