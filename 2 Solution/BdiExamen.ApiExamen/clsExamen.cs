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
        // Constructor que inicializa el gateway y el validador según el modo de acceso especificado.
        public clsExamen(ModoAcceso modo)
        {
            _gateway = ExamenGatewayFactory.Crear(modo);
            _validator = new ExamenValidator();
        }
        // Todos los métodos públicos de esta clase son asíncronos y devuelven tareas que se pueden esperar para mejorar el rendimiento.

        // Método público para agregar un examen. Valida los parámetros antes de llamar al gateway.
        // Devuelve un objeto AgregarResult que indica si la operación fue exitosa o fallida.
        public async Task<AgregarResult> AgregarExamen(string nombre, string descripcion)
        {
            if (!_validator.EsValido(nombre, descripcion, out List<string> errores))
                return AgregarResult.Fallido(-1, string.Join(" ", errores));

            return await _gateway.AgregarAsync(nombre, descripcion);
        }
        // Método público para actualizar un examen existente. Valida el id y los parámetros antes de llamar al gateway.
        // Devuelve un objeto OperationResult que indica si la operación fue exitosa o fallida.
        public async Task<OperationResult> ActualizarExamen(int id, string nombre, string descripcion)
        {
            if (!_validator.IdValido(id, out string errorId))
                return OperationResult.Error(-1, errorId);

            if (!_validator.EsValido(nombre, descripcion, out List<string> errores))
                return OperationResult.Error(-1, string.Join(" ", errores));

            return await _gateway.ActualizarAsync(id, nombre, descripcion);
        }
        // Método público para eliminar un examen existente. Valida el id antes de llamar al gateway.
        // Devuelve un objeto OperationResult que indica si la operación fue exitosa o fallida.
        public async Task<OperationResult> EliminarExamen(int id)
        {
            if (!_validator.IdValido(id, out string errorId))
                return OperationResult.Error(-1, errorId);

            return await _gateway.EliminarAsync(id);
        }
        // Método público para consultar exámenes existentes. Valida los parámetros antes de llamar al gateway.
        // Devuelve un objeto ConsultaResult que contiene la lista de exámenes encontrados o un mensaje de error.
        public async Task<ConsultaResult> ConsultarExamen(int? id, string nombre, string descripcion)
        {
            return await _gateway.ConsultarAsync(id, nombre, descripcion);
        }
    }
}