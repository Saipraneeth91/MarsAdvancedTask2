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

namespace MarsAdvancedTask2.Pages.Components.Profile
{
    public class ManageRequest
    {

        private readonly IWebDriver driver;
        private readonly ElementUtil eleUtil;
        private readonly WebDriverWait wait;
        private readonly Actions action;

        public ManageRequest(IWebDriver driver)
        {
            this.driver = driver;
            eleUtil = new ElementUtil(driver);
            action = new Actions(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        //By Locators
        private readonly By manageRequeststab = By.XPath("/html[1]/body[1]/div[1]/div[1]/section[1]/div[1]/div[1]");
        private readonly By receivedRequestsLink = By.XPath("//a[normalize-space()='Received Requests']");
        private readonly By sentRequestsLink = By.XPath("//a[normalize-space()='Sent Requests']");
        private readonly By messageWindow = By.XPath("//*[@class='ns-box-inner']");
        private readonly By acceptButton = By.XPath("//button[normalize-space()='Accept']");
        private readonly By declineButton = By.XPath("//*[@id=\"received-request-section\"]/div[2]/div[1]/table/tbody/tr[1]/td[8]/button[2]");
        private readonly By completeButton = By.XPath("//tbody/tr[1]/td[8]/button[1]");
        private readonly By withdrawButton = By.XPath("//*[@id=\"sent-request-section\"]/div[2]/div[1]/table/tbody/tr[1]/td[8]/button");
        public void RenderManageRequestsTab()
        {
            eleUtil.getElement(manageRequeststab);
        }

        public void RenderReceivedRequestsLink()
        {
            eleUtil.getElement(receivedRequestsLink);
        }

        public void GoToReceivedRequests()
        {
            RenderManageRequestsTab();
            RenderReceivedRequestsLink();
            IWebElement managerequeststab = wait.Until(ExpectedConditions.ElementIsVisible(manageRequeststab));
            action.MoveToElement(managerequeststab).Perform();
            Thread.Sleep(2000);
            IWebElement Receivedrequests = wait.Until(ExpectedConditions.ElementToBeClickable(receivedRequestsLink));
            Receivedrequests.Click();
            Thread.Sleep(2000);

        }

        public void RenderAcceptButton()
        {
            eleUtil.getElement(acceptButton);
        }

        public void RenderDeclineButton()
        {
            eleUtil.getElement(declineButton);
        }

        public void RenderCompleteButton()
        {
            eleUtil.getElement(completeButton);

        }

        public void AcceptOrDeclineRequest(string actions)
        {
            RenderAcceptButton();
            RenderDeclineButton();
            Console.WriteLine(actions);
            if (actions == "Accept")
            {
                eleUtil.doClick(acceptButton);
                Thread.Sleep(5000);
            }
            else
            {
                eleUtil.doClick(declineButton);
                Thread.Sleep(3000);
            }


        }
        public void CompleteRequest(string actions)
        {
            RenderCompleteButton();
            eleUtil.doClick(completeButton);
            Thread.Sleep(3000);


        }

        public void RenderSentRequestsLink()
        {
            eleUtil.getElement(sentRequestsLink);
        }

        public void GoToSentRequests()
        {
            RenderManageRequestsTab();
            RenderSentRequestsLink();
            IWebElement managerequeststab = wait.Until(ExpectedConditions.ElementIsVisible(manageRequeststab));
            action.MoveToElement(managerequeststab).Perform();
            Thread.Sleep(2000);
            IWebElement Sentrequests = wait.Until(ExpectedConditions.ElementToBeClickable(sentRequestsLink));
            Sentrequests.Click();
            Thread.Sleep(2000);
        }

        public void RenderWithdrawButton()
        {
            eleUtil.getElement(withdrawButton);
        }

        public void WithdrawRequest(string action)
        {
            RenderWithdrawButton();
            if (action == "Withdraw")
            {
                eleUtil.doClick(withdrawButton);
                Thread.Sleep(3000);
            }
        }

        public string RenderPopUpMessage()
        {

            eleUtil.getElement(messageWindow);
            return eleUtil.getText(messageWindow);

        }
    }

}
