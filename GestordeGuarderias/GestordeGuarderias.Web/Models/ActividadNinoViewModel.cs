using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestordeGuarderias.Web.ViewModels
{
    public class ActividadNinoViewModel
    {
        [Required(ErrorMessage = "Debe seleccionar un niño")]
        public Guid NinoId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una actividad")]
        public Guid ActividadId { get; set; }

        public List<SelectListItem> Ninos { get; set; } = new();
        public List<SelectListItem> Actividades { get; set; } = new();
    }
}
