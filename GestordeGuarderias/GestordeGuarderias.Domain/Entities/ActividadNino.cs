namespace GestordeGuarderias.Domain.Entities
{
    public class ActividadNino
    {
        public Guid NinoId { get; set; }
        public Nino? Nino { get; set; }

        public Guid ActividadId { get; set; }
        public Actividad? Actividad { get; set; }
    }
}