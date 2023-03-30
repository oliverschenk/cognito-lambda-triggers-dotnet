data "aws_caller_identity" "current" {}

locals {
  id = "${var.stage}-${var.project_name}"

  lambda_archive_path = "../lambda-triggers/output/CognitoLambdaTriggers.zip"
}

resource "random_string" "external_id" {
  length  = 12
  special = false
}

resource "aws_cognito_user_pool" "cognito_user_pool" {
  name = "${local.id}-user-pool"

  username_attributes      = ["email", "phone_number"]
  auto_verified_attributes = ["email"]

  sms_verification_message = "Your account activation code is {####}"

  email_verification_subject = "Account verification code"
  email_verification_message = "Your account activation code is {####}"

  admin_create_user_config {
    allow_admin_create_user_only = false
  }

  sms_configuration {
    sns_caller_arn = aws_iam_role.cognito_sms.arn
    external_id    = random_string.external_id.result
  }

  password_policy {
    minimum_length                   = 10
    require_lowercase                = true
    require_uppercase                = true
    require_numbers                  = true
    require_symbols                  = false
    temporary_password_validity_days = 2
  }

  account_recovery_setting {
    recovery_mechanism {
      name     = "verified_email"
      priority = 1
    }
    recovery_mechanism {
      name     = "verified_phone_number"
      priority = 2
    }
  }

  schema {
    name                     = "name"
    attribute_data_type      = "String"
    developer_only_attribute = false
    mutable                  = true
    required                 = true

    string_attribute_constraints {
      min_length = 0
      max_length = 1048
    }
  }

  schema {
    name                     = "email"
    attribute_data_type      = "String"
    developer_only_attribute = false
    mutable                  = true
    required                 = true

    string_attribute_constraints {
      min_length = 0
      max_length = 256
    }
  }

  schema {
    name                     = "phone_number"
    attribute_data_type      = "String"
    developer_only_attribute = false
    mutable                  = true
    required                 = true

    string_attribute_constraints {
      min_length = 0
      max_length = 12
    }
  }

  lambda_config {
    # custom_message                 = aws_lambda_function.lambda_function_trigger.arn
    # post_authentication            = aws_lambda_function.lambda_function_trigger.arn
    # post_confirmation              = aws_lambda_function.lambda_function_trigger.arn
    # pre_authentication             = aws_lambda_function.lambda_function_trigger.arn
    # pre_sign_up                    = aws_lambda_function.lambda_function_trigger.arn
    # pre_token_generation           = aws_lambda_function.lambda_function_trigger.arn
    # create_auth_challenge          = aws_lambda_function.lambda_function_trigger.arn
    # define_auth_challenge          = aws_lambda_function.lambda_function_trigger.arn
    # verify_auth_challenge_response = aws_lambda_function.lambda_function_trigger.arn

    # user_migration = aws_lambda_function.lambda_function_trigger.arn

    # custom_email_sender {
    #   lambda_arn     = aws_lambda_function.lambda_function_trigger.arn
    #   lambda_version = "V1_0"
    # }

    # custom_sms_sender {
    #   lambda_arn     = aws_lambda_function.lambda_function_trigger.arn
    #   lambda_version = "V1_0"
    # }

    # only required for custom senders
    # kms_key_id = aws_kms_key.kms_key.arn
  }
}

resource "aws_iam_role" "cognito_sms" {
  name = "${local.id}-sms-role"

  assume_role_policy = <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Principal": {
        "Service": "cognito-idp.amazonaws.com"
      },
      "Action": "sts:AssumeRole",
      "Condition": {
        "StringEquals": {
          "sts:ExternalId": "${random_string.external_id.result}"
        }
      }
    }
  ]
}
EOF
}

resource "aws_iam_role_policy" "cognito_sms" {
  role = aws_iam_role.cognito_sms.name

  policy = <<POLICY
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": [
        "sns:publish"
      ],
      "Resource": [
        "*"
      ]
    }
  ]
}
POLICY
}

resource "aws_cognito_user_pool_client" "cognito_user_pool_client" {
  name = "${local.id}-user-pool-client"

  user_pool_id = aws_cognito_user_pool.cognito_user_pool.id

  explicit_auth_flows = [
    "ALLOW_USER_SRP_AUTH",
    "ALLOW_REFRESH_TOKEN_AUTH",
    # not usually recommended, but makes testing easier for this guide
    "ALLOW_USER_PASSWORD_AUTH"  
  ]

  refresh_token_validity = 30
  access_token_validity  = 60
  id_token_validity      = 60

  token_validity_units {
    access_token  = "minutes"
    id_token      = "minutes"
    refresh_token = "days"
  }

  enable_token_revocation       = true
  prevent_user_existence_errors = "ENABLED"
}

resource "aws_kms_key" "kms_key" {
  description = "KMS key for Cognito Lambda triggers"
  policy      = <<EOF
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Effect": "Allow",
            "Principal": {
                "AWS": "arn:aws:iam::${data.aws_caller_identity.current.account_id}:root"
            },
            "Action": "kms:*",
            "Resource": "*"
        },
        {
            "Effect": "Allow",
            "Principal": {
                "AWS": "${data.aws_caller_identity.current.arn}"
            },
            "Action": [
                "kms:Create*",
                "kms:Describe*",
                "kms:Enable*",
                "kms:List*",
                "kms:Put*",
                "kms:Update*",
                "kms:Revoke*",
                "kms:Disable*",
                "kms:Get*",
                "kms:Delete*",
                "kms:TagResource",
                "kms:UntagResource",
                "kms:ScheduleKeyDeletion",
                "kms:CancelKeyDeletion"
            ],
            "Resource": "*"
        }
    ]
}
EOF
}
data "aws_iam_policy_document" "AWSLambdaTrustPolicy" {
  version = "2012-10-17"
  statement {
    actions = ["sts:AssumeRole"]
    effect  = "Allow"
    principals {
      type        = "Service"
      identifiers = ["lambda.amazonaws.com"]
    }
  }
}

resource "aws_iam_role" "iam_role" {
  assume_role_policy = data.aws_iam_policy_document.AWSLambdaTrustPolicy.json
  name               = "${local.id}-iam-role-lambda-trigger"
}

resource "aws_iam_role_policy_attachment" "iam_role_policy_attachment_lambda_basic_execution" {
  role       = aws_iam_role.iam_role.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AWSLambdaBasicExecutionRole"
}

data "aws_iam_policy_document" "iam_policy_document_lambda_kms" {
  version = "2012-10-17"
  statement {
    actions = ["kms:Decrypt"]
    effect  = "Allow"
    resources = [
      aws_kms_key.kms_key.arn
    ]
  }
}

resource "aws_iam_role_policy" "iam_role_policy_lambda_kms" {
  name   = "${local.id}-iam-role-policy-lambda-kms"
  role   = aws_iam_role.iam_role.name
  policy = data.aws_iam_policy_document.iam_policy_document_lambda_kms.json
}

resource "aws_lambda_function" "lambda_function_trigger" {
  environment {
    variables = {
      KEY_ID = aws_kms_key.kms_key.arn
      AWS_LAMBDA_HANDLER_LOG_LEVEL = "Debug"
      SOURCE_EMAIL_ADDRESS_PARAMETER = aws_ssm_parameter.source_notification_email.name
    }
  }

  description      = "Cognito Trigger Handler Function"
  timeout          = 10
  filename         = local.lambda_archive_path
  function_name    = "${local.id}-lambda-function-trigger"
  role             = aws_iam_role.iam_role.arn
  handler          = "CognitoLambdaTriggers::CognitoLambdaTriggers.Function::FunctionHandler"
  runtime          = "dotnet6"
  source_code_hash = filebase64sha256(local.lambda_archive_path)
}

resource "aws_lambda_permission" "lambda_permission_cognito" {
  action        = "lambda:InvokeFunction"
  function_name = aws_lambda_function.lambda_function_trigger.function_name
  principal     = "cognito-idp.amazonaws.com"
  source_arn    = aws_cognito_user_pool.cognito_user_pool.arn
}

resource "aws_ssm_parameter" "cognito_user_pool_name" {
  name        = "/${var.stage}/${var.project_name}/user-pool/name"
  description = "Cognito user pool full name"
  type        = "String"
  value       = aws_cognito_user_pool.cognito_user_pool.name
}

resource "aws_ssm_parameter" "cognito_user_pool_client_id" {
  name        = "/${var.stage}/${var.project_name}/user-pool/client/id"
  description = "Cognito user pool client ID"
  type        = "String"
  value       = aws_cognito_user_pool_client.cognito_user_pool_client.id
}

resource "aws_ssm_parameter" "cognito_user_pool_kms_key_arn" {
  name        = "/${var.stage}/${var.project_name}/user-pool/kms-key-arn"
  description = "KMS Key ARN for Lambda triggers"
  type        = "String"
  value       = aws_kms_key.kms_key.arn
}
