Feature: UpdateObject

Update object for existing Object Id

@tag1
Scenario: Update object for existing Object Id
Given There is existing object Id 7 with valid update 'UpdateObjectRequest.json' 
    When Send request to update object
	Then the response status of put method should be 200
	And The response should contain the updated object details
