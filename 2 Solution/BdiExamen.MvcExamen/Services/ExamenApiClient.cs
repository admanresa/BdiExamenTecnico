using BdiExamen.Model.Dtos;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace BdiExamen.MvcExamen.Services
{
    // Clase cliente para interactuar con ApiExamen, implementando la interfaz IExamenApiClient.
    public class ExamenApiClient : IExamenApiClient
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        // Constructor que inicializa la URL base y el cliente HTTP
        public ExamenApiClient(string baseUrl)
        {
            _baseUrl = baseUrl.TrimEnd('/');
            _httpClient = new HttpClient();
        }
        // Método genérico para manejar excepciones y retornar un resultado de fallback
        private static T Guard<T>(Func<T> action, Func<string, T> fallback)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return fallback(ex.Message);
            }
        }
        // Método para consultar exámenes con parámetros opcionales
        public ConsultaResult Consultar(int? id = null, string nombre = null, string descripcion = null)
        {
            return Guard(
                () =>
                {
                    string url = $"{_baseUrl}/api/examen/consultar";
                    var parameters = new System.Collections.Generic.List<string>();

                    if (id.HasValue)
                        parameters.Add($"id={id}");
                    if (!string.IsNullOrEmpty(nombre))
                        parameters.Add($"nombre={Uri.EscapeDataString(nombre)}");
                    if (!string.IsNullOrEmpty(descripcion))
                        parameters.Add($"descripcion={Uri.EscapeDataString(descripcion)}");

                    if (parameters.Count > 0)
                        url += "?" + string.Join("&", parameters);

                    var response = _httpClient.GetAsync(url).Result;
                    var content = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<ConsultaResult>(content);
                },
                msg => new ConsultaResult
                {
                    Exitoso = false,
                    DescripcionRetorno = $"Error en consulta: {msg}"
                });
        }
        // Método para agregar un nuevo examen
        public AgregarResult Agregar(string nombre, string descripcion)
        {
            return Guard(
                () =>
                {
                    var payload = new { nombre, descripcion };
                    var json = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = _httpClient.PostAsync($"{_baseUrl}/api/examen/agregar", content).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<AgregarResult>(responseContent);
                },
                msg => new AgregarResult
                {
                    Exitoso = false,
                    DescripcionRetorno = $"Error al agregar: {msg}"
                });
        }
        // Método para actualizar un examen existente
        public OperationResult Actualizar(int id, string nombre, string descripcion)
        {
            return Guard(
                () =>
                {
                    var payload = new { id, nombre, descripcion };
                    var json = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUrl}/api/examen/actualizar")
                    {
                        Content = content
                    };
                    var response = _httpClient.SendAsync(request).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<OperationResult>(responseContent);
                },
                msg => new OperationResult
                {
                    Exitoso = false,
                    DescripcionRetorno = $"Error al actualizar: {msg}"
                });
        }
        // Método para eliminar un examen por su ID
        public OperationResult Eliminar(int id)
        {
            return Guard(
                () =>
                {
                    var response = _httpClient.DeleteAsync($"{_baseUrl}/api/examen/eliminar/{id}").Result;
                    var content = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<OperationResult>(content);
                },
                msg => new OperationResult
                {
                    Exitoso = false,
                    DescripcionRetorno = $"Error al eliminar: {msg}"
                });
        }
    }
}
