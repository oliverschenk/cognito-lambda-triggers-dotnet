using System.Text.Json.Serialization;

namespace CognitoLambdaTriggers.Events;

public class SessionChallengeResult
{
    [JsonPropertyName("challengeName")]
    public string ChallengeName { get; }

    [JsonPropertyName("challengeResult")]
    public bool ChallengeResult { get; }

    [JsonPropertyName("challengeMetadata")]
    public string ChallengeMetadata { get; }
}