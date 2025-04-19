using GestordeGuarderias.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestordeGuarderias.Web.Models
{
    public class PagoViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un niño")]
        public Guid NinoId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una guardería")]
        public Guid GuarderiaId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tutor")]
        public Guid TutorId { get; set; }
        public NinoViewModel Nino { get; set; } = new NinoViewModel();
        public GuarderiaViewModel Guarderia { get; set; } = new GuarderiaViewModel();
        public TutorViewModel Tutor { get; set; } = new TutorViewModel();

        public List<SelectListItem> Ninos { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Guarderias { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Tutores { get; set; } = new List<SelectListItem>();
    }
}
