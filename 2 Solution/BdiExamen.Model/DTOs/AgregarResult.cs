namespace BdiExamen.Model.Dtos
{
    public class AgregarResult : OperationResult
    {
        public int IdGenerado { get; set; }

        public static AgregarResult Ok(int idGenerado, string mensaje)
            => new AgregarResult
            {
                Exitoso = true,
                CodigoRetorno = 0,
                DescripcionRetorno = mensaje,
                IdGenerado = idGenerado
            };

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