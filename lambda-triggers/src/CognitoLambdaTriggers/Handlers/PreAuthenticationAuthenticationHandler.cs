using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class PreAuthenticationAuthenticationHandler : CognitoTriggerHandler<PreAuthenticationEvent>
{
    public const string TRIGGER_SOURCE = "PreAuthentication_Authentication";

    public override string TriggerSource => TRIGGER_SOURCE;

    public PreAuthenticationAuthenticationHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}