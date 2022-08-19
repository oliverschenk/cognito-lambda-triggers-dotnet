# Amazon Cognito Lambda Triggers in Dotnet

This repo contains the code for the Medium article [Cognito Lambda Triggers in Dotnet](https://medium.com/@oliver.schenk/cognito-lambda-triggers-in-dotnet-3bf13a55eda3)

Please note that the AWS resources created by this project are NOT free. Deploy at your own risk and destroy when no longer needed.Cognito Lambda Triggers in Dotnet

This project is a working Cognito and Lambda implementation template for handling Cognito triggers. All possible handlers have been defined and you can implement your custom logic for as many of them as you need.

## Prerequisites

- Terraform
- Dotnet 6.0 SDK
- AWS account with Administrator access
- aws-vault (only required if using deployment script `deploy.sh`)

## Back-end Infrastructure

The infrastructure to be deployed by this repo is defined using Terraform. When applied it creats a user pool and a user pool client as well as the necessary parts for handling Cognito Triggers using Lambda functions.

## Implementing Cognito Triggers

To implement a Cognito trigger, find the relevant Handler class and implement the `HandleTrigger` method. This implementation should return with `base.HandleTrigger()`. THe `NoActionHandler` is an example of a handler implementation that simply returns the event that was received.

## Deployment
### Running terraform manually

This method assumes you have credentials set up appropriately.

```
cd infrastructure
terraform init
terraform apply
```

### Building and packaging

```
cd lambda-triggers/src/CognitoLambdaTriggers
dotnet restore
dotnet build -c Debug
dotnet lambda package -c Debug -o ../../output/CognitoLambdaTriggers.zip
```

### Using the deploy script

This method assumes you have aws-vault configured.

You can configure the default region in the `deploy.sh` file or specify a region by using the `-r` flag as shown below.


```
./deploy.sh

DESCRIPTION:
  Script for deploying serverless lambda.

USAGE:
  deploy.sh -p credentials_profile [-r region] [-s stage]

OPTIONS
  -p   the credentials profile to use (uses aws-vault)
  -r   region (default: ap-southeast-2)
  -s   the stage to deploy [dev, test, prod] (default: dev)
```

To apply
```
./deploy.sh -p <aws_vault_profile>
```

To destroy
```
./destroy.sh -p <aws_vault_profile>
```
