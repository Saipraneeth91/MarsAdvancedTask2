using MarsAdvancedTask2.Models;
using MarsAdvancedTask2.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MarsAdvancedTask2.Pages.Components
{
    public class PasswordComponent
    {
        private readonly IWebDriver driver;
        private readonly ElementUtil eleUtil;
        private readonly WebDriverWait wait;
        private readonly Actions action;

        public PasswordComponent(IWebDriver driver)
        {
            this.driver = driver;
            eleUtil = new ElementUtil(driver);
            action = new Actions(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // By locators
        private readonly By Welcometab = By.XPath("/html[1]/body[1]/div[1]/div[1]/div[1]/div[2]/div[1]/span[1]");
        private readonly By ChangePasswordDropdown = By.XPath("/html[1]/body[1]/div[1]/div[1]/div[1]/div[2]/div[1]/span[1]/div[1]/a[2]");
        private readonly By CurrentPasswordTextbox = By.XPath("//input[@placeholder=\"Current Password\"]");
        private readonly By NewPasswordTextbox = By.XPath("//input[@placeholder=\"New Password\"]");
        private readonly By ConfirmPasswordTextbox = By.XPath("//input[@placeholder=\"Confirm Password\"]");
        private readonly By SaveButton = By.XPath("//button[@type='button']");
        private readonly By successMessage = By.XPath("//div[@class='ns-box-inner']");
        public void renderChangePasswordDropdown()
        {
            try
            {
                eleUtil.getElement(ChangePasswordDropdown);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void renderAddComponents()
        {
            try
            {
                eleUtil.getElement(CurrentPasswordTextbox);
                eleUtil.getElement(NewPasswordTextbox);
                eleUtil.getElement(ConfirmPasswordTextbox);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void renderSaveComponents()
        {
            try
            {
                eleUtil.getElement(SaveButton);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Change_Password(PasswordModel passwordData)
        {
            try
            {
                IWebElement welcometab = wait.Until(ExpectedConditions.ElementIsVisible(Welcometab));
                action.MoveToElement(welcometab).Perform();
                IWebElement changePasswordDropdown = wait.Until(ExpectedConditions.ElementToBeClickable(ChangePasswordDropdown));
                changePasswordDropdown.Click();
                renderAddComponents();
                Thread.Sleep(2000);
                eleUtil.doClick(CurrentPasswordTextbox);
                eleUtil.doSendKeys(CurrentPasswordTextbox, passwordData.CurrentPassword);
                eleUtil.doClick(NewPasswordTextbox);
                eleUtil.doSendKeys(NewPasswordTextbox, passwordData.NewPassword);
                eleUtil.doSendKeys(ConfirmPasswordTextbox, passwordData.ConfirmPassword);
                renderSaveComponents();
                eleUtil.doClick(SaveButton);
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while changing password: {ex.Message}");
            }
        }
            public string renderMessageBoxTestComponent()
            {
                By messageBox = By.XPath("//div[@class='ns-box-inner']");
                return eleUtil.getText(messageBox);

            }


        }
    }

