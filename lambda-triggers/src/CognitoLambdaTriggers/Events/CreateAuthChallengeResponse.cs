using System.Text.Json.Serialization;

namespace CognitoLambdaTriggers.Events;

public class CreateAuthChallengeResponse
{
    [JsonPropertyName("publicChallengeParameters")]
    public Dictionary<string, string> PublicChallengeParameters { get; }
    
    [JsonPropertyName("privateChallengeParameters")]
    public Dictionary<string, string> PrivateChallengeParameters { get; }
    
    [JsonPropertyName("challengeMetadata")]
    public string ChallengeMetadata { get; set; }
}
