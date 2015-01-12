using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;

using TechTalk.SpecFlow;
using Coypu;


namespace MVCApp.Specs
{
    [Binding]
    public class CoypuInitializer
    {
        private readonly IObjectContainer _objectContainer;
        private BrowserSession _browser;

        public CoypuInitializer(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }


        [BeforeScenario]
        public void Before()
        {
            _browser = new BrowserSession(new SessionConfiguration { AppHost = "localhost", Port = 3808, Driver = typeof(SeleniumWebDriver),Browser= Browser.PhantomJS });
            _objectContainer.RegisterInstanceAs(_browser);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _browser.Dispose();
        }
      }

}
