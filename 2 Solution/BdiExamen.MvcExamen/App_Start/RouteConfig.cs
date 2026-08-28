using System.Web.Mvc;
using System.Web.Routing;

namespace BdiExamen.MvcExamen
{
    // Esta clase contiene la configuración de rutas para la aplicación ASP.NET MVC.
    // La configuración de rutas define cómo se mapean las URL a los controladores y acciones correspondientes.
    // La ruta predeterminada se establece para que las solicitudes sin un controlador o acción específicos se dirijan al controlador "Examen" y a la acción "Index".
    // La ruta también permite un parámetro opcional "id" que puede ser utilizado por las acciones del controlador.
    // La ruta predeterminada se define con el nombre "Default" y utiliza la plantilla de URL "{controller}/{action}/{id}".
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Examen", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
