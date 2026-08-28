using BdiExamen.MvcExamen.Models;
using BdiExamen.MvcExamen.Services;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace BdiExamen.MvcExamen.Controllers
{
    public class ExamenController : Controller
    {
        private readonly IExamenApiClient _apiClient;

        public ExamenController()
        {
            string baseUrl = ConfigurationManager.AppSettings["WsApiexamenBaseUrl"] ?? "https://localhost:44352";
            _apiClient = new ExamenApiClient(baseUrl);
        }

        public ExamenController(IExamenApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public ActionResult Index(int? id = null, string nombre = null, string descripcion = null)
        {
            try
            {
                var resultado = _apiClient.Consultar(id, nombre, descripcion);

                var viewModel = new ExamenIndexViewModel
                {
                    Examenes = resultado.Resultados,
                    Mensaje = resultado.DescripcionRetorno,
                    EsError = !resultado.Exitoso
                };

                if (TempData["Success"] != null)
                {
                    viewModel.Mensaje = TempData["Success"].ToString();
                    viewModel.EsError = false;
                }
                if (TempData["Error"] != null)
                {
                    viewModel.Mensaje = TempData["Error"].ToString();
                    viewModel.EsError = true;
                }

                viewModel.FiltroId = id;
                viewModel.FiltroNombre = nombre;
                viewModel.FiltroDescripcion = descripcion;

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Create()
        {
            return View(new ExamenFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExamenFormViewModel viewModel)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(viewModel.Nombre))
            {
                TempData["Error"] = "El nombre es obligatorio";
                return View(viewModel);
            }

            try
            {
                var resultado = _apiClient.Agregar(viewModel.Nombre, viewModel.Descripcion ?? "");

                if (resultado.Exitoso)
                {
                    TempData["Success"] = $"{resultado.DescripcionRetorno} (ID: {resultado.IdGenerado})";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = resultado.DescripcionRetorno;
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return View(viewModel);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var resultado = _apiClient.Consultar(id);

                if (resultado.Exitoso && resultado.Resultados.Count > 0)
                {
                    var examen = resultado.Resultados[0];
                    return View(new ExamenFormViewModel
                    {
                        Id = examen.Id,
                        Nombre = examen.Nombre,
                        Descripcion = examen.Descripcion
                    });
                }

                TempData["Error"] = "Examen no encontrado";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExamenFormViewModel viewModel)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(viewModel.Nombre))
            {
                TempData["Error"] = "El nombre es obligatorio";
                return View(viewModel);
            }

            try
            {
                var resultado = _apiClient.Actualizar(viewModel.Id, viewModel.Nombre, viewModel.Descripcion ?? "");

                if (resultado.Exitoso)
                {
                    TempData["Success"] = resultado.DescripcionRetorno;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = resultado.DescripcionRetorno;
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var resultado = _apiClient.Consultar(id);

                if (resultado.Exitoso && resultado.Resultados.Count > 0)
                {
                    var examen = resultado.Resultados[0];
                    return View(new ExamenFormViewModel
                    {
                        Id = examen.Id,
                        Nombre = examen.Nombre,
                        Descripcion = examen.Descripcion
                    });
                }

                TempData["Error"] = "Examen no encontrado";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ExamenFormViewModel viewModel)
        {
            try
            {
                var resultado = _apiClient.Eliminar(viewModel.Id);

                if (resultado.Exitoso)
                {
                    TempData["Success"] = resultado.DescripcionRetorno;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = resultado.DescripcionRetorno;
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return View(viewModel);
            }
        }
    }
}
