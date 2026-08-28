using BdiExamen.Model.Dtos;
using System.Threading.Tasks;

namespace BdiExamen.ApiExamen.Gateways
{
    public interface IExamenGateway
    {
        Task<AgregarResult> AgregarAsync(string nombre, string descripcion);
        Task<OperationResult> ActualizarAsync(int id, string nombre, string descripcion);
        Task<OperationResult> EliminarAsync(int id);
        Task<ConsultaResult> ConsultarAsync(int? id, string nombre, string descripcion);
    }
}