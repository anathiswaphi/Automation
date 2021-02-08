using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace DIgiOutsourceAutomation.UI.BetwayRegistration
{
    class BetwayRegistration
    {         
        public void Click_SignUp()
        {
            ScreenDriver.driver.FindElement(By.Id("SignUp")).Click();
        }

        public void Click_ClosePopup()
        {
            ScreenDriver.driver.FindElement(By.XPath("(//button[@id='SU-Close'])[2]")).Click();
        }   
        
        public void Set_MobileNumber(String Mobile)
        {
            ScreenDriver.driver.FindElement(By.Id("MobileNumber_tmpl")).SendKeys(Mobile);
        }

        public void Set_Password(String password)
        {
            ScreenDriver.driver.FindElement(By.Id("Password_tmpl")).SendKeys(password);
        }

        public void Set_FirstName(String FName)
        {
            ScreenDriver.driver.FindElement(By.Id("FirstName_tmpl")).SendKeys(FName);
        }

        public void Set_LastName(String LName)
        {
            ScreenDriver.driver.FindElement(By.Id("LastName_tmpl")).SendKeys(LName);
        }

        public void Set_Email(String email)
        {
            ScreenDriver.driver.FindElement(By.Id("Email_tmpl")).SendKeys(email);
        }

        public void Click_Next()
        {
            ScreenDriver.driver.FindElement(By.Id("nxtBtn")).Click();
        }

        public void Set_IDType(string type)
        {
            var elem =  ScreenDriver.driver.FindElement(By.Id("IDType_tmpl"));
            var selectElement = new SelectElement(elem);
            selectElement.SelectByValue(type);
        }

        public void Set_IDNumber(String id)
        {
            ScreenDriver.driver.FindElement(By.Id("IDNumber_tmpl")).SendKeys(id);
        }

        public void Set_DOB_Day(string day)
        {
            var selectElement = new SelectElement(ScreenDriver.driver.FindElement(By.Id("Template_TemplateFieldModels_13__Value_Day")));
            selectElement.SelectByValue(day);
        }

        public void Set_DOB_Month(string month)
        {            
            var selectElement = new SelectElement(ScreenDriver.driver.FindElement(By.Id("Template_TemplateFieldModels_13__Value_Month")));
            selectElement.SelectByValue(month);
        }

        public void Set_DOB_Year(string year)
        {            
            var selectElement = new SelectElement(ScreenDriver.driver.FindElement(By.Id("Template_TemplateFieldModels_13__Value_Year")));
            selectElement.SelectByValue(year);
        }

        public void Set_SourceOfFund(string fund)
        {            
            var selectElement = new SelectElement(ScreenDriver.driver.FindElement(By.Id("SourceOfFunds_tmpl")));
            selectElement.SelectByText(fund);
        }

        public void Click_ConfirmAge()
        {
            ScreenDriver.driver.FindElement(By.Id("ConfirmAge_tmpl")).Click();
        }    

          
        public bool Validate_HomePage()
        {
            WebDriverWait Screen = new WebDriverWait(ScreenDriver.driver, TimeSpan.FromSeconds(60));
            Screen.PollingInterval = TimeSpan.FromSeconds(5);
            var elem = ScreenDriver.driver.FindElement(By.XPath("//a[@class='navbar-link']"));

            return (Screen.Until(drv => elem.Displayed));            
        }
              
        public bool Validate_RegistrationForm()
        {
            WebDriverWait Screen = new WebDriverWait(ScreenDriver.driver, TimeSpan.FromSeconds(60));
            Screen.PollingInterval = TimeSpan.FromSeconds(5);
            var elem = ScreenDriver.driver.FindElement(By.Id("signupContainerModal"));

            return (Screen.Until(drv => elem.Displayed));
        }
     
        public bool Validate_SecondRegistrationFields()
        {
            WebDriverWait Screen = new WebDriverWait(ScreenDriver.driver, TimeSpan.FromSeconds(60));
            Screen.PollingInterval = TimeSpan.FromSeconds(5);

            var elem = ScreenDriver.driver.FindElement(By.Id("TabHeader_2"));

            return (Screen.Until(drv => elem.GetAttribute("class")).Equals("active"));
        }
    }
}
