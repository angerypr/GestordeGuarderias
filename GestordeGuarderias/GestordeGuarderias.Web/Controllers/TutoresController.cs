using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Web.Models;
using System.Text;

namespace GestordeGuarderias.Web.Controllers
{
    public class TutoresController : Controller
    {
        private readonly HttpClient _httpClient;

        public TutoresController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7037/api");
        }

        // Listar
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Tutores");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tutores = JsonConvert.DeserializeObject<IEnumerable<TutorViewModel>>(content);
                return View("Index", tutores);
            }

            TempData["Error"] = "Error al cargar la lista de tutores";
            return View(new List<TutorViewModel>());
        }

        // Crear
        public IActionResult Create()
        {
            return View(new TutorViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TutorDTO tutor)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(tutor);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Tutores", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Error al crear el tutor");
            }

            return View(tutor);
        }

        // Editar
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Tutores/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tutor = JsonConvert.DeserializeObject<TutorViewModel>(content);

                if (tutor == null)
                {
                    TempData["Error"] = "No se pudo deserializar la información del tutor.";
                    return RedirectToAction("Index");
                }

                return View(tutor);
            }

            TempData["Error"] = "No se pudo obtener la información del tutor.";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, TutorDTO tutor)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(tutor);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Tutores/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Tutor actualizado correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error del servidor: {response.StatusCode}. Detalle: {error}");
                }
            }

            var viewModel = new TutorViewModel
            {
                Id = id,
                Nombre = tutor.Nombre,
                Apellido = tutor.Apellido
            };

            return View(viewModel);
        }


        // Detalles
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Tutores/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tutor = JsonConvert.DeserializeObject<TutorViewModel>(content);
                return View(tutor);
            }

            TempData["Error"] = "Error al obtener los detalles del tutor";
            return RedirectToAction("Index");
        }

        // Eliminar
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Tutores/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tutor = JsonConvert.DeserializeObject<TutorViewModel>(content);
                return View(tutor);
            }

            TempData["Error"] = "No se pudo cargar el tutor para eliminar.";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Tutores/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Error al eliminar el tutor";
            return RedirectToAction("Index");
        }
    }
}
