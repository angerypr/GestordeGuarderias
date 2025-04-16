using System;
using System.ComponentModel.DataAnnotations;

namespace GestordeGuarderias.Web.Models
{
    public class TutorViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "Número de teléfono inválido")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(13, ErrorMessage = "La cédula no puede exceder los 13 caracteres")]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico inválido")]
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}
