namespace GestordeGuarderias.Application.DTOs
{
    public class NinoDTO
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Guid TutorId { get; set; }
        public Guid GuarderiaId { get; set; }
        public GuarderiaDTO? Guarderia { get; set; }
        public TutorDTO? Tutor { get; set; }
    }
}