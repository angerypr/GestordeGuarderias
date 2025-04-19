using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Web.Models;
using System.Text;

namespace GestordeGuarderias.Web.Controllers
{
    public class GuarderiasController : Controller
    {
        private readonly HttpClient _httpClient;

        public GuarderiasController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7037/api");
        }

        // Listar
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/Guarderias");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var guarderias = JsonConvert.DeserializeObject<IEnumerable<GuarderiaViewModel>>(content);
                return View("Index", guarderias);
            }

            return View(new List<GuarderiaViewModel>());
        }

        // Crear
        public IActionResult Create()
        {
            return View(new GuarderiaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(GuarderiaDTO guarderia)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(guarderia);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/Guarderias", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al añadir la guardería");
                }
            }

            return View(guarderia);
        }

        // Editar
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _httpClient.GetAsync($"/Guarderias/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var guarderia = JsonConvert.DeserializeObject<GuarderiaViewModel>(content);
                return View(guarderia);
            }
            else
            {
                TempData["Error"] = "Error al obtener los datos de la guardería";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, GuarderiaDTO guarderia)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(guarderia);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/Guarderias/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar la guardería");
                }
            }

            return View(guarderia);
        }

        // Mostrar detalles
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _httpClient.GetAsync($"/Guarderias/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var guarderia = JsonConvert.DeserializeObject<GuarderiaViewModel>(content);
                return View(guarderia);
            }
            else
            {
                TempData["Error"] = "Error al obtener los detalles de la guardería";
                return RedirectToAction("Index");
            }
        }

        // Eliminar
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.GetAsync($"/Guarderias/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var guarderia = JsonConvert.DeserializeObject<GuarderiaViewModel>(content);
                return View(guarderia); 
            }

            TempData["Error"] = "No se pudo cargar la guardería para eliminar.";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/Guarderias/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Error al eliminar la guardería";
            return RedirectToAction("Index");
        }

    }
}
