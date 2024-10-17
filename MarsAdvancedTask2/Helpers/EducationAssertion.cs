using MarsAdvancedTask2.Models;
using MarsAdvancedTask2.Pages.Components;
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
    public class EducationAssertion
    {
        private IWebDriver driver;
        private ElementUtil eleutil;
        private EducationComponent educationComponent;

        public EducationAssertion(IWebDriver driver)
        {
            this.driver = driver;
            eleutil = new ElementUtil(driver);
            educationComponent = new EducationComponent(driver);
        }
        public void AssertValidAddRecord(EducationDataModel expectedEducation)
        {
            var lastTitle = educationComponent.Getlastrecordtitle();
            var lastCountry = educationComponent.Getlastrecordcountry();
            var lastUniversity = educationComponent.Getlastrecorduniversity();
            var lastDegree = educationComponent.GetlastrecordDegree();
            var lastYear = educationComponent.GetlastrecordYear();
            Assert.That(lastTitle, Is.EqualTo(expectedEducation.Title), "The last record title does not match.");
            Assert.That(lastCountry, Is.EqualTo(expectedEducation.Country), "The last record country does not match.");
            Assert.That(lastUniversity, Is.EqualTo(expectedEducation.CollegeName), "The last record university does not match.");
            Assert.That(lastDegree, Is.EqualTo(expectedEducation.Degree), "The last record degree does not match.");
            Assert.That(lastYear, Is.EqualTo(expectedEducation.GraduationYear), "The last record year does not match.");
        }

        public void AddEducationWithSpecialCharacters()
        {
            string actualNotification = educationComponent.Getnotificationtext();
            string expectedNotification = "Specialcharacters are not allowed";
            Assert.That(actualNotification,Is.EqualTo(expectedNotification));
        }

        public void AddBlankEducation()
        {
            string actualNotification = educationComponent.Getnotificationtext();
            string expectedNotification = "Please enter all the fields";
            Assert.That(actualNotification, Is.EqualTo(expectedNotification));
        }

        public void AddDestructiveEducation()
        {
            string actualNotification = educationComponent.Getnotificationtext();
            string expectedNotification = "Length of fields exceed limit defined";
            Assert.That(actualNotification, Is.EqualTo(expectedNotification));
        }

            public void AssertDeleteEducation()
            {
               string actualNotification = educationComponent.Getnotificationtext();
               string expectedNotification = "Education entry successfully removed";
              Assert.That(actualNotification, Is.EqualTo(expectedNotification));
        }
    }
    }

