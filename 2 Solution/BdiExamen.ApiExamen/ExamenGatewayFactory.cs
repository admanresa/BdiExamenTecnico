using BdiExamen.ApiExamen.Gateways;
using BdiExamen.Model.Enums;

namespace BdiExamen.ApiExamen
{
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