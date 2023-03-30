resource "aws_ses_email_identity" "this" {
  email = var.email_identity
}

data "aws_iam_policy_document" "iam_policy_document_lambda_ses" {
  version = "2012-10-17"
  statement {
    actions = [
      "ses:SendEmail"
    ]
    effect  = "Allow"
    resources = [
      aws_ses_email_identity.this.arn
    ]
    condition {
      test = "StringLike"
      variable = "ses:FromAddress"
      values = [var.email_identity]
    }
  }
  statement {
    actions = [
        "ssm:GetParameter"
    ]
    effect  = "Allow"
    resources = [
      aws_ssm_parameter.source_notification_email.arn
    ]
  }
}

resource "aws_iam_role_policy" "iam_role_policy_lambda_ses" {
  name   = "${local.id}-iam-role-policy-lambda-ses"
  role   = aws_iam_role.iam_role.name
  policy = data.aws_iam_policy_document.iam_policy_document_lambda_ses.json
}

resource "aws_ssm_parameter" "source_notification_email" {
  name  = "/${var.stage}/${var.project_name}/source-email"
  type  = "String"
  value = var.email_identity
}