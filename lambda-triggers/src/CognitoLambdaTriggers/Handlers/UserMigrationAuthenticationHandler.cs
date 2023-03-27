using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class UserMigrationAuthenticationHandler : CognitoTriggerHandler<MigrateUserEvent>
{
    public const string TRIGGER_SOURCE = "UserMigration_Authentication";

    public override string TriggerSource => TRIGGER_SOURCE;

    public UserMigrationAuthenticationHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }
}