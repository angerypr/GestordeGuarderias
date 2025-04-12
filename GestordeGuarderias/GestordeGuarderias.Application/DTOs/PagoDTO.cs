namespace GestordeGuarderias.Application.DTOs
{
    public class PagoDTO
    {
        public Guid Id { get; set; }
        public required decimal Monto { get; set; }
        public required DateTime Fecha { get; set; }
        public required Guid NinoId { get; set; }
        public required Guid GuarderiaId { get; set; }
        public required Guid TutorId { get; set; }
    }
}