namespace GestordeGuarderias.Application.DTOs
{
    public class TutorDTO
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Telefono { get; set; }
        public required string Cedula { get; set; }
        public required string CorreoElectronico { get; set; }
    }
}