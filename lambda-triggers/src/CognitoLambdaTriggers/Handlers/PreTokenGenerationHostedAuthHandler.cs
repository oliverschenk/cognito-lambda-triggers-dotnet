using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class PreTokenGenerationHostedAuthHandler : CognitoTriggerHandler<PreTokenGenerationEvent>
{
    public const string TRIGGER_SOURCE = "TokenGeneration_HostedAuth";

    public override string TriggerSource => TRIGGER_SOURCE;

    public PreTokenGenerationHostedAuthHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}