using System.Web.Http;

namespace BdiExamen.WsApiexamen
{
    // Esta clase representa la aplicación web ASP.NET Web API.
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}