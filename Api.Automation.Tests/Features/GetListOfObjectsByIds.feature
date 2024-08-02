Feature: GetListOfObjectsByIds

Get list of objects by Ids

@tag1
Scenario: Get list of objects by Ids
	Given Get list of objects by multiple Ids is performed
	 | Id | 
	 | 3  | 
	 | 5  | 
	 | 10 |
	When Request is send to get the objects by these Ids
	Then the response status of objects should be 200
	And the response should contain the valid objects details
     | id | name                    | data												|
     | 3  | Apple iPhone 12 Pro Max |  {"color": "Cloudy White", "capacity GB": 512}    |
     | 5  | Samsung Galaxy Z Fold2  |  {"price": 689.99, "color": "Brown"}				|
     | 10 | Apple iPad Mini 5th Gen |  {"Capacity": "64 GB", "Screen size": 7.9}		|