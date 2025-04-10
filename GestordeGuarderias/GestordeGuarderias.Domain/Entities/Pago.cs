namespace GestordeGuarderias.Domain.Entities
{
    public class Pago 
    {
        public Guid Id { get; set; }
        public required decimal Monto { get; set; }
        public required DateTime Fecha { get; set; }
        public required Guid NinoId { get; set; }
        public required Nino Nino { get; set; }
        public required Guid GuarderiaId { get; set; }
        public required Guarderia Guarderia { get; set; }
        public required Guid TutorId { get; set; }
        public required Tutor Tutor { get; set; }
    }
}