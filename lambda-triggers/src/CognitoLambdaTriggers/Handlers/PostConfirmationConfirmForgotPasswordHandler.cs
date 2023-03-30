using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using CognitoLambdaTriggers.Core;
using CognitoLambdaTriggers.Events;

namespace CognitoLambdaTriggers.Handlers;

internal class PostConfirmationConfirmForgotPasswordHandler : CognitoTriggerHandler<PostConfirmationEvent>
{
    public const string TRIGGER_SOURCE = "PostConfirmation_ConfirmForgotPassword";

    public override string TriggerSource => TRIGGER_SOURCE;

    public PostConfirmationConfirmForgotPasswordHandler(JsonElement cognitoEvent, ILambdaLogger logger)
        : base(cognitoEvent, logger)
    {
    }

    public async override Task<JsonElement> HandleTriggerEventAsync()
    {
        var userEmailAddress = TriggerEvent.Request.UserAttributes["email"];

        await SendEmailConfirmation(userEmailAddress);

        return await base.HandleTriggerEventAsync();
    }

    private async Task SendEmailConfirmation(string userEmailAddress)
    {
        var source = await getSourceEmail();
        var recipient = userEmailAddress;

        // Setup the email recipients.
        var destination = new Destination()
        {
            ToAddresses = new List<string>() { recipient }
        };

        // Create the email subject.
        var subject = new Content()
        {
            Data = "Password reset confirmation"
        };

        var body = new Body()
        {
            Text = new Content()
            {
                Data = "This is a security notice that your password has been reset."
            }
        };

        var message = new Message()
        {
            Subject = subject,
            Body = body
        };

        // Create and transmit the email to the recipients via Amazon SES.
        var emailContent = new EmailContent()
        {
            Simple = message
        };

        var request = new SendEmailRequest()
        {
            FromEmailAddress = source,
            Destination = destination,
            Content = emailContent
        };

        using (var client = new AmazonSimpleEmailServiceV2Client())
        {
            await client.SendEmailAsync(request);
        }
    }

    private async Task<string> getSourceEmail()
    {
        var client = new AmazonSimpleSystemsManagementClient();

        var request = new GetParameterRequest()
        {
            Name = System.Environment.GetEnvironmentVariable("SOURCE_EMAIL_ADDRESS_PARAMETER")
        };
        
        var result = await client.GetParameterAsync(request);

        return result.Parameter.Value;
    }
}