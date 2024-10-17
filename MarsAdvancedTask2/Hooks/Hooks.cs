using BoDi;
using MarsAdvancedTask2.Helpers;
using MarsAdvancedTask2.Models;
using MarsAdvancedTask2.Pages.Components;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RazorEngine;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.Extensions;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
namespace MarsAdvancedTask2.Hooks
{
    [Binding]
    public sealed class Hooks
    {

        private static IWebDriver driver;
        private readonly IObjectContainer _container;
        private readonly ScenarioContext _scenarioContext;
        private static ExtentReports extent;
        private static ExtentTest feature;
        private ExtentTest scenario;

        public Hooks(IObjectContainer container, ScenarioContext scenarioContext)
        {
            _container = container;
            _scenarioContext = scenarioContext;
        }

        // This method runs once before the entire test run starts
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("Initializing Extent Report...");
            string projectRootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
            string reportPath = Path.Combine(projectRootPath, "Reports", "ExtentReport.html");
            Directory.CreateDirectory(Path.GetDirectoryName(reportPath));
            var reporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(reporter);
            Console.WriteLine("Extent Report Initialized.");
            // Initialize WebDriver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            ClearEducation();
            ClearCertification();
        }
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            feature = extent.CreateTest(featureContext.FeatureInfo.Title);
        }
        [BeforeScenario]
        public void BeforeScenario()
        {
            scenario = feature.CreateNode<AventStack.ExtentReports.Gherkin.Model.Scenario>(_scenarioContext.ScenarioInfo.Title);
            if (!_container.IsRegistered<IWebDriver>())
            {
                _container.RegisterInstanceAs(driver);
            }
        }
        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.TestError != null)
            {
                scenario.Fail(_scenarioContext.TestError.Message);
                string fileName = "Screenshot_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
                CaptureScreenshot(fileName);

            }
            else
            {
                scenario.Pass("Scenario Passed");
            }
            driver?.Quit();
            driver = null; 
        }
        [AfterScenario(Order = 1)]
        public void ClearTestSpecificLanguagesAfterScenario()
        {
            if (driver != null)
            {
                var certificate = new Certificationcomponent(driver);
                var education = new EducationComponent(driver);
                if (_scenarioContext.TryGetValue("certification", out CertificationDataModel certificationDetails))
                {
                    certificate.Deletecertification(certificationDetails.Certificate);
                }
                if (_scenarioContext.TryGetValue("education", out EducationDataModel educationDetails))
                {
                    education.Deleteeducation(educationDetails.Title);
                }
            }
        }
        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("Flushing Extent Report...");
            extent?.Flush();
            Console.WriteLine("Extent Report Flushed.");
            driver?.Quit();
            driver = null; 
        }
        public static void ClearCertification()
        {
            if (driver != null)
            {
                var certificate = new Certificationcomponent(driver);
                certificate.ClearCertification();
            }
        }
        public static void ClearEducation()
        {
            if (driver != null)
            {
                var login = new LoginPage(driver);
                login.LoginActions("saipraneethg.91@gmail.com", "Praneeth@1");
                var education = new EducationComponent(driver);
                education.ClearEducation();
            }
        }

        private void CaptureScreenshot(string fileName)
        {
            if (driver != null)
            {
                try
                {
                    string projectRootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));

                    string screenshotsFolder = Path.Combine(projectRootPath, "Screenshots");
                    if (!Directory.Exists(screenshotsFolder))
                    {
                        Directory.CreateDirectory(screenshotsFolder);
                    }
                    string filePath = Path.Combine(screenshotsFolder, fileName);
                    ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
                    Screenshot screenshot = screenshotDriver.GetScreenshot();
                    screenshot.SaveAsFile(filePath);

                    Console.WriteLine($"Screenshot saved at: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to capture screenshot: " + ex.Message);
                }
            }
        }
    }
}

