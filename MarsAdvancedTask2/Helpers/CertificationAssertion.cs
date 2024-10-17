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
    public class CertificationAssertion
    {
        private IWebDriver driver;
        private ElementUtil eleutil;
        private Certificationcomponent certificationComponent;

        public CertificationAssertion(IWebDriver driver)
        {
            this.driver = driver;
            eleutil = new ElementUtil(driver);
            certificationComponent = new Certificationcomponent(driver);
        }

        public void AssertValidAddCertification(CertificationDataModel expectedCertification)
        {

            string actualCertificate = certificationComponent.Getlastrecordcertificate();
            string expectedCertificate = expectedCertification.Certificate;
            Console.WriteLine(expectedCertificate);
            if (actualCertificate == expectedCertificate)
            {
                Assert.Pass("Record Added Succesfully");
            }

        }
        public void AddCertificationWithSpecialCharacters()
        {
            string actualNotification = certificationComponent.Getnotificationtext();
            string expectedNotification = "Specialcharacters are not allowed";
            Assert.That(actualNotification, Is.EqualTo(expectedNotification));

        }

        public void AddBlankCertification()
        {
            string actualNotification = certificationComponent.Getnotificationtext();
            string expectedNotification = "Please enter Certification Name, Certification From and Certification Year";
            Assert.That(actualNotification, Is.EqualTo(expectedNotification));
        }

        public void AddDestructiveCertification()
        {
            string actualNotification = certificationComponent.Getnotificationtext();
            string expectedNotification = "Length of fields exceed limit defined";
            Assert.That(actualNotification, Is.EqualTo(expectedNotification));

        }

        public void AssertDeleteCertification(CertificationDataModel expectedCertification)
        {
            string actualNotification = certificationComponent.Getnotificationtext();
            string expectedNotification = expectedCertification.Certificate + " has been deleted from your certification";
            Assert.That(actualNotification, Is.EqualTo(expectedNotification));

        }
    }
}


