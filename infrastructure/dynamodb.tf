resource "aws_dynamodb_table" "users_table" {
  name             = "${local.id}-users"
  hash_key         = "UserId"
  billing_mode     = "PAY_PER_REQUEST"

  attribute {
    name = "UserId"
    type = "S"
  }
  
  attribute {
    name = "GameTitle"
    type = "S"
  }
}
