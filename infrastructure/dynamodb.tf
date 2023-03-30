resource "aws_dynamodb_table" "users_table" {
  name             = "Users"
  hash_key         = "UserId"
  billing_mode     = "PAY_PER_REQUEST"

  attribute {
    name = "UserId"
    type = "S"
  }
}

data "aws_iam_policy_document" "iam_policy_document_lambda_dynamodb" {
  version = "2012-10-17"
  statement {
    actions = [
      "dynamodb:DescribeTable",
      "dynamodb:PutItem",
      "dynamodb:UpdateItem"
    ]
    effect  = "Allow"
    resources = [
      "${aws_dynamodb_table.users_table.arn}"
    ]
  }
}

resource "aws_iam_role_policy" "iam_role_policy_lambda_dynamodb" {
  name   = "${local.id}-iam-role-policy-lambda-dynamodb"
  role   = aws_iam_role.iam_role.name
  policy = data.aws_iam_policy_document.iam_policy_document_lambda_dynamodb.json
}