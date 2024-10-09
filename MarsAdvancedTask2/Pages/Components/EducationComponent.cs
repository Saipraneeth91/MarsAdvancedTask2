using MarsAdvancedTask2.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTask2.Pages.Components
{
    public class EducationComponent
    {
        private readonly IWebDriver driver;
        private readonly ElementUtil eleUtil;

        public EducationComponent(IWebDriver driver)
        {
            this.driver = driver;
            eleUtil = new ElementUtil(driver);
        }
        //By locators
        private readonly By educationtab = By.XPath("//a[text()='Education']");
        private readonly By addnewbutton = By.XPath("//div[@class='ui bottom attached tab segment tooltip-target active']//div[contains(@class,'ui teal button')][normalize-space()='Add New']");
        private readonly By collegenametextbox = By.XPath("//input[@name='instituteName']");
        private readonly By country = By.XPath("//select[@name='country']");
        private readonly By title = By.XPath("//select[@name='title']");
        private readonly By degree = By.XPath("//input[@name='degree']");
        private readonly By year = By.XPath("//select[@name='yearOfGraduation']");
        private readonly By addbutton = By.XPath("//input[@value='Add']");
        private readonly By editicon = By.XPath("//table[@class='ui fixed table']/tbody/tr/td[6]/span[1]/i");
        private readonly By updatebutton = By.XPath("//input[@value='Update']");
        private readonly By delete = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[6]/span[2]/i");
        private readonly By cancelbutton = By.XPath("//input[@value='Cancel']");
        private readonly By logoutbutton = By.XPath("//button[normalize-space()='Sign Out']");
        private readonly By lastrecordtitle = By.XPath("//th[text()='Title']/ancestor::thead//following-sibling::tbody[last()]/tr[last()]/td[3]");
        private readonly By lastrecordcountry = By.XPath("//th[text()='Country']/ancestor::thead//following-sibling::tbody[last()]/tr[last()]/td[1]");
        private readonly By lastrecorduniversity = By.XPath("//th[text()='University']/ancestor::thead//following-sibling::tbody[last()]/tr[last()]/td[2]");
        private readonly By lastrecorddegree = By.XPath("//th[text()='Degree']/ancestor::thead//following-sibling::tbody[last()]/tr[last()]/td[4]");
        private readonly By lastrecordyear = By.XPath("//th[text()='Graduation Year']/ancestor::thead//following-sibling::tbody[last()]/tr[last()]/td[5]");
        private readonly By notificationtext = By.XPath("//div[@class='ns-box-inner']");
        public ReadOnlyCollection<IWebElement> rows => driver.FindElements(By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody"));
        public void rendereducationcomponents()
        {
            try
            {
                eleUtil.getElement(addnewbutton);
                eleUtil.getElement(collegenametextbox);
                eleUtil.getElement(country);
                eleUtil.getElement(title);
                eleUtil.getElement(degree);
                eleUtil.getElement(year);
                eleUtil.getElement(addbutton);
                eleUtil.getElement(editicon);
                eleUtil.getElement(updatebutton);
                eleUtil.getElement(delete);
                eleUtil.getElement(cancelbutton);
                eleUtil.getElement(logoutbutton);
                eleUtil.getElement(lastrecordtitle);
                eleUtil.getElement(lastrecordcountry);
                eleUtil.getElement(lastrecorduniversity);
                eleUtil.getElement(lastrecorddegree);
                eleUtil.getElement(lastrecordyear);
                eleUtil.getElement(notificationtext);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void rendereducationtab()
        {
            try
            {
                eleUtil.getElement(educationtab);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public void GoToEducationTab()
        {
            rendereducationtab();
            eleUtil.doClick(educationtab);
        }
        public void AddEducationdetails(string collegename, string countryname, string titlename, string degreename, string yearofeducation)
        {
            rendereducationcomponents();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Wait.WaitToBeVisible(driver, addnewbutton, Wait.LONG_DEFAULT_WAIT);
            eleUtil.doClick(addnewbutton);
            Wait.WaitToBeVisible(driver, collegenametextbox, Wait.LONG_DEFAULT_WAIT);
            eleUtil.doSendKeys(collegenametextbox, collegename);
            Wait.WaitToBeVisible(driver, country, Wait.LONG_DEFAULT_WAIT);
            eleUtil.doSelectDropDownByVisibleText(country, countryname);
            eleUtil.doSelectDropDownByVisibleText(title, titlename);
            eleUtil.doSendKeys(degree, degreename);
            eleUtil.doSelectDropDownByVisibleText(year, yearofeducation);
            eleUtil.doClick(addbutton);
           
        }
      
        public void Deleteeducationdetails()
        {
            eleUtil.doClick(educationtab);
            Wait.WaitToBeVisible(driver, delete, Wait.LONG_DEFAULT_WAIT);
            eleUtil.doClick(delete);
            Thread.Sleep(1000);
        }
        public void ClearEducation()
        {
            Wait.WaitToBeClickable(driver, educationtab, Wait.LONG_DEFAULT_WAIT);
            eleUtil.doClick(educationtab);
            int totalrows = rows.Count;
            Console.WriteLine(totalrows);
            for (int i = 0; i < totalrows; i = i + 1)
            {
                Wait.WaitToBeClickable(driver, delete, Wait.LONG_DEFAULT_WAIT);
                eleUtil.doClick(delete);

            }
        }

        public void Deleteeducation(string education)
        {
            eleUtil.doClick(educationtab);
            By deletebyeducation = By.XPath("//td[text()='" + education + "']/following-sibling::td/span[@class='button'][2]");
            Wait.WaitToBeClickable(driver, deletebyeducation, Wait.MEDIUM_DEFAULT_WAIT);
            eleUtil.doClick(deletebyeducation);

        }

        public void Logout()
        {
            eleUtil.doClick(logoutbutton);

        }
        public string Getlastrecordtitle()
        {
            return eleUtil.getText(lastrecordtitle);
        }
        public string Getlastrecordcountry()
        {
            return eleUtil.getText(lastrecordcountry);
        }
        public string Getlastrecorduniversity()
        {
            return eleUtil.getText(lastrecorduniversity);
        }
        public string GetlastrecordDegree()
        {
            return eleUtil.getText(lastrecorddegree);
        }
        public string GetlastrecordYear()
        {
            return eleUtil.getText(lastrecordyear);
        }
        public string Getnotificationtext()
        {
            return eleUtil.getText(notificationtext);
        }

    }
}
