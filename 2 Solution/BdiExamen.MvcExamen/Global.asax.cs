using System;

namespace BdiExamen.MvcExamen
{
    public partial class Global : System.Web.HttpApplication
    {
        // Aquí se registra la configuración de rutas para la aplicación ASP.NET MVC.
        protected void Application_Start()
        {
            // Llama al método RegisterRoutes de la clase RouteConfig para configurar las rutas de la aplicación.
            RouteConfig.RegisterRoutes(System.Web.Routing.RouteTable.Routes);
        }
    }
}
