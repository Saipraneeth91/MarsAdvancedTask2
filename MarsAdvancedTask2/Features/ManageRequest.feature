Feature: Manage Sent and Received Requests of User

  As a user, I want to view both Received and Sent Requests
  so that I can effectively manage all my requests.

  @ReceivedRequests
  Scenario: Accept Received Requests
    Given the user has successfully logged into the MARS application 
    And the user navigates to the Received requests tab
    When the user attempts to accept a received request from "<RequestFile>"
    Then the request should be accepted successfully

    Examples:
       | RequestFile              |
      | Acceptrequest.json     |

  @ReceivedRequests
  Scenario: Decline Received Requests
    Given the user has successfully logged into the MARS application 
    And the user navigates to the Received requests tab
    When the user attempts to decline a received request from "<RequestFile>"
    Then the request should be declined successfully

    Examples:
    | RequestFile        |
    | DeclineRequest.json    |

  @SentRequests
  Scenario: Withdraw Sent Requests
    Given the user has successfully logged into the MARS application 
    And the user navigates to the Sent requests tab
    When the user attempts to withdraw a sent request from "<RequestFile>"
    Then the user should be able to withdraw the sent request successfully

    Examples:
     | RequestFile              |
     | Withdrawrequest.json     |

  @CompletedRequests
  Scenario: Complete Received Requests
    Given the user has successfully logged into the MARS application 
    And the user navigates to the Received requests tab
    When the user attempts to complete an accepted request from "<RequestFile>"
    Then the user should be able to complete the received request successfully

    Examples:
    | RequestFile             |
    | CompleteRequest.json    |
