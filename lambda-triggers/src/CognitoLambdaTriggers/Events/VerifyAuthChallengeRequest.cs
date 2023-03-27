using System.Text.Json.Serialization;
using CognitoLambdaTriggers.Core;

namespace CognitoLambdaTriggers.Events;

public class VerifyAuthChallengeRequest : RequestBase
{
    [JsonPropertyName("clientMetadata")]
    public Dictionary<string, string> ClientMetadata { get; }

    [JsonPropertyName("privateChallengeParameters")]
    public Dictionary<string, string> PrivateChallengeParameters { get; }

    [JsonPropertyName("challengeAnswer")]
    public string ChallengeAnswer { get; }

    [JsonPropertyName("userNotFound")]
    public bool UserNotFound { get; }
}
