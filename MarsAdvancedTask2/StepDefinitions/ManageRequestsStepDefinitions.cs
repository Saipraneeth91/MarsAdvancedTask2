using MarsAdvancedTask2.Helpers;
using MarsAdvancedTask2.Pages.Components;
using MarsAdvancedTask2.Pages.Components.Profile;
using MarsAdvancedTask2.TestModel;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace MarsAdvancedTask2.StepDefinitions
{
    [Binding]
    public class ManageRequestsStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly ManageRequest manageRequest;
        private readonly LoginPage loginMars;
        private readonly ManageRequestAssertion manageRequestAssertion;

        public ManageRequestsStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
            manageRequest = new ManageRequest(driver);
            manageRequestAssertion= new ManageRequestAssertion(driver);
        }
        [Given(@"the user navigates to the Requests Management page")]
        public void GivenTheUserNavigatesToTheRequestsManagementPage()
        {
            manageRequest.GoToSentRequests();
        }

        [When(@"the user attempts to accept a received request from ""([^""]*)""")]
        public void WhenTheUserAttemptsToAcceptAReceivedRequestFrom(string requestfile)
        {
            var requests = JSONHelper.LoadData<List<RequestModel>>(requestfile);

            if (requests == null || requests.Count == 0)
            {
                throw new InvalidOperationException("Request data is null or empty. Ensure the JSON file is properly loaded.");
            }

            foreach (var request in requests)
            {
               
                manageRequest.AcceptOrDeclineRequest(request.Actions);

            }
        }

        [Then(@"the request should be accepted successfully")]
        public void ThenTheRequestShouldBeAcceptedSuccessfully()
        {

            manageRequestAssertion.Acceptreceivedrequestassertion();

        }

        [When(@"the user attempts to decline a received request from ""([^""]*)""")]
        public void WhenTheUserAttemptsToDeclineAReceivedRequestFrom(string requestfile)
        {
            var requests = JSONHelper.LoadData<List<RequestModel>>(requestfile);

            if (requests == null || requests.Count == 0)
            {
                throw new InvalidOperationException("Request data is null or empty. Ensure the JSON file is properly loaded.");
            }

            foreach (var request in requests)
            {

                manageRequest.AcceptOrDeclineRequest(request.Actions);



            }

        }

        [Then(@"the request should be declined successfully")]
        public void ThenTheRequestShouldBeDeclinedSuccessfully()
        {
            manageRequestAssertion.Declinereceivedrequestassertion();
        }

        [When(@"the user attempts to withdraw a sent request from ""([^""]*)""")]
        public void WhenTheUserAttemptsToWithdrawASentRequestFrom(string requestFile)
        {
            var requests = JSONHelper.LoadData<List<RequestModel>>(requestFile);

            if (requests == null || requests.Count == 0)
            {
                throw new InvalidOperationException("Request data is null or empty. Ensure the JSON file is properly loaded.");
            }

            foreach (var request in requests)
            {
                manageRequest.WithdrawRequest(request.Actions);
               
                Thread.Sleep(2000);

            }
        }

        [Then(@"the user should be able to withdraw the sent request successfully")]
        public void ThenTheUserShouldBeAbleToWithdrawTheSentRequestSuccessfully()
        {
            manageRequestAssertion.Withdrawsentrequestassertion();
        }

        [When(@"the user attempts to complete an accepted request from ""([^""]*)""")]
        public void WhenTheUserAttemptsToCompleteAnAcceptedRequestFrom(string requestfile)
        {
            var requests = JSONHelper.LoadData<List<RequestModel>>(requestfile);

            if (requests == null || requests.Count == 0)
            {
                throw new InvalidOperationException("Request data is null or empty. Ensure the JSON file is properly loaded.");
            }

            foreach (var request in requests)
            {

                manageRequest.CompleteRequest(request.Actions);



            }
        }

        [Then(@"the user should be able to complete the received request successfully")]
        public void ThenTheUserShouldBeAbleToCompleteTheReceivedRequestSuccessfully()
        {

            manageRequestAssertion.Completerequestassertion();

        }
        [Given(@"the user navigates to the Received requests tab")]
        public void GivenTheUserNavigatesToTheReceivedRequestsTab()
        {
            manageRequest.GoToReceivedRequests();
        }

        [Given(@"the user navigates to the Sent requests tab")]
        public void GivenTheUserNavigatesToTheSentRequestsTab()
        {
            manageRequest.GoToSentRequests();
        }

    }
}
