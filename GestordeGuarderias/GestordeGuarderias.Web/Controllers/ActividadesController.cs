using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Web.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestordeGuarderias.Web.Controllers
{
    public class ActividadesController : Controller
    {
        private readonly HttpClient _httpClient;

        public ActividadesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7037/api");
        }

        // Listar
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/Actividad");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var actividades = JsonConvert.DeserializeObject<IEnumerable<ActividadViewModel>>(content);
                return View("Index", actividades);
            }

            return View(new List<ActividadViewModel>());
        }

        // Crear
        public async Task<IActionResult> Create()
        {
            var guarderias = await ObtenerGuarderias();
            var viewModel = new ActividadViewModel
            {
                Guarderias = guarderias
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActividadDTO actividad)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(actividad);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/Actividad", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Error al crear la actividad");
            }

            var guarderias = await ObtenerGuarderias();
            var viewModel = new ActividadViewModel
            {
                Nombre = actividad.Nombre,
                Descripcion = actividad.Descripcion,
                Fecha = actividad.Fecha,
                Hora = actividad.Hora,
                GuarderiaId = actividad.GuarderiaId, 
                Guarderias = guarderias
            };

            return View(viewModel);
        }

        public List<SelectListItem> Guarderias { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _httpClient.GetAsync($"/Actividad/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var actividad = JsonConvert.DeserializeObject<ActividadViewModel>(content);
                if (actividad == null)
                {
                    TempData["Error"] = "No se pudo deserializar la información de la actividad.";
                    return RedirectToAction("Index");
                }

                actividad.Guarderias = await ObtenerGuarderias();

                return View(actividad);
            }

            TempData["Error"] = "No se pudo obtener la actividad";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, ActividadDTO actividad)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(actividad);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/Actividad/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Actividad actualizada correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error del servidor: {response.StatusCode}. Detalle: {error}");
                }
            }

            var guarderias = await ObtenerGuarderias();
            var viewModel = new ActividadViewModel
            {
                Id = id,
                Nombre = actividad.Nombre,
                Descripcion = actividad.Descripcion,
                Fecha = actividad.Fecha,
                Hora = actividad.Hora,
                GuarderiaId = actividad.GuarderiaId,
                Guarderias = guarderias
            };

            return View(viewModel);
        }

        // Detalles
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _httpClient.GetAsync($"/Actividad/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var actividad = JsonConvert.DeserializeObject<ActividadViewModel>(content);
                return View(actividad);
            }

            TempData["Error"] = "No se pudo obtener la actividad";
            return RedirectToAction("Index");
        }

        // Eliminar
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.GetAsync($"/Actividad/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var actividad = JsonConvert.DeserializeObject<ActividadViewModel>(content);
                return View(actividad);
            }

            TempData["Error"] = "No se pudo obtener la actividad para eliminar";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/Actividad/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Error al eliminar la actividad";
            return RedirectToAction("Index");
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
