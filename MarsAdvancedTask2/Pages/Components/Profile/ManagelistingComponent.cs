using MarsAdvancedTask2.Models;
using MarsAdvancedTask2.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RazorEngine.Compilation.ImpromptuInterface.InvokeExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTask2.Pages.Components.Profile
{
    public class ManagelistingComponent
    {
        private readonly IWebDriver driver;
        private readonly ElementUtil elementUtil;
        public ManagelistingComponent(IWebDriver driver)
        {
            this.driver = driver;
            elementUtil = new ElementUtil(driver);
        }
        // By Locators
        private By Shareskill = By.XPath("//a[@class='ui basic green button']");
        private By manageListingTab = By.LinkText("Manage Listings");
        private By title = By.Name("title");
        private By description = By.Name("description");
        private By category = By.Name("categoryId");
        private By subCategory = By.Name("subcategoryId");
        private By tags = By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[4]/div[2]/div/div/div/div/input");
        private By serviceTypeHourlyBasis = By.XPath("//input[@name='serviceType' and @value ='0']");
        private By serviceTypeOneOff = By.XPath("//input[@name='serviceType' and @value ='1']");
        private By locationTypeOnsite = By.XPath("//input[@name='locationType' and @value ='0']");
        private By locationTypeOnline = By.XPath("//input[@name='locationType' and @value ='1']");
        private By startDate = By.XPath("//*[@placeholder='Start date']");
        private By endDate = By.XPath("//*[@placeholder='End date']");
        private By skillTradeSkillExchange = By.XPath("//input[@name='skillTrades' and @value ='true']");
        private By skillTradeCredit = By.XPath("//input[@name='skillTrades' and @value ='false']");
        private By skillExchange = By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[8]/div[4]/div/div/div/div/div/input");
        private By activeStatus = By.XPath("//input[@name='isActive' and @value ='true']");
        private By hiddenStatus = By.XPath("//input[@name='isActive' and @value ='false']");
        private By saveButton = By.XPath("//*[@value='Save']");
        private By editIcon = By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[8]/div/button[2]/i");
        private By viewIcon = By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[8]/div/button[1]/i");
        private By deleteIcon = By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[8]/div/button[3]/i");
        private By yesButton = By.XPath("//*[@class='ui icon positive right labeled button']");
        private By messageWindow = By.XPath("//*[@class='ns-box-inner']");
        private By toggleIcon = By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[7]/div/input");
        public void RenderShareSkillElements()
        {
            try
            {
                elementUtil.getElement(Shareskill);
                elementUtil.getElement(manageListingTab);
                elementUtil.getElement(title);
                elementUtil.getElement(description);
                elementUtil.getElement(category);
                elementUtil.getElement(tags);
                elementUtil.getElement(serviceTypeHourlyBasis);
                elementUtil.getElement(serviceTypeOneOff);
                elementUtil.getElement(locationTypeOnsite);
                elementUtil.getElement(locationTypeOnline);
                elementUtil.getElement(startDate);
                elementUtil.getElement(endDate);
                elementUtil.getElement(skillExchange);
                elementUtil.getElement(saveButton);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public void ClickEditIcon()
        {
            Thread.Sleep(3000);
            elementUtil.doClick(editIcon);
        }

        public void ViewListing()
        {
            elementUtil.doClick(viewIcon);

        }

        public void DeleteListing()
        {
            elementUtil.doClick(deleteIcon);
            Thread.Sleep(1000);
            elementUtil.doClick(yesButton);
            Thread.Sleep(3000);
        }

        public string GetSkillTitle()
        {
            IWebElement skillTitle = elementUtil.getElement(By.XPath("//*[@class='skill-title']"));
            return skillTitle.Text;
        }

        public void ClickToggle()
        {
            elementUtil.doClick(toggleIcon);
            Thread.Sleep(3000);
        }

       public void AddListing(ListingData listing)
        {
            elementUtil.doClick(Shareskill);
            Thread.Sleep(3000);
            elementUtil.doSendKeys(title, listing.Title);
            elementUtil.doSendKeys(description, listing.Description);

            SelectElement newCategory = new SelectElement(elementUtil.getElement(category));
            newCategory.SelectByText(listing.Category);
            Thread.Sleep(1000);

            SelectElement newSubCategory = new SelectElement(elementUtil.getElement(subCategory));
            newSubCategory.SelectByText(listing.SubCategory);

            elementUtil.doSendKeys(tags, listing.Tags);
            elementUtil.doSendKeys(tags, Keys.Enter);

            if (listing.ServiceType == "Hourly basis service")
            {
                elementUtil.doClick(serviceTypeHourlyBasis);
            }
            else
            {
                elementUtil.doClick(serviceTypeOneOff);
            }

            if (listing.LocationType == "On-site")
            {
                elementUtil.doClick(locationTypeOnsite);
            }
            else
            {
                elementUtil.doClick(locationTypeOnline);
            }

            elementUtil.doClear(startDate);
            elementUtil.doSendKeys(startDate, listing.StartDate);
            elementUtil.doClear(endDate);
            elementUtil.doSendKeys(endDate, listing.EndDate);

            if (listing.SkillTrade == "Skill-exchange")
            {
                elementUtil.doClick(skillTradeSkillExchange);
                elementUtil.doSendKeys(skillExchange, listing.SkillExchange);
                elementUtil.doSendKeys(skillExchange, Keys.Enter);
            }
            else
            {
                elementUtil.doClick(skillTradeCredit);
                Thread.Sleep(3000);
                elementUtil.doSendKeys(skillTradeCredit, listing.Amount);
            }

            if (listing.ActiveStatus == "Active")
            {
                elementUtil.doClick(activeStatus);
            }
            else
            {
                elementUtil.doClick(hiddenStatus);
            }

            elementUtil.doClick(saveButton);
            Thread.Sleep(3000);
        }


    public void UpdateListing(EditListingData editlisting)
            {
                ClickEditIcon();
                RenderShareSkillElements();
                elementUtil.doClear(title);
                elementUtil.doSendKeys(title, editlisting.NewTitle);
                elementUtil.doClear(description);
                elementUtil.doSendKeys(description, editlisting.NewDescription);

                SelectElement newCategory = new SelectElement(elementUtil.getElement(category));
                newCategory.SelectByText(editlisting.NewCategory);
                Thread.Sleep(1000);

                SelectElement newSubCategory = new SelectElement(elementUtil.getElement(subCategory));
                newSubCategory.SelectByText(editlisting.NewSubCategory);

                elementUtil.doClear(tags);
                elementUtil.doSendKeys(tags, editlisting.NewTags);
                elementUtil.doSendKeys(tags, Keys.Enter);

                if (editlisting.NewServiceType == "Hourly basis service")
                {
                    elementUtil.doClick(serviceTypeHourlyBasis);
                }
                else
                {
                    elementUtil.doClick(serviceTypeOneOff);
                }

                if (editlisting.NewLocationType == "On-site")
                {
                    elementUtil.doClick(locationTypeOnsite);
                }
                else
                {
                    elementUtil.doClick(locationTypeOnline);
                }

                elementUtil.doClear(startDate);
                elementUtil.doSendKeys(startDate, editlisting.NewStartDate);
                elementUtil.doClear(endDate);
                elementUtil.doSendKeys(endDate, editlisting.NewEndDate);

                if (editlisting.NewStartDate == "Skill-exchange")
                {
                    elementUtil.doClick(skillTradeSkillExchange);
                    elementUtil.doSendKeys(skillExchange, editlisting.NewSkillExchange);
                    elementUtil.doSendKeys(skillExchange, Keys.Enter);
                }
                else
                {
                    elementUtil.doClick(skillTradeCredit);
                    By credit = By.XPath("//*[@placeholder='Amount']");
                    elementUtil.doSendKeys(credit, editlisting.NewAmount);
                }

                if (editlisting.NewActiveStatus == "Active")
                {
                    elementUtil.doClick(activeStatus);
                }
                else
                {
                    elementUtil.doClick(hiddenStatus);
                }

                elementUtil.doClick(saveButton);
                Thread.Sleep(3000);
            }

            public string ListCategory()
            {
                RenderManageListingElements();
                IWebElement expectedCategory = elementUtil.getElement(By.XPath("//*[@class='ui striped table']//tr[1]//td[2]"));
                return expectedCategory.Text;
            }

            public string ListTitle()
            {
                RenderManageListingElements();
                IWebElement expectedTitle = elementUtil.getElement(By.XPath("//*[@class='ui striped table']//tr[1]//td[3]"));
                return expectedTitle.Text;
            }

            public string ListDescription()
            {
                RenderManageListingElements();
                IWebElement expectedDescription = elementUtil.getElement(By.XPath("//*[@class='ui striped table']//tr[1]//td[4]"));
                return expectedDescription.Text;
            }

        private void RenderManageListingElements()
        {
            try
            {
                By ele = By.XPath("//*[@class='ui striped table']//tr[1]//td[3]");
                elementUtil.getElement(ele);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Manage Listing elements are not rendered.");
            }
        }
            public string RenderPopUpMessage()
            {
                By messageWindow = By.XPath("//div[@class='ns-box-inner']");
                return elementUtil.getText(messageWindow);

            }
        }
        }
    