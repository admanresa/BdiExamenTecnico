namespace BdiExamen.WsApiexamen.Models
{
    // Modelo de entrada para Agregar/Actualizar. Id se ignora en Agregar
    // (lo genera el IDENTITY) y es obligatorio en Actualizar.
    public class ExamenRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}