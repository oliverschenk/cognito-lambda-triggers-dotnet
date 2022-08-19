using System.Text.Json.Serialization;
using CognitoLambdaTriggers.Core;

namespace CognitoLambdaTriggers.Events;

public class CreateAuthChallengeRequest : RequestBase
{
    [JsonPropertyName("clientMetadata")]
    public Dictionary<string, string> ClientMetadata { get; }

    [JsonPropertyName("session")]
    public List<SessionChallengeResult> Session { get; }

    [JsonPropertyName("challengeName")]
    public string ChallengeName { get; }

    [JsonPropertyName("userNotFound")]
    public bool UserNotFound { get; }
}
