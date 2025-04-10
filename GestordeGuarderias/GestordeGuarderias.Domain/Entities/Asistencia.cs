namespace GestordeGuarderias.Domain.Entities
{
    public class Asistencia 
    {
        public Guid Id { get; set; }
        public required bool Presente { get; set; }
        public required DateTime Fecha { get; set; }
        public required Guid NinoId { get; set; }
        public required Nino Nino { get; set; }
        public required Guid GuarderiaId { get; set; }
        public required Guarderia Guarderia { get; set; }
    }
}