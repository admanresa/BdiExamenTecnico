using System.Collections.Generic;
using BdiExamen.Model.Entities;

namespace BdiExamen.Model.Dtos
{
    // Clase que representa el resultado de la operación de consulta de exámenes.
    public class ConsultaResult
    {
        public bool Exitoso { get; set; }
        public string DescripcionRetorno { get; set; }
        public List<Examen> Resultados { get; set; } = new List<Examen>();

        // Se usa expression-bodied member para acortar código y mejorar la legibilidad en ambos métodos estáticos Ok y Error.

        // Devuelve una instancia de ConsultaResult indicando que la operación fue exitosa, con la lista de resultados obtenidos.
        public static ConsultaResult Ok(List<Examen> resultados)
            => new ConsultaResult { Exitoso = true, Resultados = resultados };

        // Devuelve una instancia de ConsultaResult indicando que la operación falló, con un mensaje descriptivo y una lista vacía de resultados.
        public static ConsultaResult Error(string mensaje)
            => new ConsultaResult { Exitoso = false, DescripcionRetorno = mensaje, Resultados = new List<Examen>() };
    }
}