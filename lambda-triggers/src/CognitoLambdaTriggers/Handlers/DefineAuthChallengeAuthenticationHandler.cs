using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

/// <summary>
/// Handles the DefineAuthChallenge_Authentication trigger source
/// </summary
/// <remarks>
/// This example
/// </remarks>
internal class DefineAuthChallengeAuthenticationHandler : CognitoTriggerHandler<DefineAuthChallengeEvent>
{
    public const string TRIGGER_SOURCE = "DefineAuthChallenge_Authentication";

    public override string TriggerSource => TRIGGER_SOURCE;

    public DefineAuthChallengeAuthenticationHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }

    public override JsonElement HandleTriggerEvent()
    {
        if (TriggerEvent.Request.Session.Count == 1 &&
            TriggerEvent.Request.Session[0].ChallengeName == "SRP_A")
        {
            TriggerEvent.Response.IssueTokens = false;
            TriggerEvent.Response.FailAuthentication = false;
            TriggerEvent.Response.ChallengeName = "PASSWORD_VERIFIER";
        }
        else if (TriggerEvent.Request.Session.Count == 2 &&
                    TriggerEvent.Request.Session[1].ChallengeName == "PASSWORD_VERIFIER" &&
                    TriggerEvent.Request.Session[1].ChallengeResult == true)
        {
            TriggerEvent.Response.IssueTokens = false;
            TriggerEvent.Response.FailAuthentication = false;
            TriggerEvent.Response.ChallengeName = "CUSTOM_CHALLENGE";
        }
        else if (TriggerEvent.Request.Session.Count == 3 &&
                    TriggerEvent.Request.Session[2].ChallengeName == "CUSTOM_CHALLENGE" &&
                    TriggerEvent.Request.Session[2].ChallengeResult == true)
        {
            TriggerEvent.Response.IssueTokens = true;
            TriggerEvent.Response.FailAuthentication = false;
        }
        else
        {
            TriggerEvent.Response.IssueTokens = false;
            TriggerEvent.Response.FailAuthentication = true;
        }

        return base.HandleTriggerEvent();
    }
}