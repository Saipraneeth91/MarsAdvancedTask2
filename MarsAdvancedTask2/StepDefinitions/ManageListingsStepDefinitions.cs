
using MarsAdvancedTask2.Helpers;
using MarsAdvancedTask2.Models;
using MarsAdvancedTask2.Pages.Components;
using MarsAdvancedTask2.Pages.Components.Profile;
using MarsAdvancedTask2.TestModel;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace MarsAdvancedTask2.StepDefinitions
{
    [Binding]
    public class ManageListingsStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly LoginPage loginMars;
        private readonly ManagelistingComponent managelisting;
        private List<ListingData> listings;
        private List<EditListingData> editlistings;
        private List<RequestData> requestData;
        private SearchskillModel searchskilldata;
        private Sendrequest sendrequest;
        private ManageListing managelistingassertion;
        public ManageListingsStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
            loginMars = new LoginPage(driver);
            managelisting = new ManagelistingComponent(driver);
            sendrequest = new Sendrequest(driver);
            managelistingassertion = new ManageListing(driver);


        }
        [Given(@"the user has successfully logged into the MARS application")]
        public void GivenTheUserHasSuccessfullyLoggedIntoTheMARSApplication()
        {
            loginMars.LoginActions("saipraneethg.91@gmail.com", "Praneeth@1");

        }

        [When(@"the user attempts to add a listing from ""([^""]*)""")]
        public void WhenTheUserAttemptsToAddAListingFrom(string listingFile)
        {
            listings = JSONHelper.LoadData<List<ListingData>>(listingFile);

            if (listings == null || listings.Count == 0)
            {
                throw new InvalidOperationException("Listing data is null or empty. Ensure the JSON file is properly loaded.");
            }

            // Add the first listing from the file as an example
            ListingData listing = listings.FirstOrDefault(l => l.ListingID != null);

            if (listing == null)
            {
                throw new InvalidOperationException("No valid listing found in the JSON file.");
            }

            // Use the manage listing component to add the listing
            managelisting.AddListing(listing);
        }

    [Then(@"the listing should be added successfully")]
        public void ThenTheListingShouldBeAddedSuccessfully()
        {
            var listing = listings.FirstOrDefault(l => l.ListingID != null);
            managelistingassertion.AssertAddListing(listing.Category, listing.Title, listing.Description);


        }

        [When(@"the user attempts to update a listing from ""([^""]*)""")]
        public void WhenTheUserAttemptsToUpdateAListingFrom(string listingFile)
        {
            listings = JSONHelper.LoadData<List<ListingData>>(listingFile);
            editlistings = JSONHelper.LoadData<List<EditListingData>>(listingFile);

            if (listings == null || listings.Count == 0)
            {
                throw new InvalidOperationException("Listing data is null or empty. Ensure the JSON file is properly loaded.");
            }

            // Add the first listing from the file as an example
            ListingData listing = listings.FirstOrDefault(l => l.ListingID != null);
            EditListingData editListing = editlistings.FirstOrDefault(l => l.ListingID != null);
            if (listing == null || editListing == null)
            {
                throw new InvalidOperationException("No valid listing or edit listing found in the JSON file.");
            }
            // Use the manage listing component to add the listing
            managelisting.AddListing(listing);
            managelisting.UpdateListing(editListing);
        }
        [Then(@"the listing should be updated successfully")]
        public void ThenTheListingShouldBeUpdatedSuccessfully()
        {
            var editListing = editlistings.FirstOrDefault(l => l.ListingID != null);
            managelistingassertion.AssertUpdateListing(editListing.NewCategory, editListing.NewTitle, editListing.NewDescription);

        }

        [When(@"the user attempts to view a listing from ""([^""]*)""")]
        public void WhenTheUserAttemptsToViewAListingFrom(string listingFile)
        {
            listings = JSONHelper.LoadData<List<ListingData>>(listingFile);

            if (listings == null || listings.Count == 0)
            {
                throw new InvalidOperationException("Listing data is null or empty. Ensure the JSON file is properly loaded.");
            }

            // Add the first listing from the file as an example
            ListingData listing = listings.FirstOrDefault(l => l.ListingID != null);

            if (listing == null)
            {
                throw new InvalidOperationException("No valid listing found in the JSON file.");
            }

            // Use the manage listing component to add the listing
            managelisting.AddListing(listing);
            managelisting.ViewListing();
            
        }

        [Then(@"the listing should be viewed successfully")]
        public void ThenTheListingShouldBeViewedSuccessfully()
        {

            var listing = listings.FirstOrDefault(l => l.ListingID != null);
            managelistingassertion.AssertViewListing(listing.Title);

        }

        [When(@"the user attempts to delete a listing from ""([^""]*)""")]
        public void WhenTheUserAttemptsToDeleteAListingFrom(string listingFile)
        {
            listings = JSONHelper.LoadData<List<ListingData>>(listingFile);

            if (listings == null || listings.Count == 0)
            {
                throw new InvalidOperationException("Listing data is null or empty. Ensure the JSON file is properly loaded.");
            }

            // Add the first listing from the file as an example
            ListingData listing = listings.FirstOrDefault(l => l.ListingID != null);

            if (listing == null)
            {
                throw new InvalidOperationException("No valid listing found in the JSON file.");
            }

            managelisting.AddListing(listing);
            managelisting.DeleteListing();

        }

        [Then(@"the listing should be deleted successfully")]
        public void ThenTheListingShouldBeDeletedSuccessfully()
        {
            var expectedListing = JSONHelper.LoadData<List<ListingData>>("AddListing.json").First();

            managelistingassertion.AssertDeleteListing(expectedListing);
 


        }

        [When(@"the user attempts to enable a toggle in a listing from ""([^""]*)""")]
        public void WhenTheUserAttemptsToEnableAToggleInAListingFrom(string listingFile)
        {
            listings = JSONHelper.LoadData<List<ListingData>>(listingFile);

            if (listings == null || listings.Count == 0)
            {
                throw new InvalidOperationException("Listing data is null or empty. Ensure the JSON file is properly loaded.");
            }

            // Add the first listing from the file as an example
            ListingData listing = listings.FirstOrDefault(l => l.ListingID != null);

            if (listing == null)
            {
                throw new InvalidOperationException("No valid listing found in the JSON file.");
            }

            // Use the manage listing component to add the listing
            managelisting.AddListing(listing);
            managelisting.ClickToggle();
        }

        [Then(@"the toggle should be enabled successfully")]
        public void ThenTheToggleShouldBeEnabledSuccessfully()
        {
            managelistingassertion.AssertEnableToggle();

           
        }

        [When(@"the user attempts to disable a toggle in a listing from ""([^""]*)""")]
        public void WhenTheUserAttemptsToDisableAToggleInAListingFrom(string listingFile)
        {
            listings = JSONHelper.LoadData<List<ListingData>>(listingFile);

            if (listings == null || listings.Count == 0)
            {
                throw new InvalidOperationException("Listing data is null or empty. Ensure the JSON file is properly loaded.");
            }

            // Add the first listing from the file as an example
            ListingData listing = listings.FirstOrDefault(l => l.ListingID != null);

            if (listing == null)
            {
                throw new InvalidOperationException("No valid listing found in the JSON file.");
            }

            // Use the manage listing component to add the listing
            managelisting.AddListing(listing);
            managelisting.ClickToggle();
        }

        [Then(@"the toggle should be disabled successfully")]
        public void ThenTheToggleShouldBeDisabledSuccessfully()
        {

            managelistingassertion.AssertDisableToggle();
        }
        [When(@"the user attempts to send a request to a listing from ""([^""]*)""")]
        public void WhenTheUserAttemptsToSendARequestToAListingFrom(string listingFile)
        {

            var skillcategory = JSONHelper.LoadData<List<SearchskillModel>>(listingFile);

            if (skillcategory == null || skillcategory.Count == 0)
            {
                throw new InvalidOperationException("Request data is null or empty. Ensure the JSON file is properly loaded.");
            }

            foreach (var skill in skillcategory)
            {
                sendrequest.SearchskillbyCategories(skill);

            }
        }
        [Then(@"the request should be sent successfully")]
        public void ThenTheRequestShouldBeSentSuccessfully()
        {

            managelistingassertion.AssertSendRequest();



        }


    }

}
