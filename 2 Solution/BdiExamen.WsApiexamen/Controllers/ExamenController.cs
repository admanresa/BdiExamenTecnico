using System.Web.Http;
using BdiExamen.DAL;
using BdiExamen.Model.Dtos;
using BdiExamen.WsApiexamen.Models;

namespace BdiExamen.WsApiexamen.Controllers
{
    [RoutePrefix("api/examen")]
    public class ExamenController : ApiController
    {
        private readonly IExamenRepository _repository = new ExamenRepository();

        [HttpPost]
        [Route("agregar")]
        public IHttpActionResult AgregarExamen([FromBody] ExamenRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Nombre))
                return BadRequest("El nombre es obligatorio.");

            AgregarResult resultado = _repository.Agregar(request.Nombre, request.Descripcion);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("actualizar")]
        public IHttpActionResult ActualizarExamen([FromBody] ExamenRequest request)
        {
            if (request == null || request.Id <= 0)
                return BadRequest("El Id es obligatorio.");

            OperationResult resultado = _repository.Actualizar(request.Id, request.Nombre, request.Descripcion);
            return Ok(resultado);
        }

        [HttpDelete]
        [Route("eliminar/{id:int}")]
        public IHttpActionResult EliminarExamen(int id)
        {
            OperationResult resultado = _repository.Eliminar(id);
            return Ok(resultado);
        }

        [HttpGet]
        [Route("consultar")]
        public IHttpActionResult ConsultarExamen(int? id = null, string nombre = null, string descripcion = null)
        {
            ConsultaResult resultado = _repository.Consultar(id, nombre, descripcion);
            return Ok(resultado);
        }
    }
}