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
    public class DescriptionAssertion
    {
        private IWebDriver driver;
        private ElementUtil eleutil;
        private DescriptionComponent descriptionComponent;

        public DescriptionAssertion(IWebDriver driver)
        {
            this.driver = driver;
            eleutil = new ElementUtil(driver);
            descriptionComponent = new DescriptionComponent(driver);
        }
        public void assertAddDescriptionSuccessMessage(String expected)
        {
            string actual = descriptionComponent.renderMessageBoxTestComponent();
            Assert.That(actual,Is.EqualTo(expected));
        }
    }


}

