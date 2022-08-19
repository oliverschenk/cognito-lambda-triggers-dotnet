using System.Text.Json.Serialization;

namespace CognitoLambdaTriggers.Events;

public class VerifyAuthChallengeResponse
{
    [JsonPropertyName("answerCorrect")]
    public bool AnswerCorrect { get; set; }
}
