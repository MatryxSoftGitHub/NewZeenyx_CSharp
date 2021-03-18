using NewZeenyx_CSharp.Interfaces;
using System;
using NewZeenyx_CSharp.Settings;
using System.Configuration;


namespace NewZeenyx_CSharp.Configurations
{
    public class AppConfigReader : IConfig
    {
        public BrowserType GetBrowser()
        {
            string browser = ConfigurationManager.AppSettings.Get(AppConfigKeys.fBrowser);
            return (BrowserType)Enum.Parse(typeof(BrowserType), browser);

        }

        public string GetZeenyxURL()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.ZeenyxURL);
        }


    }
}
