# Amazon Cognito Lambda Triggers in Dotnet

This repo contains the code for the Medium article [Cognito Lambda Triggers in Dotnet](https://medium.com/@oliver.schenk/cognito-lambda-triggers-in-dotnet-3bf13a55eda3)

It also relates to the following articles:

- [Cognito Triggers Deep Dive — PreSignUp](https://medium.com/@oliver.schenk/cognito-lambda-triggers-in-dotnet-presignup-4c465c6f81a)
- [Cognito Lambda Triggers in Dotnet — PostConfirmation](https://medium.com/@oliver.schenk/cognito-lambda-triggers-in-dotnet-postconfirmation-f1419f4d4952)

Please note that the AWS resources created by this project MAY NOT be free. Deploy at your own risk and destroy when no longer needed.

This project is a Cognito and Lambda implementation template for handling Cognito triggers. All possible handlers have been defined and you can implement your custom logic for as many of them as you need. Some handlers have been implemented as part of the articles listed above.

## Prerequisites

- Terraform
- Dotnet 6.0 SDK
- AWS account (non-production) with Administrator access

## Back-end Infrastructure

The infrastructure to be deployed by this repo is defined using Terraform. When applied it creates a user pool and a user pool client as well as the necessary parts for handling Cognito Triggers using Lambda functions.

Services used include:

- Cognito
- Lambda
- IAM
- KMS
- SES
- SSM Parameter Store
- DynamoDB

## Example Code Provided

In this project there are working sample implementations for the following handlers:

- PostConfirmationConfirmForgotPasswordHandler
- PostConfirmationConfirmSignUpHandler
- PreSignUpAdminCreateUserHandler
- PreSignUpExternalProviderHandler
- PreSignUpSignUpHandler

To ensure placeholder handlers do not get used, the handlers have been commented out in the Terraform Cognito module. Depending on which trigger article you are reading and testing, you should remove the comment for the appropriate handler in the Terraform `main.tf` file.

It is not recommended to enable handlers that you have not reviewed and adjusted to your needs, because they may interfere with each other. For example, if you leave `AutoApprove = true` in the pre sign-up handler, then the user will be auto verified and you won't get a code that you can test in the post confirmation handler. Another example is, if you include the custom email or SMS senders you will not receive any emails at all as they do not currently have an implementation.

## Implementing Cognito Triggers

To implement a Cognito trigger yourself, find the relevant Handler class and implement the `HandleTriggerEvent` method. This implementation should return with `base.HandleTriggerEvent()`. The `NoActionHandler` is an example of a handler implementation that simply returns the event that was received.

If you want an `async` call then override the `HandleTriggerEventAsync` method instead.

## Deployment

You can deploy the infrastructure in the project manually or automatically using the `deploy.sh` script. Both methods are outlined below.

### Set your validated email address

Some of the handlers make use of the AWS SES service, which requires a validated email identity even in sandbox mode. Please configure your own email address in the `deploy.sh` script, or provide the `email_identity` variable if running Terraform manually.

### Manual deployment

This method assumes you have credentials set up appropriately.

```
cd infrastructure
terraform init
terraform apply

cd lambda-triggers/src/CognitoLambdaTriggers
dotnet restore
dotnet build -c Debug
dotnet lambda package -c Debug -o ../../output/CognitoLambdaTriggers.zip
```

### Automatic deployment using script

You can configure the default region in the `deploy.sh` file or specify a region by using the `-r` flag as shown below.

You must pass in a test email address. This will be used to create an email identity in AWS SES for sending emails from Lambda via SES. AWS will send you a verification link after deployment.


```
./deploy.sh

DESCRIPTION:
  Script for deploying resources to AWS.

USAGE:
  deploy.sh -e <email_address> [-p aws_profile] [-r region] [-s stage]

OPTIONS
  -e   email address for testing
  -p   aws_profile (default: none)
  -r   region (default: ap-southeast-2)
  -s   the stage to deploy [dev, test, prod] (default: dev)
```

To apply
```
./deploy.sh
```

To destroy
```
./destroy.sh
```
