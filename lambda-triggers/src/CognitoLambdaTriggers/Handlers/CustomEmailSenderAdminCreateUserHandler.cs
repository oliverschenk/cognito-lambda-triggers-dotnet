using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class CustomEmailSenderAdminCreateUserHandler : CognitoTriggerHandler<CustomEmailSenderEvent>
{
    public const string TRIGGER_SOURCE = "CustomEmailSender_AdminCreateUser";

    public override string TriggerSource => TRIGGER_SOURCE;

    public CustomEmailSenderAdminCreateUserHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}