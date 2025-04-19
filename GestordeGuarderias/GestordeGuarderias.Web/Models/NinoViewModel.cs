using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestordeGuarderias.Web.Models
{
    public class NinoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tutor")]
        public Guid TutorId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una guardería")]
        public Guid GuarderiaId { get; set; }

        public List<SelectListItem> Tutores { get; set; } = new();
        public List<SelectListItem> Guarderias { get; set; } = new();
        public GuarderiaViewModel Guarderia { get; set; } = new GuarderiaViewModel();
        public TutorViewModel Tutor { get; set; } = new TutorViewModel();
    }
}
