Feature: User
	Simple calculator for adding two numbers

Scenario: Get weather forecasts 
	Given I am a client
	When I make a Post request with 'user.json' to 'UserCreate'
	Then the response status code is '201'	