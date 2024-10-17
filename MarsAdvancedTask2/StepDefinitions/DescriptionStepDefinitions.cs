using MarsAdvancedTask2.Helpers;
using MarsAdvancedTask2.Models;
using MarsAdvancedTask2.Pages.Components;
using MarsAdvancedTask2.Pages.Components.Profile;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace MarsAdvancedTask2.StepDefinitions
{
    [Binding]

        public class DescriptionStepDefinitions
        {
            private readonly IWebDriver driver;
            private readonly LoginPage loginMars;
            private readonly DescriptionComponent description;
            private readonly DescriptionAssertion descriptionAssertion;
            public DescriptionStepDefinitions(IWebDriver driver)
            {
                this.driver = driver;
                loginMars = new LoginPage(driver);
                 description = new DescriptionComponent(driver);
               descriptionAssertion = new DescriptionAssertion(driver);
            }
            [Given(@"user is logged into the MARS application successfully")]
        public void GivenUserIsLoggedIntoTheMARSApplicationSuccessfully()
        {
            loginMars.LoginActions("saipraneethg.91@gmail.com", "Praneeth@1");
        }

        [When(@"User add my description from ""([^""]*)"" with ID ""([^""]*)""")]
        public void WhenUserAddMyDescriptionFromWithID(string jsonfilename, int id)
        {
            var descriptionData = JSONHelper.LoadData<List<DescriptionModel>>(jsonfilename);
            var selectedDescription = descriptionData.FirstOrDefault(e => e.Id == id);
            if (selectedDescription != null)
            {
                description.addAndUpdateDescriptionDetails(selectedDescription.Descriptiontext);
                
        }
        }

        [Then(@"the description  should be updated successfully")]
        public void ThenTheDescriptionShouldBeUpdatedSuccessfully()
        {
            var expected = JSONHelper.LoadData<List<DescriptionModel>>("Description.json").First();
            descriptionAssertion.assertAddDescriptionSuccessMessage(expected.ExpectedMessage);
        }
    }
}
