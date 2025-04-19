namespace GestordeGuarderias.Domain.Entities
{
    public class Nino
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required DateTime FechaNacimiento { get; set; }
        public Guid TutorId { get; set; }
        public required Tutor Tutor { get; set; }
        public Guid GuarderiaId { get; set; }
        public required Guarderia Guarderia { get; set; }
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
    }
}