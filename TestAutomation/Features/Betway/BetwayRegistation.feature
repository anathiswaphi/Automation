Feature:Betway Registration
   As new member I am required to register before I login.

    Scenario Outline: Dummy Registration
    Given I navigate to Betway website <Url>
    Then I validate the Betway home page
    When I click the Sign Up button
    Then I validate betway registration form is present 
    When I complete the first required registration fields <Mobile>,<Password>,<FName>,<LName>,<Email>
    And I click the Next button on registration form
    Then I validate the second registration fields
    Then I complete the second required registration fields <ID_Type>,<ID_No>,<DOB_Day>,<DOB_Month>,<DOB_Year>,<Salary>
    Then I close the browser

    Scenarios: 
        | Mobile     | Password | FName      | LName    | Email                         | ID_Type             | ID_No         | DOB_Day | DOB_Month  | DOB_Year | Salary  | Url                      |
        | 0841235567 | Testing  | Automation | Engineer | automation.engineer@gmail.com | South African ID    | 9402116285080 | 11      | 2   | 1994     | Savings | https://www.betway.co.za |
           