Feature: PartialUpdateObject

PartialUpdate object for existing Object Id

@tag1
Scenario: Partial update object for existing Object Id
Given There is existing object Id 7 with valid partial update payload 'PartialUpdateObjectRequest.json' 
    When Send request to partial update object
	Then the response status of patch method should be 200
	And The response should contain the updated name object 
