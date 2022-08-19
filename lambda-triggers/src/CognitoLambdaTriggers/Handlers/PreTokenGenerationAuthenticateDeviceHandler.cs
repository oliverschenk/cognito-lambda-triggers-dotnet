using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class PreTokenGenerationAuthenticateDeviceHandler : CognitoTriggerHandler<PreTokenGenerationEvent>
{
    public const string TRIGGER_SOURCE = "TokenGeneration_AuthenticateDevice";

    public override string TriggerSource => TRIGGER_SOURCE;

    public PreTokenGenerationAuthenticateDeviceHandler(JsonElement cognitoElement, ILambdaLogger logger)
        : base(cognitoElement, logger)
    {
    }
}