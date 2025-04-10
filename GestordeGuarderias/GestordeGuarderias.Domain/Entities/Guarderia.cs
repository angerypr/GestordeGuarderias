namespace GestordeGuarderias.Domain.Entities
{
    public class Guarderia 
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Direccion { get; set; }
        public required string Telefono { get; set; }
        public ICollection<Nino> Ninos { get; set; } = new List<Nino>();
        public ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
        public ICollection<Actividad> Actividades { get; set; } = new List<Actividad>();
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();
    }
}