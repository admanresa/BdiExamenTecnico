namespace BdiExamen.Model.Dtos
{
    public class OperationResult
    {
        public bool Exitoso { get; set; }
        public int CodigoRetorno { get; set; }
        public string DescripcionRetorno { get; set; }

        public static OperationResult Ok(string mensaje)
            => new OperationResult { Exitoso = true, CodigoRetorno = 0, DescripcionRetorno = mensaje };

        public static OperationResult Error(int codigo, string mensaje)
            => new OperationResult { Exitoso = false, CodigoRetorno = codigo, DescripcionRetorno = mensaje };
    }
}