using BdiExamen.Model.Dtos;
using System.Threading.Tasks;

namespace BdiExamen.WinFormsExamen.Services
{
    public interface IExamenService
    {
        Task<ConsultaResult> ConsultarAsync(int? id = null, string nombre = null, string descripcion = null);
        Task<AgregarResult> AgregarAsync(string nombre, string descripcion);
        Task<OperationResult> ActualizarAsync(int id, string nombre, string descripcion);
        Task<OperationResult> EliminarAsync(int id);
    }
}
