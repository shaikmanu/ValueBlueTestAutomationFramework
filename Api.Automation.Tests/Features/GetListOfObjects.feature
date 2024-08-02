Feature: GetListOfObjects

Get list of objects

@tag1
Scenario: Get list of objects
	Given Get list of all objects is performed
	Then the response status of all objects should be 200
	And the response should contain list of objects
	  | id | name                                     | data                                                      |
      | 1  | Google Pixel 6 Pro                       | {"color": "Cloudy White", "capacity": "128 GB"}           |
      | 2  | Apple iPhone 12 Mini, 256GB, Blue        | null                                                      |
      | 3  | Apple iPhone 12 Pro Max                  | {"color": "Cloudy White", "capacity GB": 512}             |
      | 4  | Apple iPhone 11, 64GB                    | {"price": 389.99, "color": "Purple"}                      |
