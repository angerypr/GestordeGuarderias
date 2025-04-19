using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Web.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestordeGuarderias.Web.ViewModels;

namespace GestordeGuarderias.Web.Controllers
{
    public class AsistenciasController : Controller
    {
        private readonly HttpClient _httpClient;

        public AsistenciasController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7037/api");
        }

        // Listar
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Asistencia");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var asistencias = JsonConvert.DeserializeObject<IEnumerable<AsistenciaViewModel>>(content);
                return View("Index", asistencias);
            }

            return View(new List<AsistenciaViewModel>());
        }

        // Crear
        public async Task<IActionResult> Create()
        {
            var ninos = await ObtenerNinos();
            var viewModel = new AsistenciaViewModel
            {
                Ninos = ninos
            };

            return View(viewModel);
        }

        // POST: Crear asistencia
        [HttpPost]
        public async Task<IActionResult> Create(AsistenciaDTO asistencia)
        {
            if (ModelState.IsValid)
            {
                // Obtener la guardería automáticamente desde el niño
                var responseNino = await _httpClient.GetAsync($"/api/Ninos/{asistencia.NinoId}");

                if (responseNino.IsSuccessStatusCode)
                {
                    var contentNino = await responseNino.Content.ReadAsStringAsync();
                    var nino = JsonConvert.DeserializeObject<NinoViewModel>(contentNino);

                    if (nino != null && nino.GuarderiaId != Guid.Empty)
                    {
                        asistencia.GuarderiaId = nino.GuarderiaId;

                        var json = JsonConvert.SerializeObject(asistencia);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        var response = await _httpClient.PostAsync("/api/Asistencia", content);

                        if (response.IsSuccessStatusCode)
                        {
                            TempData["Success"] = "Asistencia registrada correctamente.";
                            return RedirectToAction("Index");
                        }

                        ModelState.AddModelError(string.Empty, "Error al crear la asistencia");
                    }
                    else
                    {
                        ModelState.AddModelError("", "El niño no tiene una guardería asociada.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo obtener la información del niño.");
                }
            }

            var ninos = await ObtenerNinos();
            var viewModel = new AsistenciaViewModel
            {
                Fecha = asistencia.Fecha,
                Presente = asistencia.Presente,
                NinoId = asistencia.NinoId,
                Ninos = ninos
            };

            return View(viewModel);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Asistencia/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var asistencia = JsonConvert.DeserializeObject<AsistenciaViewModel>(content);
                if (asistencia == null)
                {
                    TempData["Error"] = "No se pudo deserializar la información de la asistencia.";
                    return RedirectToAction("Index");
                }

                asistencia.Ninos = await ObtenerNinos();
                asistencia.Guarderias = await ObtenerGuarderias();

                return View(asistencia);
            }

            TempData["Error"] = "No se pudo obtener la asistencia";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, AsistenciaDTO asistencia)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(asistencia);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Asistencia/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Asistencia actualizada correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error del servidor: {response.StatusCode}. Detalle: {error}");
                }
            }

            var ninos = await ObtenerNinos();
            var guarderias = await ObtenerGuarderias();
            var viewModel = new AsistenciaViewModel
            {
                Id = id,
                Fecha = asistencia.Fecha,
                Presente = asistencia.Presente,
                NinoId = asistencia.NinoId,
                GuarderiaId = asistencia.GuarderiaId,
                Ninos = ninos,
                Guarderias = guarderias
            };

            return View(viewModel);
        }

        // Detalles
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Asistencia/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var asistencia = JsonConvert.DeserializeObject<AsistenciaViewModel>(content);
                return View(asistencia);
            }

            TempData["Error"] = "No se pudo obtener la asistencia";
            return RedirectToAction("Index");
        }

        // Eliminar
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Asistencia/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var asistencia = JsonConvert.DeserializeObject<AsistenciaViewModel>(content);
                return View(asistencia);
            }

            TempData["Error"] = "No se pudo obtener la asistencia para eliminar";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Asistencia/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Error al eliminar la asistencia";
            return RedirectToAction("Index");
        }

        public async Task<List<SelectListItem>> ObtenerNinos()
        {
            var response = await _httpClient.GetAsync("/api/Ninos");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var ninos = JsonConvert.DeserializeObject<List<NinoViewModel>>(content) ?? new List<NinoViewModel>();

                return ninos.Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = $"{n.Nombre} {n.Apellido}"
                }).ToList();
            }

            return new List<SelectListItem>();
        }

        public async Task<List<SelectListItem>> ObtenerGuarderias()
        {
            var response = await _httpClient.GetAsync("/Guarderias");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var guarderias = JsonConvert.DeserializeObject<List<GuarderiaViewModel>>(content) ?? new List<GuarderiaViewModel>();

                return guarderias.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Nombre
                }).ToList();
            }

            return new List<SelectListItem>();
        }
    }
}
