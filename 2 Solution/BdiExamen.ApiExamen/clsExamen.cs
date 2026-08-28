using BdiExamen.ApiExamen.Gateways;
using BdiExamen.ApiExamen.Validation;
using BdiExamen.Model.Dtos;
using BdiExamen.Model.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BdiExamen.ApiExamen
{
    // Clase pública pedida por el examen. Configurable para usar SP o WebService
    // según el ModoAcceso pasado al constructor.
    public class clsExamen
    {
        private readonly IExamenGateway _gateway;
        private readonly ExamenValidator _validator;

        public clsExamen(ModoAcceso modo)
        {
            _gateway = ExamenGatewayFactory.Crear(modo);
            _validator = new ExamenValidator();
        }

        public async Task<AgregarResult> AgregarExamen(string nombre, string descripcion)
        {
            if (!_validator.EsValido(nombre, descripcion, out List<string> errores))
                return AgregarResult.Fallido(-1, string.Join(" ", errores));

            return await _gateway.AgregarAsync(nombre, descripcion);
        }

        public async Task<OperationResult> ActualizarExamen(int id, string nombre, string descripcion)
        {
            if (!_validator.IdValido(id, out string errorId))
                return OperationResult.Error(-1, errorId);

            if (!_validator.EsValido(nombre, descripcion, out List<string> errores))
                return OperationResult.Error(-1, string.Join(" ", errores));

            return await _gateway.ActualizarAsync(id, nombre, descripcion);
        }

        public async Task<OperationResult> EliminarExamen(int id)
        {
            if (!_validator.IdValido(id, out string errorId))
                return OperationResult.Error(-1, errorId);

            return await _gateway.EliminarAsync(id);
        }

        public async Task<ConsultaResult> ConsultarExamen(int? id, string nombre, string descripcion)
        {
            return await _gateway.ConsultarAsync(id, nombre, descripcion);
        }
    }
}