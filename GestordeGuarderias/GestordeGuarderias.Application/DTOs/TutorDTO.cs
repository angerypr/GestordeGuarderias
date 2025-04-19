namespace GestordeGuarderias.Application.DTOs
{
    public class TutorDTO
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}