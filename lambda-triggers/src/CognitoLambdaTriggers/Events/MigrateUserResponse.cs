using System.Text.Json.Serialization;

namespace CognitoLambdaTriggers.Events;

public class MigrateUserResponse
{    
    [JsonPropertyName("finalUserStatus")]
    public string FinalUserStatus { get; set; }

    [JsonPropertyName("messageAction")]
    public string MessageAction { get; set; }

    [JsonPropertyName("desiredDeliveryMediums")]
    public List<string> DesiredDeliveryMediums { get; set; }

    [JsonPropertyName("forceAliasCreation")]
    public bool ForceAliasCreation { get; set; }
}
