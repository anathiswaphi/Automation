Feature:Google News
   As a client I want to be able to retrieve all google news headlines

    Scenario:Get All Google News Headlines 
    Given I navigate to google headlines page http://news.google.com/
    Then I validate google headlines page
    When I extract all the headlines
    Then I print the extracted headlines
    Then I close the browser