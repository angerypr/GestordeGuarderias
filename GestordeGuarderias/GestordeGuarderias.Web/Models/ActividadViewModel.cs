using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GestordeGuarderias.Web.Models
{
    public class ActividadViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre de la actividad es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora es obligatoria")]
        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una guardería")]
        public Guid GuarderiaId { get; set; }

        public List<SelectListItem> Guarderias { get; set; } = new();
    }
}
