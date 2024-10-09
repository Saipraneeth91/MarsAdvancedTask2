using MarsAdvancedTask2.Helpers;
using MarsAdvancedTask2.Models;
using MarsAdvancedTask2.Pages.Components;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace MarsAdvancedTask2.StepDefinitions
{
    [Binding]
    public class PasswordManagementStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly LoginPage loginMars;
        private readonly PasswordComponent password;
        private readonly PasswordAssertion passwordAssertion;
        public PasswordManagementStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
            loginMars = new LoginPage(driver);
            password = new PasswordComponent(driver);
            passwordAssertion = new PasswordAssertion(driver);
        }

        [Given(@"user is able to login to MARS application successfully \.")]
        public void GivenUserIsAbleToLoginToMARSApplicationSuccessfully_()
        {
            loginMars.LoginActions("saipraneethg.91@gmail.com", "Praneeth@1");
        }

        [When(@"the user tries to change password from ""([^""]*)"" with (.*)")]
        public void WhenTheUserTriesToChangePasswordFromWith(string jsonfilename, int id)
        {
            var passwordDataList = JSONHelper.LoadData<List<PasswordModel>>(jsonfilename);
            var passwordData = passwordDataList.FirstOrDefault(p => p.Id == id);

            if (passwordData != null)
            {
                password.Change_Password(passwordData);
            }
            else
            {
                throw new Exception($"No password data found with ID {id}");
            }
        }

        [Then(@"the password should be updated successfully")]
        public void ThenThePasswordShouldBeUpdatedSuccessfully()
        {
            var expected = JSONHelper.LoadData<List<PasswordModel>>("password.json").First();
             passwordAssertion.assertPassword(expected.PopUpMessage);

        }
    }
}
