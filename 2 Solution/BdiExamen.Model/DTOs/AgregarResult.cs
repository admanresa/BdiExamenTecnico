namespace BdiExamen.Model.Dtos
{
    // Clase que representa el resultado de la operación de agregar un examen.
    // Contiene información sobre si la operación fue exitosa, un código de retorno, una descripción del resultado y el ID generado del examen agregado.
    public class AgregarResult : OperationResult
    {
        public int IdGenerado { get; set; }

        // Se usa expression-bodied member para acortar código y mejorar la legibilidad en ambos métodos estáticos Ok y Fallido.

        // Devuelve una instancia de AgregarResult indicando que la operación fue exitosa, con el ID generado y un mensaje descriptivo.
        public static AgregarResult Ok(int idGenerado, string mensaje)
            => new AgregarResult
            {
                Exitoso = true,
                CodigoRetorno = 0,
                DescripcionRetorno = mensaje,
                IdGenerado = idGenerado
            };
        // Devuelve una instancia de AgregarResult indicando que la operación falló, con un código de error y un mensaje descriptivo.
        public static AgregarResult Fallido(int codigo, string mensaje)
            => new AgregarResult
            {
                Exitoso = false,
                CodigoRetorno = codigo,
                DescripcionRetorno = mensaje,
                IdGenerado = 0
            };
    }
}