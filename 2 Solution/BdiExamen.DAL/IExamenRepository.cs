using BdiExamen.Model.Dtos;

namespace BdiExamen.DAL
{
    public interface IExamenRepository
    {
        AgregarResult Agregar(string nombre, string descripcion);
        OperationResult Actualizar(int id, string nombre, string descripcion);
        OperationResult Eliminar(int id);
        ConsultaResult Consultar(int? id, string nombre, string descripcion);
    }
}