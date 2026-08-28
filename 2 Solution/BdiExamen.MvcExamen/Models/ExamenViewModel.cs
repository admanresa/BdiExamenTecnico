using BdiExamen.Model.Entities;
using System.Collections.Generic;

namespace BdiExamen.MvcExamen.Models
{
    // ViewModel para la vista de índice de Examenes
    public class ExamenIndexViewModel
    {
        public List<Examen> Examenes { get; set; } = new List<Examen>();
        public string Mensaje { get; set; }
        public bool EsError { get; set; }
        public int? FiltroId { get; set; }
        public string FiltroNombre { get; set; }
        public string FiltroDescripcion { get; set; }
    }
    // ViewModel para la vista de formulario de Examen
    public class ExamenFormViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
