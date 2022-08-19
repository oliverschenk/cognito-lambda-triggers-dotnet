using System.Text.Json.Serialization;

namespace CognitoLambdaTriggers.Events;

public class CustomMessageResponse
{
    [JsonPropertyName("smsMessage")]
    public string SmsMessage { get; set; }
    
    [JsonPropertyName("emailMessage")]
    public string EmailMessage { get; set; }
    
    [JsonPropertyName("emailSubject")]
    public string EmailSubject { get; set; }        
}
