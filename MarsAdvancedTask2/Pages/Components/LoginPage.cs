using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsAdvancedTask2.Utilities;

namespace MarsAdvancedTask2.Pages.Components
{
    public class LoginPage 
    {
        private ElementUtil eleUtil;
        private readonly IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            eleUtil = new ElementUtil(driver);
        }
           private readonly By signin = By.XPath("//a[contains(text(),'Sign In')]");
           private readonly By emailaddress = By.XPath("//*[@placeholder='Email address']");
           private readonly By password = By.XPath("//*[@placeholder='Password']");
           private readonly By login = By.XPath("//*[text()='Login']");
        public void RenderLoginComponents()
        {
            try
            {
                eleUtil.getElement(signin);
                eleUtil.getElement(emailaddress);
                eleUtil.getElement(password);
                eleUtil.getElement(login);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void LoginActions(string username, string pwd)
        {
            try
            {
                RenderLoginComponents();
                //navigate to Mars Application
                driver.Navigate().GoToUrl("http://localhost:5000/");
                //click sign in
                Thread.Sleep(2000);
                eleUtil.doClick(signin);
                //enter email address
                eleUtil.doSendKeys(emailaddress, username);
                // enter password
                eleUtil.doSendKeys(password, pwd);
                // click login
                eleUtil.doClick(login);
                Thread.Sleep(2000);
            }
            catch (WebDriverException ex)
            {
                Console.WriteLine($"WebDriverException occurred: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception occurred: {ex.Message}");
                throw;
            }
        }

    }
}

