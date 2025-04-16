using System;
using System.ComponentModel.DataAnnotations;

namespace GestordeGuarderias.Web.Models
{
    public class GuarderiaViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder los 200 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(300, ErrorMessage = "La dirección no puede exceder los 300 caracteres")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "El número de teléfono no es válido")]
        public string Telefono { get; set; } = string.Empty;
    }
}
