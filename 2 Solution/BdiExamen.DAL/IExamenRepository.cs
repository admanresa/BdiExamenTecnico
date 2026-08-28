using BdiExamen.Model.Dtos;

namespace BdiExamen.DAL
{
    // Interface que define los métodos para interactuar con los datos de los exámenes en la base de datos.
    // Proporciona métodos para agregar, actualizar, eliminar y consultar exámenes.
    // Cada método devuelve un resultado que indica si la operación fue exitosa o no, junto con información adicional.
    // Esta interfaz permite la implementación de diferentes estrategias de acceso a datos (por ejemplo, mediante procedimientos almacenados o servicios web) sin acoplar el código a una implementación específica.
    public interface IExamenRepository
    {
        AgregarResult Agregar(string nombre, string descripcion);
        OperationResult Actualizar(int id, string nombre, string descripcion);
        OperationResult Eliminar(int id);
        ConsultaResult Consultar(int? id, string nombre, string descripcion);
    }
}