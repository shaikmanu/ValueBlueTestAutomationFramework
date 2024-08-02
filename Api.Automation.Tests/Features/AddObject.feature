Feature: AddObject
   Create a new object

@tag1
Scenario Outline: Add object with valid inputs
	Given Object is created using AddObject api with '<request>'
	When Send request to add object
	Then the response of AddObject status should be 200
	And the object should be created with valid data
	 
	Examples: 
	| request               |
	| AddObjectRequest.json |
