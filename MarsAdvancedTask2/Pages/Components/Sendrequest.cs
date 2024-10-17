using MarsAdvancedTask2.TestModel;
using MarsAdvancedTask2.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTask2.Pages.Components
{
    public class Sendrequest
    {
            private ElementUtil eleutil;
            private IWebDriver driver;
            public Sendrequest(IWebDriver driver)
            {
                eleutil = new ElementUtil(driver);
                this.driver = driver;

            }
            private By searchSkillicon = By.XPath("//div[@class='ui secondary menu']//i[@class='search link icon']");
            private By searchbar = By.XPath("//input[@placeholder='Search skills']");
           
            public void SearchskillbyCategories(SearchskillModel skillcategory)
            {
             
                eleutil.doSendKeys(searchbar, skillcategory.Skillname);
           
                eleutil.doClick(searchSkillicon);

                By skillName = By.XPath($"//p[text()='{skillcategory.Skillname}']");
               Wait.WaitToBeClickable(driver, skillName, Wait.LONG_DEFAULT_WAIT);
                eleutil.doClick(skillName);
  
                By clickrequest = By.XPath("//div[contains(@class,'button')]");
                eleutil.doClick(clickrequest);
                By yes = By.XPath("//button[normalize-space()='Yes']");
            Wait.WaitToBeClickable(driver, yes, Wait.LONG_DEFAULT_WAIT);
            eleutil.doClick(yes);
            Thread.Sleep(3000);
        }
    }    
}
