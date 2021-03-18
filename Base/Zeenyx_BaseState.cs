using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using NewZeenyx_CSharp.Utilities;

using System.IO;

namespace NewZeenyx_CSharp.Base
{ 

    [TestFixture]
    public class Zeenyx_BaseState
{
        public IWebDriver driver;
        public ExtentReports extent;
        public ExtentTest test;



        [SetUp]
        public void OnStart()
        {
            test = extent.CreateTest("Zeenyx Website").Info("Testcase is started");

            test.Log(Status.Info, "Browser is launched");

            //Navigate to Zeenyx website
            BrowserFactory.OnStart(driver, "Firefox", "https://www.zeenyx.com/");

            Console.WriteLine("Zeenyx Website home page is displayed!");

            test.Log(Status.Info, "Zeenyx home page is displayed");

            // driver.Manage().Window.Maximize();

        }

        [OneTimeSetUp]

        public void SetUp()
        {
            Console.WriteLine("Enter in to Setup Function");
            try
            {
                extent = new ExtentReports();
                //To obtain the current solution path/ project path

               //  string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                // string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
                 //string projectPath = new Uri(actualPath).LocalPath;


                //Append the html report file to current project path
                string startupPath = Environment.CurrentDirectory;
                // This will get the current PROJECT directory
                string projectDirectory = Directory.GetParent(startupPath).Parent.FullName;
                Debug.WriteLine("The current directory is {0}", startupPath);

                Console.WriteLine("The current directory is {0}", startupPath);
                string reportPath = projectDirectory + "Reports/ZeenyxReport1.html";

                var htmlReporter = new ExtentHtmlReporter(reportPath);

                //var htmlReporter = new ExtentHtmlReporter(@"D:\Selenium\seleniumPJTs\ZeenyxWebsite_CSharp\Reports\ZeenyxReport.html");
                extent.AttachReporter(htmlReporter);
                extent.AddSystemInfo("Operating System", "Windows 10 Pro");
                extent.AddSystemInfo("HostName", "MSDES13");
                extent.AddSystemInfo("Environment", "QA");
                extent.AddSystemInfo("Author", "Vidya");
                extent.AddSystemInfo("Browser", "Firefox");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



        public static string Capture(IWebDriver driver, string ScreenshotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string sPath = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string sCurrentTime = DateTime.Now.ToShortTimeString();//To get current date and time to the snapshot name
            string sFinalpath = sPath.Substring(0, sPath.LastIndexOf("bin")) + "Scrrenshots\\" + ScreenshotName + sCurrentTime + ".png";
            string sLocalpath = new Uri(sFinalpath).LocalPath;
            screenshot.SaveAsFile(sLocalpath, ScreenshotImageFormat.Png);
           // string sSnaPath = @"D:\Selenium\seleniumPJTs\ZeenyxWebsite_CSharp\Screenshots\" + ScreenshotName + ".png";
           // screenshot.SaveAsFile(sSnaPath, ScreenshotImageFormat.Png);

            //return sSnaPath;
             return sLocalpath;
        }


        public void SendAnEmailNow(string sSubject, string sContentBody)
        {
            //Sender's email, Sender's password, To/Receiver's email, Subject, Body, cc, Attachment
            MailMessage mail = new MailMessage();
            string sFromMail = "regression@matryxsoft.com";
            string sPassword = "Matryx@2020";
            string sToMail = "supritha@matryxsoft.com";
           

            mail.From = new MailAddress(sFromMail);
            mail.To.Add(sToMail);
            mail.Subject = sSubject;
            mail.Body = sContentBody;
            mail.IsBodyHtml = true;
            ///To obtain the current solution path/project path

            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;

            //Append the html report file to current project path
            string reportPath = projectPath + "Reports\\ZeenyxReport.html";
            mail.Attachments.Add(new Attachment(reportPath));
            //mail.Attachments.Add(new Attachment(@"D:\Selenium\seleniumPJTs\ZeenyxWebsite_CSharp\Reports\ZeenyxReport.html"));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(sFromMail, sPassword),
                EnableSsl = true
            };
            smtp.Send(mail);

        }



        [OneTimeTearDown]
        public void CleansUp()
        {

            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = "stackTrace" + TestContext.CurrentContext.Result.StackTrace + "stackTrace";
                var errorMessage = TestContext.CurrentContext.Result.Message;
                Status logstatus;
                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        //string screenShotPath = Helper.Capture1(driver, TestContext.CurrentContext.Test.Name);
                        test.Log(logstatus, "Test ended with " + logstatus + " – " + errorMessage);
                        test.Log(logstatus, "Snapshot below: " + test.AddScreenCaptureFromPath(Capture(driver, "SSS")));
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                    default:
                        logstatus = Status.Pass;
                        test.Log(logstatus, "Test ended with " + logstatus);
                        test.Log(logstatus, "Snapshot below: " + test.AddScreenCaptureFromPath(Capture(driver, "SSS")));
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw (e);
            }

        }

        [TearDown]
        public void OnFinish()
        {
            extent.Flush();
            driver.Quit();
            test.Log(Status.Info, "Browser is closed");
            test.Log(Status.Pass, "Testcase is passed");

            try
            {
                test.Pass("details").AddScreenCaptureFromPath("Snapshot.png");
                test.Fail("details").AddScreenCaptureFromPath("Snapshot.png");
                //test.AddScreenCaptureFromPath();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



        }



    }
}


//..


