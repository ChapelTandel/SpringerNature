using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using SpringerNature_UI_Test.Steps;
using TechTalk.SpecFlow;

namespace SpringerNature_UI_Test
{
    [Binding]
    public class WebdriverHooks
    {
        [BeforeTestRun]
        public static void LaunchBrowser()
        {
            Context.Driver = new FirefoxDriver();
            Context.Driver.Manage().Window.Maximize();
        }


        [BeforeFeature]
        [AfterFeature]
        public static void ClearBrowserCookies()
        {
            Context.Driver.Manage().Cookies.DeleteAllCookies();
        }

        [AfterTestRun]
        public static void CloseBrowser()
        {
            Context.Driver.Close();
            Context.Driver.Dispose();
        }
    }
}
