using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Web.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestordeGuarderias.Web.ViewModels;
using GestordeGuarderias.Domain.Entities;

namespace GestordeGuarderias.Web.Controllers
{
    public class PagosController : Controller
    {
        private readonly HttpClient _httpClient;

        public PagosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7037/api");
        }

        // Listar
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("/api/Pago");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var pagos = JsonConvert.DeserializeObject<IEnumerable<PagoViewModel>>(content);
                return View("Index", pagos);
            }

            return View(new List<PagoViewModel>());
        }

        // Crear
        public async Task<IActionResult> Create()
        {
            var ninos = await ObtenerNinos();
            var guarderias = await ObtenerGuarderias();
            var tutores = await ObtenerTutores();
            var viewModel = new PagoViewModel
            {
                Ninos = ninos,
                Guarderias = guarderias,
                Tutores = tutores
            };

            return View(viewModel);
        }

        // POST: Crear pago
        [HttpPost]
        public async Task<IActionResult> Create(PagoDTO pago)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(pago);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Pago", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Pago registrado correctamente.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Error al crear el pago");
            }

            var ninos = await ObtenerNinos();
            var guarderias = await ObtenerGuarderias();
            var viewModel = new PagoViewModel
            {
                Monto = pago.Monto,
                Fecha = pago.Fecha,
                NinoId = pago.NinoId,
                GuarderiaId = pago.GuarderiaId,
                Ninos = ninos,
                Guarderias = guarderias
            };

            return View(viewModel);
        }

        // Editar
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Pago/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var pago = JsonConvert.DeserializeObject<PagoViewModel>(content);
                if (pago == null)
                {
                    TempData["Error"] = "No se pudo deserializar la información del pago.";
                    return RedirectToAction("Index");
                }

                pago.Ninos = await ObtenerNinos();
                pago.Guarderias = await ObtenerGuarderias();

                return View(pago);
            }

            TempData["Error"] = "No se pudo obtener el pago";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, PagoDTO pago)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(pago);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/Pago/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Pago actualizado correctamente.";
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
            var viewModel = new PagoViewModel
            {
                Id = id,
                Monto = pago.Monto,
                Fecha = pago.Fecha,
                NinoId = pago.NinoId,
                GuarderiaId = pago.GuarderiaId,
                Ninos = ninos,
                Guarderias = guarderias
            };

            return View(viewModel);
        }

        // Detalles
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Pago/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var pago = JsonConvert.DeserializeObject<PagoViewModel>(content);
                return View(pago);
            }

            TempData["Error"] = "No se pudo obtener el pago";
            return RedirectToAction("Index");
        }

        // Eliminar
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Pago/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var pago = JsonConvert.DeserializeObject<PagoViewModel>(content);
                return View(pago);
            }

            TempData["Error"] = "No se pudo obtener la información del pago para eliminar.";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Pago/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Pago eliminado correctamente.";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Error al eliminar el pago.";
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
        public async Task<List<SelectListItem>> ObtenerTutores()
{
    var response = await _httpClient.GetAsync("/api/Tutores");

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();
        var tutores = JsonConvert.DeserializeObject<List<TutorViewModel>>(content) ?? new List<TutorViewModel>();

        return tutores.Select(t => new SelectListItem
        {
            Value = t.Id.ToString(),
            Text = $"{t.Nombre} {t.Apellido}"
        }).ToList();
    }

    return new List<SelectListItem>();
}

    }
}
