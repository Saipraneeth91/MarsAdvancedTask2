Feature: Password Management of User

Scenario: Change Password of User
	Given user is able to login to MARS application successfully .
	When the user tries to change password from <JsonFileName> with <id>
	Then the password should be updated successfully
	Examples:
    | JsonFileName   | id |
    | "password.json"   | 1 |
