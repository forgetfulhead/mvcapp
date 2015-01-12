using System;
using Coypu;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace MVCApp.Specs.Steps
{
    [Binding]
    public class AddCustomerSteps
    {

        private readonly BrowserSession _browser;

          public AddCustomerSteps(BrowserSession browser)
        {
            _browser = browser;
        }

     [Given(@"I have the main url open in a browser")]
        public void GivenIHaveTheMainUrlOpenInABrowser()
        {
           
            _browser.Visit("/");
        }
        
   
        [When(@"I press Add Customer")]
        public void WhenIPressAddCustomer()
        {
            _browser.ClickButton("addCustomer");
        }


        [Then(@"A new empty user should be added to the system")]
        public void ThenANewEmptyUserShouldBeAddedToTheSystem()
        {
            Assert.That(_browser.HasContent("Customers(1)"));
        }
    }
}
