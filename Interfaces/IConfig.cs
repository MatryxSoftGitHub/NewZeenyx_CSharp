using NewZeenyx_CSharp.Configurations;

namespace NewZeenyx_CSharp.Interfaces
{
    public interface IConfig
    {
        BrowserType GetBrowser();

        string GetZeenyxURL();

    }
}
