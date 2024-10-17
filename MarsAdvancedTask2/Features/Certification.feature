Feature: certificationdetailsFeature

As a user, 
I want to manage my certification details in my profile

Scenario Outline: 01 - Add certification with valid details
    Given User logged in to Mars Application and Navigates to certification tab
    When  User adds a new certification from json file <JsonFileName> with ID <id> 
    Then  certification details should be added succesfully to my profile
    Examples:
      | JsonFileName            | id |
      | "ValidCertificationDetails.json"   | 1 |

Scenario Outline: 02 - Add a new certification with special character
    Given User logged in to Mars Application and Navigates to certification tab
    When User  adds a special character certification record from json file <JsonFileName> with ID <id> 
    Then certification details with special characters should not be added succesfully to my profile

    Examples:
    | JsonFileName            | id |
    | "AddCertificationWithSpecialCharacters.json"   | 1 |

Scenario Outline: 03 - Add a new certification with Blank values
    Given User logged in to Mars Application and Navigates to certification tab
    When User  adds a Blank value certification record from json file <JsonFileName> with ID <id>
    Then certification with blank values should not be added succesfully to my profile

    Examples:
    | JsonFileName            | id |
    | "BlankCertificationDetails.json"   | 1 |

Scenario Outline: 04 - Add a new certification with Destructive  Data
    Given User logged in to Mars Application and Navigates to certification tab
    When User  adds a destructive data certification record from json file <JsonFileName> with ID <id>
    Then certification details with destructive data should not be added succesfully to my profile

   Examples:
    | JsonFileName            | id |
    | "DestructiveCertificationDetails.json"   | 1 |

    Scenario Outline: 05 - Delete an existing certification record
    Given User logged in to Mars Application and Navigates to certification tab
    When User deletes an existing certification record in <JsonFileName> with ID <id> 
    Then certification record should be succesfully deleted

  Examples:
    | JsonFileName                  | id |
    | "DeleteCertificationDetails.json"   | 1 |
   

