using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BdiExamen.Model.Dtos;

namespace BdiExamen.ApiExamen.Gateways
{
    // Implementación de IExamenGateway que utiliza un servicio web RESTful para interactuar con la fuente de datos de exámenes.
    // Esta clase encapsula la lógica de acceso a datos a través de HTTP, manejando las solicitudes y respuestas JSON.
    // Cada método devuelve un objeto de resultado estandarizado (AgregarResult, OperationResult, ConsultaResult) que indica el éxito o fallo de la operación.
    public class WebServiceExamenGateway : IExamenGateway
    {
        private readonly string _baseUrl;

        public WebServiceExamenGateway()
        {
            _baseUrl = ConfigurationManager.AppSettings["WsApiexamenBaseUrl"];
        }

        public async Task<AgregarResult> AgregarAsync(string nombre, string descripcion)
        {
            var payload = new { Nombre = nombre, Descripcion = descripcion };
            return await PostOrPutAsync<AgregarResult>(HttpMethod.Post, "api/examen/agregar", payload);
        }

        public async Task<OperationResult> ActualizarAsync(int id, string nombre, string descripcion)
        {
            var payload = new { Id = id, Nombre = nombre, Descripcion = descripcion };
            return await PostOrPutAsync<OperationResult>(HttpMethod.Put, "api/examen/actualizar", payload);
        }

        public async Task<OperationResult> EliminarAsync(int id)
        {
            using (var client = CreateClient())
            {
                try
                {
                    var response = await client.DeleteAsync($"api/examen/eliminar/{id}");
                    var body = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                        return OperationResult.Error(-1, $"Error HTTP {(int)response.StatusCode}: {body}");

                    return JsonConvert.DeserializeObject<OperationResult>(body);
                }
                catch (Exception ex)
                {
                    return OperationResult.Error(-1, ex.Message);
                }
            }
        }

        public async Task<ConsultaResult> ConsultarAsync(int? id, string nombre, string descripcion)
        {
            using (var client = CreateClient())
            {
                try
                {
                    var query = $"api/examen/consultar?id={id}&nombre={nombre}&descripcion={descripcion}";
                    var response = await client.GetAsync(query);
                    var body = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                        return ConsultaResult.Error($"Error HTTP {(int)response.StatusCode}: {body}");

                    return JsonConvert.DeserializeObject<ConsultaResult>(body);
                }
                catch (Exception ex)
                {
                    return ConsultaResult.Error(ex.Message);
                }
            }
        }

        // Helper único para Agregar (POST) y Actualizar (PUT), que comparten la misma forma.
        private async Task<T> PostOrPutAsync<T>(HttpMethod method, string relativeUrl, object payload) where T : OperationResult, new()
        {
            using (var client = CreateClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage(method, relativeUrl) { Content = content };
                    var response = await client.SendAsync(request);
                    var body = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        return new T
                        {
                            Exitoso = false,
                            CodigoRetorno = -1,
                            DescripcionRetorno = $"Error HTTP {(int)response.StatusCode}: {body}"
                        };
                    }

                    return JsonConvert.DeserializeObject<T>(body);
                }
                catch (Exception ex)
                {
                    return new T { Exitoso = false, CodigoRetorno = -1, DescripcionRetorno = ex.Message };
                }
            }
        }

        private HttpClient CreateClient()
        {
            return new HttpClient { BaseAddress = new Uri(_baseUrl) };
        }
    }
}