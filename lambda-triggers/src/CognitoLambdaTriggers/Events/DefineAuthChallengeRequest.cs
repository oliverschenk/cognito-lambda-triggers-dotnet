using System.Text.Json.Serialization;
using CognitoLambdaTriggers.Core;

namespace CognitoLambdaTriggers.Events;

public class DefineAuthChallengeRequest : RequestBase
{
    [JsonPropertyName("clientMetadata")]
    public Dictionary<string, string> ClientMetadata { get; }

    [JsonPropertyName("session")]
    public List<SessionChallengeResult> Session { get; }

    [JsonPropertyName("userNotFound")]
    public bool UserNotFound { get; }
}
