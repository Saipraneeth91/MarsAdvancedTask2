Feature: Manage Listings of the User

  As a user, I want to add, view, update, and delete listings
  so that people seeking listings can see the details User hold.

  @AddListing
  Scenario Outline: 01 Add a listing in Manage Listings
    Given the user has successfully logged into the MARS application 
    When the user attempts to add a listing from "<ListingFile>"
    Then the listing should be added successfully
    Examples:
     | ListingFile            |
     | AddListing.json        |

  @UpdateListing
  Scenario Outline: 02 Update a listing in Manage Listings
    Given the user has successfully logged into the MARS application 
    When the user attempts to update a listing from "<ListingFile>"
    Then the listing should be updated successfully

    Examples:
      | ListingFile            |
      | UpdateListing.json     |

  @ViewListing
  Scenario Outline: 03 View a listing in Manage Listings
    Given the user has successfully logged into the MARS application 
    When the user attempts to view a listing from "<ListingFile>"
    Then the listing should be viewed successfully

    Examples:
      | ListingFile            |
      | ViewListing.json       |

  @DeleteListing
  Scenario Outline: 04 Delete a listing in Manage Listings
    Given the user has successfully logged into the MARS application 
    When the user attempts to delete a listing from "<ListingFile>"
    Then the listing should be deleted successfully

    Examples:
      | ListingFile            |
      | DeleteListing.json     |

  @EnableToggle
  Scenario Outline: 05 Enable toggle in Manage Listings
    Given the user has successfully logged into the MARS application 
    When the user attempts to enable a toggle in a listing from "<ToggleFile>"
    Then the toggle should be enabled successfully

    Examples:
      | ToggleFile             |
      | ToggleEnable.json      |

  @DisableToggle
  Scenario Outline: 06 Disable toggle in Manage Listings
    Given the user has successfully logged into the MARS application 
    When the user attempts to disable a toggle in a listing from "<ToggleFile>"
    Then the toggle should be disabled successfully

    Examples:
      | ToggleFile             |
     | ToggleDisable.json     |

     @SendRequest
Scenario Outline: 07 Send a request to a listing published by another user
  Given the user has successfully logged into the MARS application 
  When the user attempts to send a request to a listing from "<RequestFile>"
  Then the request should be sent successfully

  Examples:
    | RequestFile            |
   | SendRequest.json       |
