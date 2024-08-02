Feature: DeleteObject

Delete operation for existing Id

@tag1
Scenario: Delete an exsting object
	Given Delete operation for existing Id is performed
	| Id |
	| 6  |
	Then the delete response status should be 200
	And the object should no longer exist
