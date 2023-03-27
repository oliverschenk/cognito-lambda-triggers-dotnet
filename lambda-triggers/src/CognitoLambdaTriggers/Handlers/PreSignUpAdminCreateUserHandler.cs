using System.Text.Json;
using Amazon.Lambda.Core;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class PreSignUpAdminCreateUserHandler : CognitoTriggerHandler<PreSignUpEvent>
{
    public const string TRIGGER_SOURCE = "PreSignUp_AdminCreateUser";

    public override string TriggerSource => TRIGGER_SOURCE;

    public PreSignUpAdminCreateUserHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }

    public override JsonElement HandleTriggerEvent()
    {  
        // In an AdminCreateUser API call the user is automatically confirmed so the
        // response parameters are not used.
        
        // What we can do here is deny the user from being created by throwing an error.
        // You'll implement any logic here to validate the user against say some external directory.
      
        bool validationSucceeded = CheckSomeExternalSystem(TriggerEvent);
        
        if (!validationSucceeded)
        {
            throw new Exception("User could not be created.");
        }

        return base.HandleTriggerEvent();
    }

    private bool CheckSomeExternalSystem(PreSignUpEvent triggerEvent)
    {
        // simulate user verification failure
        return false;
    }
}