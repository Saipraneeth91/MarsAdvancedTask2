using MarsAdvancedTask2.Helpers;
using MarsAdvancedTask2.Models;
using MarsAdvancedTask2.Pages.Components;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System.Linq;

using RazorEngine;
using System;
using TechTalk.SpecFlow;

namespace MarsAdvancedTask2.StepDefinitions
{
    [Binding]
    public class EducationStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly LoginPage loginMars;
        private readonly EducationComponent education;
        private readonly EducationAssertion educationAssertion;
        private readonly ScenarioContext _scenarioContext;
        public EducationStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            this.driver = driver;
            loginMars = new LoginPage(driver);
            education = new EducationComponent(driver);
            educationAssertion = new EducationAssertion(driver);            _scenarioContext = scenarioContext;

        }

        [Given(@"User logged in to Mars Application and Navigates to education tab")]
        public void GivenUserLoggedInToMarsApplicationAndNavigatesToEducationTab()
        {
            loginMars.LoginActions("saipraneethg.91@gmail.com", "Praneeth@1");
            education.GoToEducationTab();
        }

        [When(@"User adds a new education from json file ""([^""]*)"" with ID (.*)")]
        public void WhenUserAddsANewEducationFromJsonFileWithID(string jsonfilename, int id)
        {
            var educationData = JSONHelper.LoadData<List<EducationDataModel>>(jsonfilename);
            var selectedEducation = educationData.FirstOrDefault(e => e.Id == id);
            if (selectedEducation != null)
            {
                education.AddEducationdetails(selectedEducation.CollegeName, selectedEducation.Country, selectedEducation.Title, selectedEducation.Degree, selectedEducation.GraduationYear);
                _scenarioContext["education"] = selectedEducation;
            }
        }

        [Then(@"education details should be added succesfully to my profile")]
        public void ThenEducationDetailsShouldBeAddedSuccesfullyToMyProfile()
        {
            var expectedEducation = JSONHelper.LoadData<List<EducationDataModel>>("ValidEducationDetails.json").First();
            educationAssertion.AssertValidAddRecord(expectedEducation);
        }

        [When(@"User  adds a special character education record from json file ""([^""]*)"" with ID (.*)")]
        public void WhenUserAddsASpecialCharacterEducationRecordFromJsonFileWithID(string jsonfilename, int id)
        {
            var educationData = JSONHelper.LoadData<List<EducationDataModel>>(jsonfilename);
            var selectedEducation = educationData.FirstOrDefault(e => e.Id == id);
            if (selectedEducation != null)
            {
                education.AddEducationdetails(selectedEducation.CollegeName, selectedEducation.Country, selectedEducation.Title, selectedEducation.Degree, selectedEducation.GraduationYear);
                _scenarioContext["education"] = selectedEducation;
            }

        }

        [Then(@"education details with special characters should not be added succesfully to my profile")]
        public void ThenEducationDetailsWithSpecialCharactersShouldNotBeAddedSuccesfullyToMyProfile()
        {
            educationAssertion.AddEducationWithSpecialCharacters();
        }
        [When(@"User  adds a Blank value education record from json file ""([^""]*)"" with ID (.*)")]
        public void WhenUserAddsABlankValueEducationRecordFromJsonFileWithID(string jsonfilename, int id)
        {
            var educationData = JSONHelper.LoadData<List<EducationDataModel>>(jsonfilename);
            var selectedEducation = educationData.FirstOrDefault(e => e.Id == id);
            if (selectedEducation != null)
            {
                education.AddEducationdetails(selectedEducation.CollegeName, selectedEducation.Country, selectedEducation.Title, selectedEducation.Degree, selectedEducation.GraduationYear);
              
            }
        }
        [Then(@"education details with blank values should not be added succesfully to my profile")]
        public void ThenEducationDetailsWithBlankValuesShouldNotBeAddedSuccesfullyToMyProfile()
        {
            educationAssertion.AddBlankEducation();
        }


        [When(@"User  adds a destructive data education record from json file ""([^""]*)"" with ID (.*)")]
        public void WhenUserAddsADestructiveDataEducationRecordFromJsonFileWithID(string jsonfilename, int id)
        {
            var educationData = JSONHelper.LoadData<List<EducationDataModel>>(jsonfilename);
            var selectedEducation = educationData.FirstOrDefault(e => e.Id == id);
            if (selectedEducation != null)
            {
                education.AddEducationdetails(selectedEducation.CollegeName, selectedEducation.Country, selectedEducation.Title, selectedEducation.Degree, selectedEducation.GraduationYear);
                _scenarioContext["education"] = selectedEducation;
            }
        }
        [Then(@"education details with destructive data should not be added succesfully to my profile")]
        public void ThenEducationDetailsWithDestructiveDataShouldNotBeAddedSuccesfullyToMyProfile()
        {
            educationAssertion.AddDestructiveEducation();
        }



        [When(@"User deletes an existing education record in ""([^""]*)"" with ID (.*)")]
        public void WhenUserDeletesAnExistingEducationRecordInWithID(string jsonfilename, int id)
        {
            var educationData = JSONHelper.LoadData<List<EducationDataModel>>(jsonfilename);
            var selectedEducation = educationData.FirstOrDefault(e => e.Id == id);
            if (selectedEducation != null)
            {
                education.AddEducationdetails(selectedEducation.CollegeName, selectedEducation.Country, selectedEducation.Title, selectedEducation.Degree, selectedEducation.GraduationYear);
                education.Deleteeducationdetails();
            }
        }

        [Then(@"education record should be succesfully deleted")]
        public void ThenEducationRecordShouldBeSuccesfullyDeleted()
        {
            educationAssertion.AssertDeleteEducation();
        }
    }
}






