using BdiExamen.ApiExamen;
using BdiExamen.Model.Dtos;
using BdiExamen.Model.Enums;
using System.Threading.Tasks;

namespace BdiExamen.WinFormsExamen.Services
{
    // Servicio que encapsula la lógica para interactuar con ApiExamen
    // Implementa la interfaz IExamenService para garantizar que se cumplan los contratos de servicio definidos.
    // Proporciona métodos asincrónicos para consultar, agregar, actualizar y eliminar exámenes.
    public class ExamenService : IExamenService
    {
        private readonly clsExamen _examen;

        public ExamenService()
        {
            _examen = new clsExamen(ModoAcceso.StoredProcedure);
        }

        public async Task<ConsultaResult> ConsultarAsync(int? id = null, string nombre = null, string descripcion = null)
        {
            return await _examen.ConsultarExamen(id, nombre, descripcion);
        }

        public async Task<AgregarResult> AgregarAsync(string nombre, string descripcion)
        {
            return await _examen.AgregarExamen(nombre, descripcion);
        }

        public async Task<OperationResult> ActualizarAsync(int id, string nombre, string descripcion)
        {
            return await _examen.ActualizarExamen(id, nombre, descripcion);
        }

        public async Task<OperationResult> EliminarAsync(int id)
        {
            return await _examen.EliminarExamen(id);
        }
    }
}
