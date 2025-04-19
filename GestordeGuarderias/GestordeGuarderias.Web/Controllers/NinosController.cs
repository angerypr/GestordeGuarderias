using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace GestordeGuarderias.Web.Controllers
{
    public class NinosController : Controller
    {
        private readonly HttpClient _httpClient;

        public NinosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7037/api");
        }

        // Index
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Ninos");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var ninos = JsonConvert.DeserializeObject<IEnumerable<NinoDTO>>(content);
                return View(ninos);
            }

            TempData["Error"] = "No se pudo cargar la lista de niños.";
            return View(new List<NinoDTO>());
        }

        // Crear
        public async Task<IActionResult> Create()
        {
            var tutores = await ObtenerTutores();
            var guarderias = await ObtenerGuarderias();

            var viewModel = new NinoViewModel
            {
                Tutores = tutores,
                Guarderias = guarderias
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NinoDTO nino)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(nino);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Ninos", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Niño registrado correctamente.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Error al crear el niño");
            }

            var tutores = await ObtenerTutores();
            var guarderias = await ObtenerGuarderias();
            var viewModel = new NinoViewModel
            {
                Nombre = nino.Nombre,
                Apellido = nino.Apellido,
                FechaNacimiento = nino.FechaNacimiento,
                TutorId = nino.TutorId,
                GuarderiaId = nino.GuarderiaId,
                Tutores = tutores,
                Guarderias = guarderias
            };

            return View(viewModel);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Ninos/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var nino = JsonConvert.DeserializeObject<NinoViewModel>(content);

                if (nino == null)
                {
                    TempData["Error"] = "No se pudo deserializar la información del niño.";
                    return RedirectToAction("Index");
                }

                return View(nino);
            }

            TempData["Error"] = "No se pudo obtener la información del niño.";
            return RedirectToAction("Index");
        }

        // Editar
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Ninos/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var nino = JsonConvert.DeserializeObject<NinoViewModel>(content);

                if (nino == null)
                {
                    TempData["Error"] = "No se pudo deserializar la información del niño.";
                    return RedirectToAction("Index");
                }

                nino.Tutores = await ObtenerTutores();
                nino.Guarderias = await ObtenerGuarderias();

                return View(nino);
            }

            TempData["Error"] = "No se pudo obtener la información del niño";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, NinoViewModel nino)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(nino);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Ninos/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Niño actualizado correctamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Error del servidor: {response.StatusCode}. Detalle: {error}");
                }
            }

            var tutores = await ObtenerTutores();
            var guarderias = await ObtenerGuarderias();
            var viewModel = new NinoViewModel
            {
                Id = id,
                Nombre = nino.Nombre,
                Apellido = nino.Apellido,
                FechaNacimiento = nino.FechaNacimiento,
                TutorId = nino.TutorId,
                GuarderiaId = nino.GuarderiaId,
                Tutores = tutores,
                Guarderias = guarderias
            };

            return View(viewModel);
        }

        // Eliminar
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Ninos/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var nino = JsonConvert.DeserializeObject<NinoViewModel>(content); 
                return View(nino); 
            }

            TempData["Error"] = "No se pudo obtener el niño para eliminar";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Ninos/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Error al eliminar el niño";
            return RedirectToAction("Index");
        }
        private async Task<List<SelectListItem>> ObtenerTutores()
        {
            var response = await _httpClient.GetAsync("/api/Tutores");
            var list = new List<SelectListItem>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tutores = JsonConvert.DeserializeObject<IEnumerable<TutorDTO>>(content) ?? new List<TutorDTO>();
                list = tutores.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"{t.Nombre} {t.Apellido}"
                }).ToList();
            }

            return list;
        }
        private async Task<List<SelectListItem>> ObtenerGuarderias()
        {
            var response = await _httpClient.GetAsync("/Guarderias");
            var list = new List<SelectListItem>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var guarderias = JsonConvert.DeserializeObject<IEnumerable<GuarderiaDTO>>(content) ?? new List<GuarderiaDTO>();
                list = guarderias.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Nombre
                }).ToList();
            }

            return list;
        }
    }
}
