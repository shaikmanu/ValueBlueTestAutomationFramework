Feature: Example
Simple page which shows some details related to domains

Scenario: FinishMe
    Given I am on the example home page and click on the link 'More information...'
    Then a link with text 'RFC 2606' must be present
    And a link with text 'RFC 6761' must be present
    And the 'Domain Names' box must contain 'Root Zone Management' at index '2'
