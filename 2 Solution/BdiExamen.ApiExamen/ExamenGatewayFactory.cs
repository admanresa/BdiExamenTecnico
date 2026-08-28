using BdiExamen.ApiExamen.Gateways;
using BdiExamen.Model.Enums;

namespace BdiExamen.ApiExamen
{
    // Factory para crear instancias de IExamenGateway según el ModoAcceso especificado.
    // Permite cambiar fácilmente entre diferentes implementaciones de acceso a datos (Stored Procedure o Web Service) sin modificar el código cliente.
    // Esta clase es parte del patrón de diseño Factory, facilitando la creación de objetos según el contexto de ejecución.
    public static class ExamenGatewayFactory
    {
        public static IExamenGateway Crear(ModoAcceso modo)
        {
            switch (modo)
            {
                case ModoAcceso.StoredProcedure:
                    return new SpExamenGateway();
                case ModoAcceso.WebService:
                    return new WebServiceExamenGateway();
                default:
                    throw new System.ArgumentOutOfRangeException(
                        nameof(modo), modo, "Modo de acceso no soportado.");
            }
        }
    }
}