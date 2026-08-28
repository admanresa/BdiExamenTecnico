using System.Web.Http;

namespace BdiExamen.WsApiexamen
{
    // Esta clase contiene la configuración de rutas y formateadores para la aplicación ASP.NET Web API.
    // La configuración de rutas define cómo se mapean las URL a los controladores y acciones correspondientes.
    public static class WebApiConfig
    {
        // Este método se llama al iniciar la aplicación y configura las rutas y formateadores de la API.
        public static void Register(HttpConfiguration config)
        {
            // Configura las rutas de la API utilizando atributos en los controladores y acciones.
            config.MapHttpAttributeRoutes();

            // Configura la ruta predeterminada para la API, donde las solicitudes se dirigen a los controladores y acciones correspondientes.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );

            // Configura el formateador JSON para ignorar los bucles de referencia al serializar objetos.
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}