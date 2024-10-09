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
    public class PasswordAssertion
    {
        private IWebDriver driver;
        private ElementUtil eleutil;
        private PasswordComponent passwordComponent;

        public PasswordAssertion(IWebDriver driver)
        {
            this.driver = driver;
            eleutil = new ElementUtil(driver);
            passwordComponent = new PasswordComponent(driver);
        }

        public void assertPassword(String expected)
        {
            string actual = passwordComponent.renderMessageBoxTestComponent();
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
