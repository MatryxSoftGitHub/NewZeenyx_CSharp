using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace NewZeenyx_CSharp.PageObjects
{
    public class ZeenyxHomePage
    {
        IWebDriver driver;
        [Obsolete]
        public ZeenyxHomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }



        //Contact link
        [FindsBy(How = How.XPath, Using = "//ul[@class='navbar-nav ml-auto']//a[contains(text(),'Contact')]")]
        public IWebElement Lnk_Contact { get; set; }

        //FirstName textfield
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='First Name']")]
        public IWebElement Txt_FirstName { get; set; }

        //LastName textfield
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Last Name']")]
        public IWebElement Txt_LastName { get; set; }

        //Email textfield
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Email Address']")]
        public IWebElement Txt_Email { get; set; }

        //Subject dropdown
        [FindsBy(How = How.XPath, Using = "//select[@name='input_3']")]
        public IWebElement Dropdown_Subject { get; set; }

        //Message textfield
        [FindsBy(How = How.XPath, Using = "//textarea[@placeholder='Message']")]
        public IWebElement Txt_Message { get; set; }

        //Raise a request for AscentialTest automation tool information.
        public void ZeenyxWebsite(string sFirstname, string sLastname, string sEmail, string sSubject, string sMessage)
        {
            Lnk_Contact.Click();

            Txt_FirstName.SendKeys(sFirstname);
            //Thread.Sleep(2000);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            Txt_LastName.SendKeys(sLastname);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            Txt_Email.SendKeys(sEmail);

            Dropdown_Subject.Click();
            SelectElement oSelect = new SelectElement(Dropdown_Subject);
            oSelect.SelectByText(sSubject);

            Txt_Message.SendKeys(sMessage);

            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

        }

    }
}
