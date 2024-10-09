using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsAdvancedTask2.Utilities;


namespace MarsAdvancedTask2.Pages.Components.Profile
{
    public class DescriptionComponent
    {
        private readonly IWebDriver driver;
        private readonly ElementUtil eleUtil;

        public DescriptionComponent(IWebDriver driver)
        {
            this.driver = driver;
            eleUtil = new ElementUtil(driver);
        }
        // By Locators
        private readonly By Descriptionicon = By.XPath("//i[@class='outline write icon']");
        private readonly By enterDescription = By.Name("value");
        private By saveBtn = By.XPath("//button[@type='button']");
        private By addedDescription = By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/div/div/div/span");
        private By Descriptionbox = By.XPath("//textarea[@placeholder='Please tell us about any hobbies, additional expertise, or anything else you’d like to add.']");
        private IWebElement messageBox;
        public void renderDescriptionComponents()
        {
            try
            {
                eleUtil.getElement(enterDescription);
                eleUtil.getElement(saveBtn);
                eleUtil.getElement(Descriptionicon);
                eleUtil.getElement(addedDescription);
                eleUtil.getElement(Descriptionbox);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public string renderMessageBoxTestComponent()
        {
            By messageBox = By.XPath("//div[@class='ns-box-inner']");
            return eleUtil.getText(messageBox);

        }
        public void addAndUpdateDescriptionDetails(string description)
        {
            eleUtil.doClick(Descriptionicon);   
            renderDescriptionComponents();
            eleUtil.doClear(enterDescription);
            eleUtil.doSendKeys(Descriptionbox,description);
            eleUtil.doClick(saveBtn);
            Thread.Sleep(2000);

        }
    }
}
 