namespace BdiExamen.Model.Dtos
{
    // Clase que representa el resultado de una operación genérica (agregar, actualizar, eliminar).
    public class OperationResult
    {
        public bool Exitoso { get; set; }
        public int CodigoRetorno { get; set; }
        public string DescripcionRetorno { get; set; }
        
        // Se usa expression-bodied member para acortar código y mejorar la legibilidad en ambos métodos estáticos Ok y Error.

        // Devuelve una instancia de OperationResult indicando que la operación fue exitosa, con un mensaje descriptivo.
        public static OperationResult Ok(string mensaje)
            => new OperationResult { Exitoso = true, CodigoRetorno = 0, DescripcionRetorno = mensaje };
        // Devuelve una instancia de OperationResult indicando que la operación falló, con un código de error y un mensaje descriptivo.
        public static OperationResult Error(int codigo, string mensaje)
            => new OperationResult { Exitoso = false, CodigoRetorno = codigo, DescripcionRetorno = mensaje };
    }
}