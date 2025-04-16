namespace GestordeGuarderias.Domain.Entities
{
    public class Nino
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required DateTime FechaNacimiento { get; set; }
        public Guid TutorId { get; set; }
        public Tutor? Tutor { get; set; }
        public Guid GuarderiaId { get; set; }
        public Guarderia? Guarderia { get; set; }
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
        public ICollection<ActividadNino> ActividadesNinos { get; set; } = new List<ActividadNino>();
    }
}