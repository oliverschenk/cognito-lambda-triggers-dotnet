using System.Text.Json.Serialization;

namespace CognitoLambdaTriggers.Events;

public class ClaimsOverrideDetails
{
    [JsonPropertyName("claimsToAddOrOverride")]
    public Dictionary<string, string> ClaimsToAddOrOverride { get; }

    [JsonPropertyName("claimsToSuppress")]
    public List<string> ClaimsToSuppress { get; }

    [JsonPropertyName("groupOverrideDetails")]
    public List<GroupConfiguration> GroupOverrideDetails { get; }
}