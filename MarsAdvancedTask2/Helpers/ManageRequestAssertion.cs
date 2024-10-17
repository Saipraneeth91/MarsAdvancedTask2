using MarsAdvancedTask2.Models;
using MarsAdvancedTask2.Pages.Components;
using MarsAdvancedTask2.Pages.Components.Profile;
using MarsAdvancedTask2.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTask2.Helpers
{
    public class ManageRequestAssertion
    {
        private IWebDriver driver;
        private ElementUtil eleutil;
        private ManageRequest ManageRequestComponent;

        public ManageRequestAssertion(IWebDriver driver)
        {
            this.driver = driver;
            eleutil = new ElementUtil(driver);
            ManageRequestComponent = new ManageRequest(driver);
        }
  
        public void Acceptreceivedrequestassertion()
        {
            string actualtext = ManageRequestComponent.RenderPopUpMessage();
            string expectedtext = "Service has been updated";
            Assert.That(actualtext, Is.EqualTo(expectedtext));

        }
        public void Declinereceivedrequestassertion()
        {
            string actualtext = ManageRequestComponent.RenderPopUpMessage();
            string expectedtext = "Service has been updated";
            Assert.That(actualtext, Is.EqualTo(expectedtext));

        }
        public void Completerequestassertion()
        {
            string actualtext = ManageRequestComponent.RenderPopUpMessage();
            string expectedtext = "Request has been updated";
            Assert.That(actualtext, Is.EqualTo(expectedtext));

        }
        public void Withdrawsentrequestassertion()
        {
            string actualtext = ManageRequestComponent.RenderPopUpMessage();
            string expectedtext = "Request has been withdrawn";
            Assert.That(actualtext, Is.EqualTo(expectedtext));

        }

    }

}
