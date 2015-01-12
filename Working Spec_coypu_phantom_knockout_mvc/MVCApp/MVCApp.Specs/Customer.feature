Feature: AddCustomer
	In order to mass mail customers
	As a user
	I want to add a customer so I can send them email

@mytag
Scenario: Browse Add page
	Given I have the main url open in a browser
	When I press Add Customer
	Then A new empty user should be added to the system
