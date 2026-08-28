using System.Web.Http;
using BdiExamen.DAL;
using BdiExamen.Model.Dtos;
using BdiExamen.WsApiexamen.Models;

namespace BdiExamen.WsApiexamen.Controllers
{
    // Controlador de la API para manejar operaciones relacionadas con los exámenes.
    [RoutePrefix("api/examen")]
    public class ExamenController : ApiController
    {
        // Repositorio para acceder a los datos de los exámenes.
        private readonly IExamenRepository _repository = new ExamenRepository();

        // Acción para agregar un nuevo examen.
        [HttpPost]
        [Route("agregar")]
        public IHttpActionResult AgregarExamen([FromBody] ExamenRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Nombre))
                return BadRequest("El nombre es obligatorio.");

            AgregarResult resultado = _repository.Agregar(request.Nombre, request.Descripcion);
            return Ok(resultado);
        }
        // Acción para actualizar un examen existente.
        [HttpPut]
        [Route("actualizar")]
        public IHttpActionResult ActualizarExamen([FromBody] ExamenRequest request)
        {
            if (request == null || request.Id <= 0)
                return BadRequest("El Id es obligatorio.");

            OperationResult resultado = _repository.Actualizar(request.Id, request.Nombre, request.Descripcion);
            return Ok(resultado);
        }
        // Acción para eliminar un examen por su Id.
        [HttpDelete]
        [Route("eliminar/{id:int}")]
        public IHttpActionResult EliminarExamen(int id)
        {
            OperationResult resultado = _repository.Eliminar(id);
            return Ok(resultado);
        }
        // Acción para consultar exámenes según criterios opcionales.
        [HttpGet]
        [Route("consultar")]
        public IHttpActionResult ConsultarExamen(int? id = null, string nombre = null, string descripcion = null)
        {
            ConsultaResult resultado = _repository.Consultar(id, nombre, descripcion);
            return Ok(resultado);
        }
    }
}