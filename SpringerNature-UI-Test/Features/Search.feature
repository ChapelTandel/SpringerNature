Feature: Search
	As a user I can perform various kind of search

Scenario: Default Search
	Given I am on Springer Link homepage 
	When I click the search button 
	Then I see list of search results 

Scenario: Search specific term 
	Given I am on Springer Link homepage
	When I search for the test "Swiss Journal of Palaeontology" 
	Then I see "Swiss Journal of Palaeontology" in the search result 

Scenario: Search item not found 
	Given I am on Springer Link homepage
	When I search for the test "xhgdsfr" 
	Then I see "0" seach result retrun 
