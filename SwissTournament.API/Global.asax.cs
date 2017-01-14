using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using SwissTournament.API.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

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
