Feature: DescriptionFeature

As a user, 
I want to login and update my description on profile page.

Scenario Outline: 01 - Adding my description on the profile page 
    Given  user is logged into the MARS application successfully 
    When  User add my description from "<JsonFileName>" with ID "<id>"
    Then the description  should be updated successfully

Examples:
    | JsonFileName      | id |
    | Description.json  | 1  |