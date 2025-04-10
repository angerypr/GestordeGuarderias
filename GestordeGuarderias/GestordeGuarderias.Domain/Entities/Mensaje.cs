namespace GestordeGuarderias.Domain.Entities
{
    public class Mensaje 
    {
        public Guid Id { get; set; }
        public required string Asunto { get; set; }
        public required string Contenido { get; set; }
        public required DateTime Fecha { get; set; }
        public required TimeSpan Hora { get; set; }
        public Guid NinoId { get; set; }
        public required Nino Nino { get; set; }
        public Guid TutorId { get; set; }
        public required Tutor Tutor { get; set; }
        public Guid GuarderiaId { get; set; }
        public required Guarderia Guarderia { get; set; }

    }

}