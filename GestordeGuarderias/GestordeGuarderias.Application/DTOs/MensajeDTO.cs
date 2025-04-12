namespace GestordeGuarderias.Application.DTOs
{
    public class MensajeDTO
    {
        public Guid Id { get; set; }
        public required string Asunto { get; set; }
        public required string Contenido { get; set; }
        public required DateTime Fecha { get; set; }
        public required TimeSpan Hora { get; set; }
        public Guid NinoId { get; set; }
        public Guid TutorId { get; set; }
        public Guid GuarderiaId { get; set; }
    }

}