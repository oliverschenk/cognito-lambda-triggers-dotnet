using System.Text.Json.Serialization;

namespace CognitoLambdaTriggers.Events;

public class DefineAuthChallengeResponse
{
    [JsonPropertyName("challengeName")]
    public string ChallengeName { get; set; }
    
    [JsonPropertyName("issueTokens")]
    public bool IssueTokens { get; set; }
    
    [JsonPropertyName("failAuthentication")]
    public bool FailAuthentication { get; set; }
}
