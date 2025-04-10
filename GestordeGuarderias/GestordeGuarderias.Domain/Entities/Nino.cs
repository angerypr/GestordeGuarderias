namespace GestordeGuarderias.Domain.Entities
{
    public class Nino
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required DateTime FechaNacimiento { get; set; }
        public int TutorId { get; set; }
        public required Tutor Tutor { get; set; }
        public Guid? ActividadId { get; set; }
        public Actividad? Actividad { get; set; }
        public Guid GuarderiaId { get; set; }
        public required Guarderia Guarderia { get; set; }
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
        public ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();
        public ICollection<Actividad> Actividades { get; set; } = new List<Actividad>();
        public ICollection<ActividadNino> ActividadesNinos { get; set; } = new List<ActividadNino>();


    }
}