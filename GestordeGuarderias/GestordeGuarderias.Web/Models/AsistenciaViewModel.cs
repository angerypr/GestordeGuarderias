using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestordeGuarderias.Web.ViewModels
{
    public class AsistenciaViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public bool Presente { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un niño")]
        public Guid NinoId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una guardería")]
        public Guid GuarderiaId { get; set; }

        public List<SelectListItem> Ninos { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> Guarderias { get; set; } = new List<SelectListItem>();
    }
}
