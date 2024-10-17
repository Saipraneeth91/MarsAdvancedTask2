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
    public class ManageListing
    {
        private IWebDriver driver;
        private ElementUtil eleutil;
        private ManagelistingComponent manageListingComponent;

        public ManageListing(IWebDriver driver)
        {
            this.driver = driver;
            eleutil = new ElementUtil(driver);
            manageListingComponent = new ManagelistingComponent(driver);
        }
            public void AssertDeleteListing(ListingData expectedmessage)
            {
             string actualMessage = manageListingComponent.RenderPopUpMessage();
            string expectedMessage = expectedmessage.Title + " has been deleted";
            Assert.That(actualMessage, Is.EqualTo(expectedMessage));
         
        }

            public void AssertAddListing(string expectedCategory, string expectedTitle, string expectedDescription)
            {
                string category = manageListingComponent.ListCategory();
                string title = manageListingComponent.ListTitle();
                string description = manageListingComponent.ListDescription();
                Assert.That(category == expectedCategory, "Category not added Successfully");
                Assert.That(title == expectedTitle, "Title not added Successfully");
            Assert.That(description == expectedDescription, "Description not added successfully");

            }

            public void AssertUpdateListing(string expectedCategory, string expectedTitle, string expectedDescription)
            {
                string category = manageListingComponent.ListCategory();
                string title = manageListingComponent.ListTitle();
                string description = manageListingComponent.ListDescription();
                 Console.WriteLine(expectedTitle);
                 Console.WriteLine(title);
                Assert.That(category == expectedCategory, "Category not updated Successfully");
                Assert.That(title == expectedTitle, "Title not updated Successfully");
                Assert.That(description == expectedDescription, "Description not updated successfully");
            }


            public void AssertViewListing(string skillTitle)
            {
            string title = manageListingComponent.GetSkillTitle();
             Assert.That(title == skillTitle, "Skill not viewed Successfully");

            }

            public void AssertEnableToggle()
            {
              string actualmessage = manageListingComponent.RenderPopUpMessage();
              string expectedmessage = "Service has been activated";
            Assert.That(actualmessage, Is.EqualTo(expectedmessage));


        }

            public void AssertDisableToggle()
            {

            string actualmessage = manageListingComponent.RenderPopUpMessage();
            string expectedmessage = "Service has been deactivated";
            Assert.That(actualmessage, Is.EqualTo(expectedmessage));
            }
       


            public void AssertSendRequest()
            {
               string actualmessage = manageListingComponent.RenderPopUpMessage();
               string expectedmessage = "Request sent";
            Assert.That(actualmessage, Is.EqualTo(expectedmessage));
        }
    }
        }

    
