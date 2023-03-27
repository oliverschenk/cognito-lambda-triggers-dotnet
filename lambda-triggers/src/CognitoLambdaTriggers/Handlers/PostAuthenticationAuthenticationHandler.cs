using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class PostAuthenticationAuthenticationHandler : CognitoTriggerHandler<PostAuthenticationEvent>
{
    public const string TRIGGER_SOURCE = "PostAuthentication_Authentication";

    public override string TriggerSource => TRIGGER_SOURCE;

    public PostAuthenticationAuthenticationHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}
