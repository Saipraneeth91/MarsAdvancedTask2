using MarsAdvancedTask2.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTask2.Pages.Components
{
    public class Certificationcomponent
    {

        private readonly IWebDriver driver;
        private readonly ElementUtil eleUtil;
        public Certificationcomponent(IWebDriver driver)
        {
            this.driver = driver;
            eleUtil = new ElementUtil(driver);
        }
        //By Locators
        private readonly By certificationtab = By.XPath("//a[text()='Certifications']");
        private readonly By addnewbutton = By.XPath("//div[@class='ui bottom attached tab segment tooltip-target active']//div[contains(@class,'ui teal button')][normalize-space()='Add New']");
        private readonly By certificate = By.Name("certificationName");
        private readonly By certifiedfrom = By.Name("certificationFrom");
        private readonly By year = By.XPath("//select[@name = 'certificationYear']");
        private readonly By add = By.XPath("//input[@value='Add']");
        private readonly By editicon = By.XPath("//tbody[last()]/tr[last()]/td[4]/span[1]/i[1]");
        private readonly By updatebutton = By.XPath("//input[@value='Update']");
        private readonly By deleteicon = By.XPath("//tbody[last()]/tr[last()]/td[4]/span[2]/i[1]");
        private readonly By cancelbutton = By.XPath("//input[@value='Cancel']");
        private readonly By notificationtext = By.XPath("//div[@class='ns-box-inner']");
        private readonly By lastrecordcertificate = By.XPath("//th[text()='Certificate']/ancestor::thead//following-sibling::tbody[last()]/tr/td[1]");
        private readonly By lastrecordcertificatefrom = By.XPath("//th[text()='From']/ancestor::thead//following-sibling::tbody[last()]/tr/td[2]");
        private readonly By lastrecordcertificateyear = By.XPath("//th[text()='Year']/ancestor::thead//following-sibling::tbody[last()]/tr/td[3]");
        private readonly By logoutbutton = By.XPath("//button[normalize-space()='Sign Out']");
        public ReadOnlyCollection<IWebElement> Rows => driver.FindElements(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody"));

        public void rendercertificationcomponents()
        {
            try
            {
                eleUtil.getElement(certificationtab);
                eleUtil.getElement(addnewbutton);
                eleUtil.getElement(certificate);
                eleUtil.getElement(certifiedfrom);
                eleUtil.getElement(year);
                eleUtil.getElement(add);
                eleUtil.getElement(editicon);
                eleUtil.getElement(updatebutton);
                eleUtil.getElement(deleteicon);
                eleUtil.getElement(cancelbutton);
                eleUtil.getElement(notificationtext);
                eleUtil.getElement(lastrecordcertificate);
                eleUtil.getElement(lastrecordcertificatefrom);
                eleUtil.getElement(lastrecordcertificateyear);
                eleUtil.getElement(logoutbutton);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
            public void Addcertification(string certificatename, string certificationfrom, string certificationyear)
        {
            rendercertificationcomponents();
            eleUtil.doClick(certificationtab);
            eleUtil.doClick(addnewbutton);
            eleUtil.doSendKeys(certificate, certificatename);
            eleUtil.doSendKeys(certifiedfrom, certificationfrom);
            eleUtil.doSendKeys(year, certificationyear);
            eleUtil.doClick(add);
            Thread.Sleep(4000);
        }
        public void GoTocertificationTab()
        {
            Thread.Sleep(3000);
            eleUtil.doClick(certificationtab);
        }
        public string Getnotificationtext()
        {
            return eleUtil.getText(notificationtext);
        }
        public string Getlastrecordcertificate()
        {
            return eleUtil.getText(lastrecordcertificate);
        }
        public void ClearCertification()
        {
            Wait.WaitToBeClickable(driver, certificationtab, Wait.LONG_DEFAULT_WAIT);
            eleUtil.doClick(certificationtab);
            int totalrows = Rows.Count;
            Console.WriteLine(totalrows);

            for (int i = 0; i < totalrows; i = i + 1)
            {
                Wait.WaitToBeClickable(driver, certificationtab, Wait.LONG_DEFAULT_WAIT);
                eleUtil.doClick(deleteicon);
                Thread.Sleep(2000);
            }
            eleUtil.doClick(logoutbutton);
        }
        public void Logout()
        {
            eleUtil.doClick(logoutbutton);

        }
        public void Deletecertification(string certification)
        {
            eleUtil.doClick(certificationtab);
            By deletebycertification = By.XPath("//td[text()='" + certification + "']/following-sibling::td/span[@class='button'][2]");
            Wait.WaitToBeClickable(driver, deletebycertification, Wait.MEDIUM_DEFAULT_WAIT);
            eleUtil.doClick(deletebycertification);
            Thread.Sleep(3000); 

        }


    }
}

