using System.Web.Http;
using SwissTournament.API.App_Start;

namespace SwissTournament.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Create a new Simple Injector container from InjectionConfig file
            InjectorInitializer.Initialize();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
