using BdiExamen.Model.Dtos;

namespace BdiExamen.MvcExamen.Services
{
    public interface IExamenApiClient
    {
        ConsultaResult Consultar(int? id = null, string nombre = null, string descripcion = null);
        AgregarResult Agregar(string nombre, string descripcion);
        OperationResult Actualizar(int id, string nombre, string descripcion);
        OperationResult Eliminar(int id);
    }
}
