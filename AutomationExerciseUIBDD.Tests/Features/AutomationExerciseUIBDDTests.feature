Feature: SpecflowUITests

Scenario: Test Case 1 - Register User
	Given some user 
	When I open home page
	Then I validate home page is opened

	When I go to Signup and Login header menu
	Then I validate Signup form title is New User Signup! on Signup and Login page

	When I fill Signup form for previously created user on Signup and Login page
	When I submit Signup form on Signup and Login page
	Then I validate Signup form title is ENTER ACCOUNT INFORMATION on Signup page

	When I fill Account Info form for previously created user on Signup page
	When I fill Address Info form for previously created user on Signup page
	When I submit Signup form on Signup page
	Then I validate account creation message is ACCOUNT CREATED! on Signup page

	When I click continue button on Signup page
	Then I validate Logged in header is visible

	When I click on delete account header menu
	Then I validate account deleted message is ACCOUNT DELETED! on Signup page
	When I click continue button on Signup page
