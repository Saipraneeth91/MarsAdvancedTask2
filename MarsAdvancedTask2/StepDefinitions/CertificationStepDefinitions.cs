using MarsAdvancedTask2.Helpers;
using MarsAdvancedTask2.Models;
using MarsAdvancedTask2.Pages.Components;
using OpenQA.Selenium;
using RazorEngine;
using System;
using TechTalk.SpecFlow;

namespace MarsAdvancedTask2.StepDefinitions
{
    [Binding]
    public class CertificationStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly LoginPage loginMars;
        private readonly Certificationcomponent certification;
        private readonly CertificationAssertion certificationAssertion;
        private readonly ScenarioContext _scenarioContext;
        public CertificationStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            this.driver = driver;
            loginMars = new LoginPage(driver);
            certification = new Certificationcomponent(driver);
            certificationAssertion = new CertificationAssertion(driver);
            _scenarioContext = scenarioContext;
        }

        [Given(@"User logged in to Mars Application and Navigates to certification tab")]
        public void GivenUserLoggedInToMarsApplicationAndNavigatesToCertificationTab()
        {
            loginMars.LoginActions("saipraneethg.91@gmail.com", "Praneeth@1");
            certification.GoTocertificationTab();
        }

        [When(@"User adds a new certification from json file ""([^""]*)"" with ID (.*)")]
        public void WhenUserAddsANewCertificationFromJsonFileWithID(string jsonfilename, int id)
        {

            
            var certificationData = JSONHelper.LoadData<List<CertificationDataModel>>(jsonfilename);
            var selectedCertification = certificationData.FirstOrDefault(e => e.Id == id);
            if (selectedCertification != null)
            {
                certification.Addcertification( selectedCertification.Certificate,selectedCertification.From,selectedCertification.Year);
                _scenarioContext["certification"] = selectedCertification;
            }

        }

        [Then(@"certification details should be added succesfully to my profile")]
        public void ThenCertificationDetailsShouldBeAddedSuccesfullyToMyProfile()
        {

           var expectedEducation = JSONHelper.LoadData<List<CertificationDataModel>>("ValidCertificationDetails.json").First();

            certificationAssertion.AssertValidAddCertification(expectedEducation);
        }
        [When(@"User  adds a special character certification record from json file ""([^""]*)"" with ID (.*)")]
        public void WhenUserAddsASpecialCharacterCertificationRecordFromJsonFileWithID(string jsonfilename, int id)
        {
            var certificationData = JSONHelper.LoadData<List<CertificationDataModel>>(jsonfilename);
            var selectedCertification = certificationData.FirstOrDefault(e => e.Id == id);
            if (selectedCertification != null)
            {
                certification.Addcertification(selectedCertification.Certificate, selectedCertification.From, selectedCertification.Year);
                // Store the added education details in ScenarioContext
                _scenarioContext["certification"] = selectedCertification;
            }

        }

        [Then(@"certification details with special characters should not be added succesfully to my profile")]
        public void ThenCertificationDetailsWithSpecialCharactersShouldNotBeAddedSuccesfullyToMyProfile()
        {
            certificationAssertion.AddCertificationWithSpecialCharacters();
        }

        [Then(@"certification details should not be added succesfully to my profile")]
        public void ThenCertificationDetailsShouldNotBeAddedSuccesfullyToMyProfile()
        {
        }

        [When(@"User  adds a Blank value certification record from json file ""([^""]*)"" with ID (.*)")]
        public void WhenUserAddsABlankValueCertificationRecordFromJsonFileWithID(string jsonfilename, int id)
        {
            var certificationData = JSONHelper.LoadData<List<CertificationDataModel>>(jsonfilename);
            var selectedCertification = certificationData.FirstOrDefault(e => e.Id == id);
            if (selectedCertification != null)
            {
                certification.Addcertification(selectedCertification.Certificate, selectedCertification.From, selectedCertification.Year);
      
            }
        }

        [Then(@"certification with blank values should not be added succesfully to my profile")]
        public void ThenCertificationWithBlankValuesShouldNotBeAddedSuccesfullyToMyProfile()
        {
            certificationAssertion.AddBlankCertification();
        }

        [When(@"User deletes an existing certification record in ""([^""]*)"" with ID (.*)")]
        public void WhenUserDeletesAnExistingCertificationRecordInWithID(string jsonfilename, int id)
        {
            var certificationData = JSONHelper.LoadData<List<CertificationDataModel>>(jsonfilename);
            var selectedCertification = certificationData.FirstOrDefault(e => e.Id == id);
            if (selectedCertification != null)
            {
                certification.Addcertification(selectedCertification.Certificate, selectedCertification.From, selectedCertification.Year);
                certification.Deletecertification(selectedCertification.Certificate);
            }
        }

        [When(@"User  adds a destructive data certification record from json file ""([^""]*)"" with ID (.*)")]
        public void WhenUserAddsADestructiveDataCertificationRecordFromJsonFileWithID(string jsonfilename, int id)
        {
            var certificationData = JSONHelper.LoadData<List<CertificationDataModel>>(jsonfilename);
            var selectedCertification = certificationData.FirstOrDefault(e => e.Id == id);
            if (selectedCertification != null)
            {
                certification.Addcertification(selectedCertification.Certificate, selectedCertification.From, selectedCertification.Year);
                _scenarioContext["certification"] = selectedCertification;
            }
        }


        [Then(@"certification details with destructive data should not be added succesfully to my profile")]
        public void ThenCertificationDetailsWithDestructiveDataShouldNotBeAddedSuccesfullyToMyProfile()
        {

            certificationAssertion.AddDestructiveCertification();
        }


        [Then(@"certification record should be succesfully deleted")]
        public void ThenCertificationRecordShouldBeSuccesfullyDeleted()
        {

            var expectedCertification = JSONHelper.LoadData<List<CertificationDataModel>>("ValidCertificationDetails.json").First();

            certificationAssertion.AssertDeleteCertification(expectedCertification);
        }
    }
}


