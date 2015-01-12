Feature: PersistCustomer
	In order keep information about my customers
	As a user
	I want to keep customer information between browsing sessions

@mytag
Scenario: Save customer
	Given I have entered a customers details
	When I press save
	And I reload the page
	Then the customer should be shown
