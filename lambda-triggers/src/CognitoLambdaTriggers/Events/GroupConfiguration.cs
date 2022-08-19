using System.Text.Json.Serialization;

namespace CognitoLambdaTriggers.Events;

public class GroupConfiguration
{
    [JsonPropertyName("groupsToOverride")]
    public List<string> GroupsToOverride { get; }

    [JsonPropertyName("iamRolesToOverride")]
    public List<string> iamRolesToOverride { get; }

    [JsonPropertyName("preferredRole")]
    public string PreferredRole { get; }
}