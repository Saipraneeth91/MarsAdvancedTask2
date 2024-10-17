Feature: EducationdetailsFeature

As a user, 
I want to manage education details in profile

Scenario Outline: 01 - Add education with valid details
    Given User logged in to Mars Application and Navigates to education tab
    When  User adds a new education from json file <JsonFileName> with ID <id> 
    Then  education details should be added succesfully to my profile
    Examples:
      | JsonFileName            | id |
      | "ValidEducationDetails.json"   | 1 |

Scenario Outline: 02 - Add a new education with special character
    Given User logged in to Mars Application and Navigates to education tab
    When User  adds a special character education record from json file <JsonFileName> with ID <id> 
    Then education details with special characters should not be added succesfully to my profile

    Examples:
    | JsonFileName            | id |
    | "AddEducationWithSpecialCharacters.json"   | 1 |

Scenario Outline: 03 - Add a new education with Blank values
    Given User logged in to Mars Application and Navigates to education tab
    When User  adds a Blank value education record from json file <JsonFileName> with ID <id>
    Then education details with blank values should not be added succesfully to my profile

    Examples:
    | JsonFileName            | id |
    | "BlankEducationDetails.json"   | 1 |

Scenario Outline: 04 - Add a new education with Destructive  Data
    Given User logged in to Mars Application and Navigates to education tab
    When User  adds a destructive data education record from json file <JsonFileName> with ID <id>
    Then education details with destructive data should not be added succesfully to my profile

   Examples:
    | JsonFileName            | id |
    | "DestructiveEducationDetails.json"   | 1 |

    Scenario Outline: 05 - Delete an existing education record
    Given User logged in to Mars Application and Navigates to education tab
    When User deletes an existing education record in <JsonFileName> with ID <id> 
    Then education record should be succesfully deleted

  Examples:
    | JsonFileName                  | id |
    | "DeleteEducationDetails.json"   | 1 |
   

