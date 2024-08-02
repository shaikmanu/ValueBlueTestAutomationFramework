Feature: GetSingleObject

Perfroms a get operation by Id

@tag1
Scenario: Get an object by Id
	Given Get operation for existing Id is performed
		| Id |
		| 7  |
	Then the response status should be 200
	And the response should contain the object details
