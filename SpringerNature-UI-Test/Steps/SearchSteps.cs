using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace SpringerNature_UI_Test.Steps
{
    [Binding]
    public class SearchSteps
    {
        [Given(@"I am on Springer Link homepage")]
        public void GivenIAmOnSpringerLinkHomepage()
        {
            var hompePage = new HomePage(Context.Driver);
            hompePage.Open();
        }

        [When(@"I click the search button")]
        public void WhenIClickTheSearchButton()
        {
            var hompePage = new HomePage(Context.Driver);
            hompePage.Search();
        }

        [Then(@"I see list of search results")]
        public void ThenISeeListOfSearchResults()
        {
            var searchResultPage = new SearchResultPage(Context.Driver);
            searchResultPage.ListOfSearchResultItem.Count.Should().BeGreaterThan(1);
        }

        [When(@"I search for the test ""(.*)""")]
        public void WhenISearchForTheTest(string p0)
        {
            var hompePage = new HomePage(Context.Driver);
            hompePage.Search(p0);
        }

        [Then(@"I see ""(.*)"" in the search result")]
        public void ThenISeeInTheSearchResult(string p0)
        {
            var searchResultPage = new SearchResultPage(Context.Driver);
            Assert.IsTrue(searchResultPage.ListOfSearchResultItem.Any(x => x.Contains(p0)));
        }

        [Then(@"I see ""(.*)"" seach result retrun")]
        public void ThenISeeSeachResultRetrun(int p0)
        {
            var searchResultPage = new SearchResultPage(Context.Driver);
            searchResultPage.NumberOfResultItems.Should().Contain(p0.ToString());

            Console.WriteLine(searchResultPage.NumberOfResultItems);
        }
    }

    public class SearchResultPage : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = ".title")]
        private IList<IWebElement> _listOfTitleLabels;

        [FindsBy(How = How.CssSelector, Using = ".number-of-search-results-and-search-terms>strong")]
        private IWebElement _resultLable;

        public SearchResultPage(IWebDriver driver) : base(driver)
        {
        }

        public List<string> ListOfSearchResultItem => _listOfTitleLabels.Select(x => x.Text).ToList();

        public string NumberOfResultItems => _resultLable.Text;
    }

    public class Context
    {
        public static IWebDriver Driver { get; set; }
    }

    public class HomePage : BasePage
    {
        [FindsBy(How = How.Id, Using = "search")]
        private IWebElement _searchButton;

        [FindsBy(How = How.Id, Using = "query")]
        private IWebElement _searchInput;

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public HomePage Open()
        {
            Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["baseUrl"]);

            Wait5Seconds.Until(d => _searchInput.Displayed);

            return this;
        }

        public void Search(string text = "")
        {
            _searchInput.SendKeys(text);
            _searchButton.Click();
        }
    }

    public class BasePage
    {
        protected BasePage(IWebDriver driver)
        {
            Driver = driver;

            Wait5Seconds = new WebDriverWait(driver, TimeSpan.FromSeconds(5));


            PageFactory.InitElements(driver, this);
        }

        public static WebDriverWait Wait5Seconds { get; set; }

        public IWebDriver Driver { get; set; }
    }
}