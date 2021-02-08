using System;
using DIgiOutsourceAutomation.Helpers;
using DIgiOutsourceAutomation.UI;
using DIgiOutsourceAutomation.UI.BetwayRegistration;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace DIgiOutsourceAutomation.Steps
{
    [Binding]
    public sealed class StepDefinition
    {
        static BetwayRegistration Registration = new BetwayRegistration();

      
        [Given(@"I navigate to Betway website (.*)")]
        public void GivenINavigateToBetwayWebsite(string p0)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                ScreenDriver.Goto(p0);
                success = true;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Parameters.SaveParameter("Expected", success ? message : message);
            Assert.IsTrue(success, message);
        }

        [Then(@"I validate the Betway home page")]
        public void ThenIValidateTheBetwayHomePage()
        {    
            string message = string.Empty;
            bool success = false;

            try
            {
                Registration.Click_ClosePopup();
                success = Registration.Validate_HomePage();
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Parameters.SaveParameter("Expected", success ? message : message);
            Assert.IsTrue(success, message);
        }

        [When(@"I click the Sign Up button")]
        public void WhenIClickTheSignUpButton()
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                Registration.Click_SignUp();
                success = true;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Parameters.SaveParameter("Expected", success ? message : message);
            Assert.IsTrue(success, message);
        }


        [Then(@"I validate betway registration form is present")]
        public void ThenIValidateBetwayRegistrationFormIsPresent()
        {            
            string message = string.Empty;
            bool success = false;

            try
            {                
                success = Registration.Validate_RegistrationForm();
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Parameters.SaveParameter("Expected", success ? message : message);
            Assert.IsTrue(success, message);
        }

        [When(@"I complete the first required registration fields (.*),(.*),(.*),(.*),(.*)")]
        public void WhenICompleteTheFirstRequiredRegistrationFields(string p0, string p1, string p2, string p3, string p4)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                Registration.Set_MobileNumber(p0);
                Registration.Set_Password(p1);
                Registration.Set_FirstName(p2);
                Registration.Set_LastName(p3);
                Registration.Set_Email(p4);
                success = true;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Parameters.SaveParameter("Expected", success ? message : message);
            Assert.IsTrue(success, message);
        }

        [When(@"I click the Next button on registration form")]
        public void WhenIClickTheNextButtonOnRegistrationForm()
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                Registration.Click_Next();
                success = true;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Parameters.SaveParameter("Expected", success ? message : message);
            Assert.IsTrue(success, message);
        }

        [Then(@"I validate the second registration fields")]
        public void ThenIValidateTheSecondRegistrationFields()
        {       
            string message = string.Empty;
            bool success = false;

            try
            {                
                success = Registration.Validate_SecondRegistrationFields();
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Parameters.SaveParameter("Expected", success ? message : message);
            Assert.IsTrue(success, message);
        }

        [Then(@"I complete the second required registration fields (.*),(.*),(.*),(.*),(.*),(.*)")]
        public void ThenICompleteTheSecondRequiredRegistrationFields(string p0, string p1, string p2, string p3, string p4, string p5)
        {
            string message = string.Empty;
            bool success = false;

            try
            {
                Registration.Set_IDType(p0);
                Registration.Set_IDNumber(p1);
                Registration.Set_DOB_Day(p2);
                Registration.Set_DOB_Month(p3);
                Registration.Set_DOB_Year(p4);
                Registration.Set_SourceOfFund(p5);
                Registration.Click_ConfirmAge();

                success = true;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            Parameters.SaveParameter("Expected", success ? message : message);
            Assert.IsTrue(success, message);
        }
    }
}
