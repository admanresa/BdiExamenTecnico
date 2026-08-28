using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BdiExamen.Model.Dtos;
using BdiExamen.Model.Entities;
using System.Threading.Tasks;

namespace BdiExamen.ApiExamen.Gateways
{
    // Implementación de IExamenGateway que utiliza procedimientos almacenados (Stored Procedures) para interactuar con la base de datos.
    // Esta clase encapsula la lógica de acceso a datos y maneja la conexión, ejecución de comandos y manejo de resultados.
    // Cada método devuelve un objeto de resultado estandarizado (AgregarResult, OperationResult, ConsultaResult) que indica el éxito o fallo de la operación.
    public class SpExamenGateway : IExamenGateway
    {
        private readonly string _connectionString;

        public SpExamenGateway()
        {
            _connectionString = ConfigurationManager
                .ConnectionStrings["ExamenSqlDirecto"].ConnectionString;
        }

        public async Task<AgregarResult> AgregarAsync(string nombre, string descripcion)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("dbo.spAgregarExamen", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Descripcion", (object)descripcion ?? DBNull.Value);

                var idGeneradoParam = cmd.Parameters.Add("@IdGenerado", SqlDbType.Int);
                idGeneradoParam.Direction = ParameterDirection.Output;

                var codigoParam = cmd.Parameters.Add("@CodigoRetorno", SqlDbType.Int);
                codigoParam.Direction = ParameterDirection.Output;

                var descripcionParam = cmd.Parameters.Add("@DescripcionRetorno", SqlDbType.NVarChar, 500);
                descripcionParam.Direction = ParameterDirection.Output;

                try
                {
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    int codigo = (int)codigoParam.Value;
                    string mensaje = descripcionParam.Value?.ToString();

                    return codigo == 0
                        ? AgregarResult.Ok((int)idGeneradoParam.Value, mensaje)
                        : AgregarResult.Fallido(codigo, mensaje);
                }
                catch (Exception ex)
                {
                    return AgregarResult.Fallido(-1, ex.Message);
                }
            }
        }

        public async Task<OperationResult> ActualizarAsync(int id, string nombre, string descripcion)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("dbo.spActualizarExamen", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Descripcion", (object)descripcion ?? DBNull.Value);

                var codRet = cmd.Parameters.Add("@CodigoRetorno", SqlDbType.Int);
                codRet.Direction = ParameterDirection.Output;

                var descRet = cmd.Parameters.Add("@DescripcionRetorno", SqlDbType.NVarChar, 500);
                descRet.Direction = ParameterDirection.Output;

                try
                {
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    int codigo = (int)codRet.Value;
                    string texto = descRet.Value?.ToString();

                    return codigo == 0
                        ? OperationResult.Ok(texto)
                        : OperationResult.Error(codigo, texto);
                }
                catch (Exception ex)
                {
                    return OperationResult.Error(-1, ex.Message);
                }
            }
        }

        public async Task<OperationResult> EliminarAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("dbo.spEliminarExamen", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                var cod = cmd.Parameters.Add("@CodigoRetorno", SqlDbType.Int);
                cod.Direction = ParameterDirection.Output;

                var mensajeRet = cmd.Parameters.Add("@DescripcionRetorno", SqlDbType.NVarChar, 500);
                mensajeRet.Direction = ParameterDirection.Output;

                try
                {
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    int codigo = (int)cod.Value;
                    string mensaje = mensajeRet.Value?.ToString();

                    return codigo == 0
                        ? OperationResult.Ok(mensaje)
                        : OperationResult.Error(codigo, mensaje);
                }
                catch (Exception ex)
                {
                    return OperationResult.Error(-1, ex.Message);
                }
            }
        }

        public async Task<ConsultaResult> ConsultarAsync(int? id, string nombre, string descripcion)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("dbo.spConsultarExamenes", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", (object)id ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Nombre", (object)nombre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Descripcion", (object)descripcion ?? DBNull.Value);

                try
                {
                    var resultados = new List<Examen>();
                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            resultados.Add(new Examen
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion"))
                                    ? null
                                    : reader.GetString(reader.GetOrdinal("Descripcion"))
                            });
                        }
                    }

                    return ConsultaResult.Ok(resultados);
                }
                catch (Exception ex)
                {
                    return ConsultaResult.Error(ex.Message);
                }
            }
        }
    }
}