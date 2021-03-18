using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NewZeenyx_CSharp.Utilities;
using NUnit.Framework;
using System.IO;

namespace NewZeenyx_CSharp.Testcases
{
    [TestFixture]
    public class ZeenyxInfoRequest_Testcase
    {

        //static string sLocationOfReport = @"D:\Selenium\seleniumPJTs\NewZeenyx_CSharp\Reports\ZeenyxReport.html";
        //static ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(sLocationOfReport);
        //static ExtentReports extent = new ExtentReports();

        public ExtentTest test;
        public ExtentReports extent;
        public object SmtpConnectType { get; private set; }
        public static SendCompletedEventHandler OnMailSent { get; private set; }

        [SetUp]
        public void SetUp()
        {
            
           extent = new ExtentReports();

            string startupPath = Environment.CurrentDirectory;
            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(startupPath).Parent.FullName;
            Console.WriteLine("The current directory is 12345", projectDirectory);
           string reportPath = startupPath  + "/ZeenyxReport.html";

           var htmlReporter = new ExtentHtmlReporter(reportPath);



            
           extent.AttachReporter(htmlReporter);
            
        }

        public static string Capture(IWebDriver driver, string ScreenshotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            //string sPath = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            //string sCurrentTime = DateTime.Now.ToShortTimeString();//To get current date and time to the snapshot name
            //string sFinalpath = sPath.Substring(0, sPath.LastIndexOf("bin")) + "Scrrenshots\\" + ScreenshotName + sCurrentTime + ".png";
            //string sLocalpath = new Uri(sFinalpath).LocalPath;
            //screenshot.SaveAsFile(sLocalpath, ScreenshotImageFormat.Png);
            string sSnaPath = @"D:\Selenium\seleniumPJTs\NewZeenyx_CSharp\Screenshots\" + ScreenshotName + ".png";
            screenshot.SaveAsFile(sSnaPath, ScreenshotImageFormat.Png);

            return sSnaPath;
            // return sLocalpath;
        }

        [Test]
        public void EnvVariables()
        {
            //Use Reasl Values
            string sHostName = Dns.GetHostName();
            OperatingSystem os = Environment.OSVersion;
            extent.AddSystemInfo("Operating System", os.ToString());
            extent.AddSystemInfo("Host Name", sHostName);

            //Hard Coding
            extent.AddSystemInfo("Author", "Vidya");
            extent.AddSystemInfo("Application Type", "Zeenyx Website");
            extent.AddSystemInfo("Browser", "Google Chrome");
        }


        [Test]
        public void Zeenyx()
        {
            ExcelDataProvider.PopulateInCollection(@"D:\Selenium\seleniumPJTs\NewZeenyx_CSharp\TestData\ZeenyxTestData.xlsx");


            IWebDriver driver = new FirefoxDriver();
            var LaunchInfo = extent.CreateTest("Zeenyx Web Site", "Request information about Ascential Test Tool");

            LaunchInfo.Log(Status.Info, "Browser is launched.");
            

            //Navigate to naukri website
            driver.Url = "https://www.zeenyx.com/";
            


            Console.WriteLine("Zeenyx Website is displayed!");

            LaunchInfo.Log(Status.Info, " Zeenyx Website is displayed!");

            driver.Manage().Window.Maximize();
            



            //Contact Link
            driver.FindElement(By.XPath("//ul[@class='navbar-nav ml-auto']//a[contains(text(),'Contact')]")).Click();
            Thread.Sleep(2000);

            //Firstname text field
            driver.FindElement(By.XPath("//input[@placeholder='First Name']")).SendKeys(ExcelDataProvider.ReadData(1, "FirstName"));

            //Lastname text field
            driver.FindElement(By.XPath("//input[@placeholder='Last Name']")).SendKeys(ExcelDataProvider.ReadData(1, "LastName"));


            //Email text field
            driver.FindElement(By.XPath("//input[@placeholder='Email Address']")).SendKeys(ExcelDataProvider.ReadData(1, "EmailAddress"));


            //Drop down select Subject
            SelectElement DropdownSelect = new SelectElement(driver.FindElement(By.XPath("//select[@name='input_3']")));

            DropdownSelect.SelectByText(ExcelDataProvider.ReadData(1, "RequestSubject"));

            //Request Textfield
            driver.FindElement(By.XPath("//textarea[@placeholder='Message']")).SendKeys(ExcelDataProvider.ReadData(1, "Message"));

            //Screenshot
            var test = extent.CreateTest("Screenshot", " Capture Screenshot ");

            //test.Log(Status.Info, "First Setup of Warning test methods.");
            //test.Log(Status.Info, " The Test Case is Executed.");

            Thread.Sleep(2000);


            //Screenshot
            string sScreenshotPath = Capture(driver, "ZeenyxSnap3");
            test.AddScreenCaptureFromPath(sScreenshotPath);


            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            driver.Close();

            driver.Quit();

        }
        



        

//             [TearDown]
//             public void CleansUp()
//             {
//                 extent.Flush();

//             }



        }
}

