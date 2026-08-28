using System.Collections.Generic;
using BdiExamen.Model.Entities;

namespace BdiExamen.Model.Dtos
{
    public class ConsultaResult
    {
        public bool Exitoso { get; set; }
        public string DescripcionRetorno { get; set; }
        public List<Examen> Resultados { get; set; } = new List<Examen>();

        public static ConsultaResult Ok(List<Examen> resultados)
            => new ConsultaResult { Exitoso = true, Resultados = resultados };

        public static ConsultaResult Error(string mensaje)
            => new ConsultaResult { Exitoso = false, DescripcionRetorno = mensaje, Resultados = new List<Examen>() };
    }
}